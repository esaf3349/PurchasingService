using Application.Requests.Requisitions.Create;
using Application.Requests.Requisitions.GetById;
using Application.Requests.Requisitions.Search;
using Application.Requests.Requisitions.SetDueDate;
using Application.Requests.Requisitions.SetDepartment;
using Application.Requests.Requisitions.SetSupplier;
using Application.Requests.Requisitions.SetTitle;
using Domain.Model.Requisitions;
using Domain.Model.Requisitions.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Requisitions;

[Route("requisitions")]
public sealed class RequisitionsController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<JsonResponse<Guid>>> Create(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<Requisition>>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<JsonResponse<IEnumerable<Requisition>>>> Search(int? number, string? title, Status? status, Guid? supplierId, Guid? departmentId, Guid? requesterId, DateTime? dueDate, CancellationToken cancellationToken = default)
    {
        var appRequest = new SearchRequest
        {
            Number = number,
            Title = title,
            Status = status,
            SupplierId = supplierId,
            DepartmentId = departmentId,
            RequesterId = requesterId,
            DueDate = dueDate
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/due-date")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetDueDate(Guid id, Dtos.SetDueDateRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetDueDateRequest
        {
            Id = id,
            DueDate = request.DueDate
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/department")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetDepartment(Guid id, Dtos.SetDepartmentRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetDepartmentRequest
        {
            Id = id,
            DepartmentId = request.DepartmentId
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/supplier")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetSupplier(Guid id, Dtos.SetSupplierRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetSupplierRequest
        {
            Id = id,
            SupplierId = request.SupplierId
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }

    [HttpPatch("{id:guid}/title")]
    public async Task<ActionResult<JsonResponse<Unit>>> SetTitle(Guid id, Dtos.SetTitleRequest request, CancellationToken cancellationToken = default)
    {
        var appRequest = new SetTitleRequest
        {
            Id = id,
            Title = request.Title
        };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonResponse(response);
    }
}