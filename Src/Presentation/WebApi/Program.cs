using WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.ReadAppSettingsJson();

builder.Services.AddLayers(settings);

var app = builder.Build();

app.UseWebApi(settings);

app.Run();