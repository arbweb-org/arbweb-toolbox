using arbweb_toolbox_api.Models.v1;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder().AddJsonFile("app_custom_settings.json").Build();
_c_api_weather.g_key = config["API_Keys:Weather"];

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();