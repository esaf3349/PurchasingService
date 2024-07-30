using Application.Contracts.Presentation.CurrentUser;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Text.Json.Serialization;
using WebApi.Services.CurrentUser;

namespace WebApi.Configuration;

internal static class ServiceCollectionExtensions
{
    public static void AddWebApi(this IServiceCollection services, WebApiSettings webApiSettings)
    {
        new WebApiSettingsValidator().ValidateAndThrow(webApiSettings);

        var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddHttpContextAccessor();

        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));

        services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        services.AddDistributedMemoryCache();
        services.AddSession();

        services.AddAutoMapper(currentAssembly);

        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}