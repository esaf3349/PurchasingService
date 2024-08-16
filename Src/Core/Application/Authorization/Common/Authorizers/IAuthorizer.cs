using Application.Authorization.Common.Requirements;
using MediatR;

namespace Application.Authorization.Common.Authorizers;

internal interface IAuthorizer<TRequest> where TRequest : IBaseRequest
{
    void BuildPolicy(TRequest request);

    IReadOnlyCollection<IAuthorizationRequirement> Requirements { get; }
}