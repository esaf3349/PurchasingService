namespace WebApi.Configuration.Settings;

internal readonly record struct PresentationSettings
{
    public required WebApiSettings WebApi { get; init; }
}