using Application.Exceptions;
using Domain.Common.Exceptions;
using System.Net;
using System.Text.Json;
using WebApi.Common.Http;

namespace WebApi.Middleware;

internal sealed class AppExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppExceptionHandlingMiddleware> _logger;

    public AppExceptionHandlingMiddleware(RequestDelegate next, ILogger<AppExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            await WriteToHttpResponse(context.Response, exception);
        }
    }

    private async Task WriteToHttpResponse(HttpResponse response, Exception exception, CancellationToken cancellationToken = default)
    {
        switch (exception)
        {
            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case UnauthorizedException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case ForbiddenException:
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            case AlreadyExistsException:
            case BadRequestException:
            case IDomainException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default: 
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        response.ContentType = "application/json";

        var responseBody = JsonSerializer.Serialize(JsonResponse<object>.Error(exception.Message));

        await response.WriteAsync(responseBody, cancellationToken);
    }
}