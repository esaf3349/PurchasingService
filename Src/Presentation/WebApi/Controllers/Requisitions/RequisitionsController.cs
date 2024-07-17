using Application.Requests.Requisitions.Create;
using Application.Requests.Requisitions.GetById;
using Domain.Model.Requisitions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Requisitions;

[ApiController]
[Route("requisitions")]
public sealed class RequisitionsController : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<JsonResponse<Guid>>> Create(CreateRequest request)
    {
        var response = await Mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<Requisition>>> GetById(Guid id)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest);

        return Ok(response);
    }
}