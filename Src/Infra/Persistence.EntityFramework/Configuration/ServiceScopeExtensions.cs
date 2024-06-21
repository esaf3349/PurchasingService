using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.EntityFramework.Configuration;

public static class ServiceScopeExtensions
{
    public static void ApplyDbMigrations(this IServiceScope serviceScope)
    {
        using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
        {
            dbContext.Database.Migrate();
        }
    }
}