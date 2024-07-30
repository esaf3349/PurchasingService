using Application.Requests.Roles.Create;
using Application.Requests.Roles.Delete;
using Application.Requests.Roles.GetById;
using Application.Requests.Roles.Search;
using Application.Requests.Roles.SetDescription;
using Application.Requests.Roles.SetName;
using Domain.Model.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Roles;

[Route("roles")]
public sealed class RolesController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<JsonResponse<Guid>>> Create(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPost("delete")]
    public async Task<ActionResult<JsonResponse<Unit>>> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<Role>>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<JsonResponse<IEnumerable<Role>>>> Search(string? name, string? description, bool? forSingleUser, CancellationToken cancellationToken = default)
    {
        var appRequest = new SearchRequest
        {
            Name = name,
            Description = description,
            ForSingleUser = forSingleUser
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/name")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetName(Guid id, Dtos.SetNameRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetNameRequest
        {
            Id = id,
            Name = request.Name
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/description")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetDescription(Guid id, Dtos.SetDescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetDescriptionRequest
        {
            Id = id,
            Description = request.Description
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }
}