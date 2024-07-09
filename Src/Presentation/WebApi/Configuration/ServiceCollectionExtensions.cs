using Application.Contracts.Presentation.CurrentUser;
using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Text.Json.Serialization;
using WebApi.Configuration.Settings;
using WebApi.Services.CurrentUser;

namespace WebApi.Configuration;

internal static class ServiceCollectionExtensions
{
    public static void AddWebApi(this IServiceCollection services, WebApiSettings webApiSettings)
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