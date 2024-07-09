using Persistence.EntityFramework.Configuration;

namespace DependencyInjection.Configuration.Settings;

public sealed record InfraSettings
{
    public required PersistenceSettings Persistence { get; init; }
}