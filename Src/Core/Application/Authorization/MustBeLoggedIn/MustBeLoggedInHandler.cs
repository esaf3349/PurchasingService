using Application.Authorization.Common.Handlers;
using Application.Contracts.Presentation.CurrentUser;

namespace Application.Authorization.MustBeLoggedIn;

internal sealed class MustBeLoggedInHandler : IAuthorizationRequirementHandler<MustBeLoggedInRequirement>
{
    private readonly ICurrentUserService _currentUserService;

    public MustBeLoggedInHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public Task<AuthorizationResult> Handle(MustBeLoggedInRequirement requirement, CancellationToken cancellationToken = default)
    {
        if (_currentUserService.Details == null)
            return Task.FromResult(AuthorizationResult.Error("You must be logged in"));

        return Task.FromResult(AuthorizationResult.Success);
    }
}