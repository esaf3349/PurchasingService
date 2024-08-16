using Application.Authorization.Common.Requirements;

namespace Application.Authorization.Common.Handlers;

internal interface IAuthorizationRequirementHandler<TRequirement> where TRequirement : IAuthorizationRequirement
{
    Task<AuthorizationResult> Handle(TRequirement requirement, CancellationToken cancellationToken = default);
}