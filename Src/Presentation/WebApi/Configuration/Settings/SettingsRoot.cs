namespace WebApi.Configuration.Settings;

internal readonly record struct SettingsRoot
{
    public required InfraSettings Infra { get; init; }
    public required PresentationSettings Presentation { get; init; }
}