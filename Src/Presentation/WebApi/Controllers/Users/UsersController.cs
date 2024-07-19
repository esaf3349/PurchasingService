using Application.Requests.Users.Create;
using Application.Requests.Users.GetById;
using Domain.Model.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;
using CurrentUserDtos = Application.Contracts.Presentation.CurrentUser.Dtos;

namespace WebApi.Controllers.Users;

[ApiController]
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

    [HttpGet("current")]
    public async Task<ActionResult<JsonResponse<CurrentUserDtos.User?>>> GetCurrent()
    {
        return OkJsonResponse(CurrentUser.Details);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<User>>> GetById(Guid id)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest);

        return OkJsonResponse(response);
    }
}