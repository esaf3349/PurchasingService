using WebApi.Configuration;
using DependencyInjection;
using DependencyInjection.Configuration;

var builder = WebApplication.CreateBuilder(args);

var layeredSettings = builder.Configuration.GetLayeredSettings();
builder.Services.AddLayers(layeredSettings);

var webApiSettings = builder.Configuration.GetWebApiSettings();
builder.Services.AddWebApi(webApiSettings);

var app = builder.Build();

app.UseWebApi(webApiSettings);

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    serviceScope.RunStartupActions(layeredSettings.Infra);

app.Run();