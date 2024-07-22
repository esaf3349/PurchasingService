using Application.Requests.Departments.Create;
using Application.Requests.Departments.Delete;
using Application.Requests.Departments.GetById;
using Application.Requests.Departments.Search;
using Application.Requests.Departments.SetName;
using Domain.Model.Departments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Departments;

[Route("departments")]
public sealed class DepartmentsController : BaseController
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
    public async Task<ActionResult<JsonResponse<Department>>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<JsonResponse<IEnumerable<Department>>>> Search(string? name, string? description, CancellationToken cancellationToken = default)
    {
        var appRequest = new SearchRequest { Name = name };
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
}