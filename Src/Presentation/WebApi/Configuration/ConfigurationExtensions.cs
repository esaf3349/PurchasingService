using DependencyInjection.Configuration.Settings;
using Persistence.EntityFramework.Configuration;
using WebApi.Configuration.Settings;

namespace WebApi.Configuration;

internal static class ConfigurationExtensions
{
    public static SettingsRoot GetLayeredSettings(this IConfiguration config)
    {
        var applicationSection = config.GetSection("Application");
        var applicationSettings = applicationSection.Get<ApplicationSettings>();

        var infraSection = config.GetRequiredSection("Infra");
        var infraSettings = GetInfraSettings(infraSection);

        var settingsRoot = new SettingsRoot
        {
            Application = applicationSettings,
            Infra = infraSettings
        };

        return settingsRoot;
    }

    public static WebApiSettings GetWebApiSettings(this IConfiguration config)
    {
        var webApiSection = config.GetRequiredSection("WebApi");
        var webApiSettings = webApiSection.Get<WebApiSettings>();

        return webApiSettings;
    }

    private static InfraSettings GetInfraSettings(IConfigurationSection infraSection)
    {
        var persistenceSettings = infraSection.GetRequiredSection("Persistence").Get<PersistenceSettings>();

        var infraSettings = new InfraSettings 
        {
            Persistence = persistenceSettings
        };

        return infraSettings;
    }
}