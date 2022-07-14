var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddAzureAppConfiguration(config =>
{
    config.Connect(builder.Configuration.GetConnectionString("AppConfig")).UseFeatureFlags();
});

builder.Services.AddAzureAppConfiguration();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAzureAppConfiguration();

app.Run();