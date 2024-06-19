using WebApi.Configuration.Settings;

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

        app.UseMiddleware();

        app.UseCors(builder => builder.WithOrigins()
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

    }

    private static void RunStartupActions(this WebApplication app, SettingsRoot settings)
    {

    }
}