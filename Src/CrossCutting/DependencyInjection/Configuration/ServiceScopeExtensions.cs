using DependencyInjection.Configuration.Settings;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Configuration;

namespace DependencyInjection.Configuration;

public static class ServiceScopeExtensions
{
    public static void RunStartupActions(this IServiceScope serviceScope, InfraSettings infraSettings)
    {
        if (!infraSettings.Persistence.UseInMemoryContext)
            serviceScope.ApplyDbMigrations();
    }
}