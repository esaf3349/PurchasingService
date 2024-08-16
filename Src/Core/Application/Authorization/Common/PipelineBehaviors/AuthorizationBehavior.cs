using Application.Authorization.Common.Authorizers;
using Application.Authorization.Common.Handlers;
using Application.Authorization.Common.Requirements;
using Application.Exceptions;
using MediatR;

namespace Application.Authorization.Common.PipelineBehaviors;

internal sealed class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;
    private readonly IServiceProvider _serviceProvider;

    public AuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers, IServiceProvider serviceProvider)
    {
        _authorizers = authorizers;
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        foreach (var authorizer in _authorizers)
        {
            authorizer.BuildPolicy(request);

            foreach (var requirement in authorizer.Requirements)
            {
                var result = await ExecuteHandler(requirement, cancellationToken);

                if (!result.IsSuccess)
                    throw new ForbiddenException(result.Message!);
            }
        }

        return await next();
    }

    private Task<AuthorizationResult> ExecuteHandler(IAuthorizationRequirement requirement, CancellationToken cancellationToken)
    {
        var requirementType = requirement.GetType();
        var handlerType = typeof(IAuthorizationRequirementHandler<>).MakeGenericType(requirementType);

        var specificHandlers = _serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType)) as IEnumerable<object>;

        var specificHandler = specificHandlers!.First();
        var specificHandlerType = specificHandler.GetType();

        var handleMethod = specificHandlerType.GetMethods().Where(x => x.Name == nameof(IAuthorizationRequirementHandler<IAuthorizationRequirement>.Handle)).First();

        var result = (Task<AuthorizationResult>)handleMethod.Invoke(specificHandler, new object[] { requirement, cancellationToken }/*[requirement, cancellationToken]*/)!;

        return result;
    }
}