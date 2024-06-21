namespace Persistence.EntityFramework.Configuration;

public record PersistenceSettings
{
    public required bool UseInMemoryContext { get; init; }
    public required string DbConnection { get; init; }
}