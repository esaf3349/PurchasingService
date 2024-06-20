namespace WebApi.Configuration.Settings;

internal readonly record struct WebApiSettings
{
    public required IEnumerable<string> AllowedOrigins { get; init; }
}