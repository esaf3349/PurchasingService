using Application.Requests.Users.Create;
using Application.Requests.Users.Delete;
using Application.Requests.Users.GetById;
using Application.Requests.Users.GetByLogin;
using Application.Requests.Users.Search;
using Application.Requests.Users.SetEmail;
using Application.Requests.Users.SetFullName;
using Domain.Model.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;
using CurrentUserDtos = Application.Contracts.Presentation.CurrentUser.Dtos;

namespace WebApi.Controllers.Users;

[Route("users")]
public sealed class UsersController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<JsonResponse<Unit>>> Create(string login)
    {
        var appRequest = new CreateRequest { Login = login };
        var response = await Mediator.Send(appRequest);

        return Ok(response);
    }

    [HttpPost("delete")]
    public async Task<ActionResult<JsonResponse<Unit>>> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<User>>> GetById(Guid id)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest);

        return OkJsonResponse(response);
    }

    [HttpGet("{login}")]
    public async Task<ActionResult<JsonResponse<User>>> GetByLogin(string login)
    {
        var appRequest = new GetByLoginRequest { Login = login };
        var response = await Mediator.Send(appRequest);

        return OkJsonResponse(response);
    }

    [HttpGet("current")]
    public async Task<ActionResult<JsonResponse<CurrentUserDtos.User?>>> GetCurrent()
    {
        return OkJsonResponse(CurrentUser.Details);
    }

    [HttpGet("search")]
    public async Task<ActionResult<JsonResponse<IEnumerable<User>>>> Search(string? login, string? firstName, string? lastName, string? middleName, string? email, CancellationToken cancellationToken = default)
    {
        var appRequest = new SearchRequest
        {
            Login = login,
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            Email = email
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/full-name")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetName(Guid id, Dtos.SetFullNameRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetFullNameRequest
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/email")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetEmail(Guid id, Dtos.SetEmailRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetEmailRequest
        {
            Id = id,
            Email = request.Email
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }
}