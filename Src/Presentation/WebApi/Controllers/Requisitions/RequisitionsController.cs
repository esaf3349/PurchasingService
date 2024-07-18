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
    public async Task<ActionResult<JsonResponse<Guid>>> Create(CreateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return OkJsonReponse(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JsonResponse<Requisition>>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var appRequest = new GetByIdRequest { Id = id };
        var response = await Mediator.Send(appRequest, cancellationToken);

        return OkJsonReponse(response);
    }
}