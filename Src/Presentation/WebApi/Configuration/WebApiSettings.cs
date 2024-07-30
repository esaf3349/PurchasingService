namespace WebApi.Configuration;

internal record WebApiSettings
{
    public required IEnumerable<string> AllowedOrigins { get; init; }
}