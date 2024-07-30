using WebApi.Middleware;

namespace WebApi.Configuration;

internal static class WebApplicationExtensions
{
    public static void UseWebApi(this WebApplication app, WebApiSettings settings)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSession();

        app.UseMiddleware();

        app.UseCors(builder => builder.WithOrigins(settings.AllowedOrigins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }

    private static void UseMiddleware(this WebApplication app)
    {
        app.UseMiddleware<AppExceptionHandlingMiddleware>();
    }
}