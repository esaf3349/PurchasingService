using Persistence.EntityFramework.Configuration;
using WebApi.Configuration.Settings;
using WebApi.Middleware;

namespace WebApi.Configuration;

internal static class WebApplicationExtensions
{
    public static void UseWebApi(this WebApplication app, SettingsRoot settings)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSession();

        app.UseMiddleware();

        app.UseCors(builder => builder.WithOrigins(settings.Presentation.WebApi.AllowedOrigins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.RunStartupActions(settings);
    }

    private static void UseMiddleware(this WebApplication app)
    {
        app.UseMiddleware<AppExceptionHandlingMiddleware>();
    }

    private static void RunStartupActions(this WebApplication app, SettingsRoot settings)
    {
        using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            if (!settings.Infra.Persistence.UseInMemoryContext)
                serviceScope.ApplyDbMigrations();
        }
    }
}