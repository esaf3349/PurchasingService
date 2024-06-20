using Application.Contracts.Presentation.CurrentUser;

namespace WebApi.Services.CurrentUser;

internal sealed class CurrentUserService : ICurrentUserService
{
    public string? IdentityName { get; private set; }

    public CurrentUserService(IHttpContextAccessor httpAccessor)
    {
        var identityName = httpAccessor?.HttpContext?.User?.Identity?.Name;

        IdentityName = identityName;
    }
}