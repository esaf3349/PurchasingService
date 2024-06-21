namespace WebApi.Configuration.Settings;

internal record PresentationSettings
{
    public required WebApiSettings WebApi { get; init; }
}