using Application.Authorization.Common.Authorizers;
using Application.Authorization.Common.Handlers;
using Application.Authorization.Common.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Authorization.Configuration;

internal static class ServiceCollectionExtensions
{
    private static readonly Assembly _currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

    internal static void AddAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationRequirementHandlers();

        services.AddAuthorizers();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
    }

    private static void AddAuthorizationRequirementHandlers(this IServiceCollection services)
    {
        var handlerInterfaceType = typeof(IAuthorizationRequirementHandler<>);

        var specificHandlers = _currentAssembly.DefinedTypes.Where(type =>
            type.IsClass
            && !type.IsAbstract
            && type != handlerInterfaceType
            && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
        );

        foreach (var specificHandler in specificHandlers)
        {
            var implementedInterfaces = specificHandler.ImplementedInterfaces
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType);

            foreach (var implementedInterface in implementedInterfaces)
                services.AddTransient(implementedInterface, specificHandler);
        }
    }

    private static void AddAuthorizers(this IServiceCollection services)
    {
        var authorizerInterfaceType = typeof(IAuthorizer<>);

        var specificAuthorizers = _currentAssembly.DefinedTypes.Where(type =>
            type.IsClass
            && !type.IsAbstract
            && type != authorizerInterfaceType
            && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == authorizerInterfaceType)
        );

        foreach (var specificAuthorizer in specificAuthorizers)
        {
            var implementedInterfaces = specificAuthorizer.ImplementedInterfaces
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == authorizerInterfaceType);

            foreach (var implementedInterface in implementedInterfaces)
            {
                var authorizerContract = implementedInterface.ContainsGenericParameters ? authorizerInterfaceType : implementedInterface;

                services.AddScoped(authorizerContract, specificAuthorizer);
            }
        }
    }
}