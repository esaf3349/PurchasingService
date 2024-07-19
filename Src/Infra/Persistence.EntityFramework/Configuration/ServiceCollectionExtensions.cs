using Application.Contracts.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Persistence.EntityFramework.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddEfPersistence(this IServiceCollection services, PersistenceSettings settings)
    {
        services.AddSingleton(settings);

        services.AddDbContext<AppDbContext>(builder => ConfigureDbContext(builder, settings));

        services.TryAddScoped<IUnitOfWork, AppDbContext>();
    }

    private static void ConfigureDbContext(DbContextOptionsBuilder builder, PersistenceSettings settings)
    {
        if (settings.UseInMemoryContext)
            builder.UseInMemoryDatabase(AppDbContextConstants.InMemoryDbName);
        else
            builder.UseSqlServer(settings.DbConnection);
    }
}