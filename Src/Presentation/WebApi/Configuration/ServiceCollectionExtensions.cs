using Application.Configuration;
using Application.Contracts.Presentation.CurrentUser;
using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Text.Json.Serialization;
using WebApi.Configuration.Settings;
using WebApi.Services.CurrentUser;

namespace WebApi.Configuration;

internal static class ServiceCollectionExtensions
{
    public static void AddLayers(this IServiceCollection services, SettingsRoot settings)
    {
        services.AddApplication();

        services.AddInfra(settings.Infra);

        services.AddPresentation(settings.Presentation);
    }

    private static void AddInfra(this IServiceCollection services, InfraSettings infraSettings)
    {
        
    }

    private static void AddPresentation(this IServiceCollection services, PresentationSettings presentationSettings)
    {
        services.AddBackgroundServices(presentationSettings);

        services.AddWebApi(presentationSettings.WebApi);
    }

    private static void AddBackgroundServices(this IServiceCollection services, PresentationSettings presentationSettings)
    {

    }

    private static void AddWebApi(this IServiceCollection services, WebApiSettings webApiSettings)
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

        services.AddDistributedMemoryCache();
        services.AddSession();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}