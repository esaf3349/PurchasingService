using Application.Requests.Suppliers.Create;
using Application.Requests.Suppliers.Delete;
using Application.Requests.Suppliers.GetById;
using Application.Requests.Suppliers.Search;
using Application.Requests.Suppliers.SetDescription;
using Application.Requests.Suppliers.SetName;
using Domain.Model.Suppliers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Suppliers;

[ApiController]
[Route("suppliers")]
public sealed class SuppliersController : BaseController
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
    public async Task<ActionResult<JsonResponse<Supplier>>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<JsonResponse<IEnumerable<Supplier>>>> Search(string? name, string? description, CancellationToken cancellationToken = default)
    {
        var appRequest = new SearchRequest
        { 
            Name = name, 
            Description = description 
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