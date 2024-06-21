namespace WebApi.Configuration.Settings;

internal record SettingsRoot
{
    public required InfraSettings Infra { get; init; }
    public required PresentationSettings Presentation { get; init; }
}