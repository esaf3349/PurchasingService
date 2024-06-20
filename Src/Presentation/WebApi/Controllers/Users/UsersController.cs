using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Controllers;
using WebApi.Common.Http;

namespace WebApi.Controllers.Users;

[ApiController]
[Route("users")]
public sealed class UsersController : BaseController
{
    [HttpGet("current")]
    public async Task<ActionResult<JsonResponse<string?>>> GetCurrentUser()
    {
        return OkJsonReponse(CurrentUserIdentityName);
    }
}