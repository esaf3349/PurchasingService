using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(currentAssembly));

        services.AddAutoMapper(currentAssembly);
    }
}