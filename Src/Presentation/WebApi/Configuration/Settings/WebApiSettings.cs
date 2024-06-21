namespace WebApi.Configuration.Settings;

internal record WebApiSettings
{
    public required IEnumerable<string> AllowedOrigins { get; init; }
}