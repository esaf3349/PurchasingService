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
            InfraSettings = infraSettings,
            PresentationSettings = presentationSettings
        };

        return settingsRoot;
    }

    private static InfraSettings GetInfraSettings(IConfigurationSection section)
    {
        var infraSettings = new InfraSettings { };

        return infraSettings;
    }

    private static PresentationSettings GetPresentationSettings(IConfigurationSection section)
    {
        var presentationSettings = new PresentationSettings { };

        return presentationSettings;
    }
}