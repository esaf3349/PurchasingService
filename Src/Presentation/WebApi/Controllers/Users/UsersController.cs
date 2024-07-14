using Application.Requests.Users.Create;
using Application.Requests.Users.Get;
using Domain.Model.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Users;

[ApiController]
[Route("users")]
public sealed class UsersController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<JsonResponse<Unit>>> Create(string login)
    {
        var appRequest = new CreateUserRequest { Login = login };
        var response = await Mediator.Send(appRequest);

        return Ok(response);
    }

    [HttpGet("current")]
    public async Task<ActionResult<JsonResponse<string?>>> GetCurrent()
    {
        return OkJsonReponse(CurrentUserIdentityName);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<User>>> GetById(Guid id)
    {
        var appRequest = new GetUserRequest { Id = id };
        var response = await Mediator.Send(appRequest);

        return OkJsonReponse(response);
    }
}