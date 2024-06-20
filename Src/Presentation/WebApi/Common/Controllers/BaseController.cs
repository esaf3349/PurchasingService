using Application.Contracts.Presentation.CurrentUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Common.Http;

namespace WebApi.Common.Controllers;

internal abstract class BaseController : Controller
{
    private const string _currentUserSessionName = "CurrentUser";

    private ICurrentUserService _currentUserService => HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

    private protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    private protected string? CurrentUserIdentityName;

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Session.Keys.Contains(_currentUserSessionName))
            CurrentUserIdentityName = context.HttpContext.Session.GetString(_currentUserSessionName);
        else
        {
            CurrentUserIdentityName = _currentUserService.IdentityName;
            context.HttpContext.Session.SetString(_currentUserSessionName, CurrentUserIdentityName ?? string.Empty);
        }

        await next();
    }

    private protected ActionResult<JsonResponse<TData>> OkJsonReponse<TData>(TData data) => Ok(new JsonResponse<TData>(data));
}