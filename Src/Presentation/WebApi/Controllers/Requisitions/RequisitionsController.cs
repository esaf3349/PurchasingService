using Application.Requests.Requisitions.Create;
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
}