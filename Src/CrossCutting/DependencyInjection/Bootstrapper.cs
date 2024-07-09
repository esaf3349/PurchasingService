using Application.Configuration;
using DependencyInjection.Configuration.Settings;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Configuration;

namespace DependencyInjection;

public static class Bootstrapper
{
    public static void AddLayers(this IServiceCollection services, SettingsRoot settings)
    {
        services.AddApplication();

        services.AddInfra(settings.Infra);
    }

    private static void AddInfra(this IServiceCollection services, InfraSettings settings)
    {
        services.AddEfPersistence(settings.Persistence);
    }
}