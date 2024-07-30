using Application.Configuration;

namespace DependencyInjection.Configuration.Settings;

public sealed record SettingsRoot
{
    public required ApplicationSettings Application { get; init; }
    public required InfraSettings Infra { get; init; }
}