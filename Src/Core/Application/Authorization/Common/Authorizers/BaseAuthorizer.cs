using Application.Authorization.Common.Requirements;
using MediatR;

namespace Application.Authorization.Common.Authorizers;

internal abstract class BaseAuthorizer<TRequest> : IAuthorizer<TRequest> where TRequest : IBaseRequest
{
    private HashSet<IAuthorizationRequirement> _requirements { get; } = [];

    public IReadOnlyCollection<IAuthorizationRequirement> Requirements => _requirements;

    protected void UseRequirement(IAuthorizationRequirement requirement)
    {
        _requirements.Add(requirement);
    }

    public abstract void BuildPolicy(TRequest request);
}