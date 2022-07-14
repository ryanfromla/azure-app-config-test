using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(config =>
{
    config.Connect(builder.Configuration.GetConnectionString("AppConfig")).UseFeatureFlags();
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAzureAppConfiguration();

app.Run();