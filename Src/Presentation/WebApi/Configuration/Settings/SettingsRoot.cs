namespace WebApi.Configuration.Settings;

internal readonly record struct SettingsRoot
{
    public required InfraSettings InfraSettings { get; init; }
    public required PresentationSettings PresentationSettings { get; init; }
}