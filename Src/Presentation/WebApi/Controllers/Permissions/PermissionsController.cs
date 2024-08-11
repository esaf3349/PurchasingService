using Application.Requests.Permissions.Create;
using Application.Requests.Permissions.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Permissions;

[Route("roles")]
public sealed class PermissionsController : BaseController
{
    [HttpPost("{roleId:guid}/permissions/create")]
    public async Task<ActionResult<JsonResponse<Guid>>> Create(Guid roleId, Dtos.AddRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new CreateRequest
        {
            RoleId = roleId,
            EntityPermissionFilter = request.EntityPermissionFilter,
            EntityIdPermissionFilter = request.EntityIdPermissionFilter,
            PropertyPermissionFilter = request.PropertyPermissionFilter,
            ActionPermissionFilter = request.ActionPermissionFilter
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPost("{roleId:guid}/permissions/{permissionId:guid}/delete")]
    public async Task<ActionResult<JsonResponse<Unit>>> Delete(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default)
    {
        var appRequest = new DeleteRequest
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }
}