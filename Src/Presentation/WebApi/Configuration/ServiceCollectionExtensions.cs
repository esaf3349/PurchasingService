using Application.Configuration;
using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Text.Json.Serialization;
using WebApi.Configuration.Settings;

namespace WebApi.Configuration;

internal static class ServiceCollectionExtensions
{
    public static void AddLayers(this IServiceCollection services, SettingsRoot settings)
    {
        services.AddApplication();

        services.AddInfra(settings.InfraSettings);

        services.AddPresentation(settings.PresentationSettings);
    }

    private static void AddInfra(this IServiceCollection services, InfraSettings infraSettings)
    {
        
    }

    private static void AddPresentation(this IServiceCollection services, PresentationSettings settings)
    {
        services.AddBackgroundServices(settings);

        services.AddWebApi();
    }

    private static void AddBackgroundServices(this IServiceCollection services, PresentationSettings settings)
    {

    }

    private static void AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
    }
}