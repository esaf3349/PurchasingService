using Persistence.EntityFramework.Configuration;
using WebApi.Configuration.Settings;

namespace WebApi.Configuration;

internal static class ConfigurationExtensions
{
    public static SettingsRoot ReadAppSettingsJson(this IConfiguration config)
    {
        var infraSection = config.GetRequiredSection("Infra");
        var infraSettings = GetInfraSettings(infraSection);

        var presentationSection = config.GetRequiredSection("Presentation");
        var presentationSettings = GetPresentationSettings(presentationSection);

        var settingsRoot = new SettingsRoot
        {
            Infra = infraSettings,
            Presentation = presentationSettings
        };

        return settingsRoot;
    }

    private static InfraSettings GetInfraSettings(IConfigurationSection section)
    {
        var persistenceSettings = section.GetRequiredSection("Persistence").Get<PersistenceSettings>();

        var infraSettings = new InfraSettings 
        {
            Persistence = persistenceSettings
        };

        return infraSettings;
    }

    private static PresentationSettings GetPresentationSettings(IConfigurationSection section)
    {
        var webApiSettings = section.GetRequiredSection("WebApi").Get<WebApiSettings>();

        var presentationSettings = new PresentationSettings 
        {
            WebApi = webApiSettings
        };

        return presentationSettings;
    }
}