using Application.Requests.UserRoles.Create;
using Application.Requests.UserRoles.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.UserRoles;

[Route("users")]
public sealed class UserRolesController : BaseController
{
    [HttpPost("{userId:guid}/roles/create")]
    public async Task<ActionResult<JsonResponse<Unit>>> Create(Guid userId, Dtos.AddRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new CreateRequest
        {
            UserId = userId,
            RoleId = request.RoleId
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return Ok(response);
    }

    [HttpPost("{userId:guid}/roles/{roleId:guid}/delete")]
    public async Task<ActionResult<JsonResponse<Unit>>> Delete(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var appRequest = new DeleteRequest
        {
            UserId = userId,
            RoleId = roleId
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }
}