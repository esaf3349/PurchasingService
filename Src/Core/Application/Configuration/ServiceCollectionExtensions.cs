using Application.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(currentAssembly));

        services.AddAutoMapper(currentAssembly);

        services.AddPipelineBehaviors();
    }

    private static void AddPipelineBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
    }
}