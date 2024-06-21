using Persistence.EntityFramework.Configuration;

namespace WebApi.Configuration.Settings;

internal record InfraSettings
{
    public required PersistenceSettings Persistence { get; init; }
}