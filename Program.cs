using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(config =>
{
    config.Connect(builder.Configuration.GetConnectionString("AppConfig"))
        //This allows values to be refreshed 
        .ConfigureRefresh(config=> 
        { 
            //default is 30 seconds
            config.SetCacheExpiration(TimeSpan.FromSeconds(1));
            //Specify which config value will trigger a refresh.
            //If flag is true, a change to this value will trigger a refresh for all values
            config.Register("BetaConfigValue", true); 
        })
        //default is 30 seconds
        //Each key is automatically refreshed, no need to register keys
        .UseFeatureFlags(options=> options.CacheExpirationInterval = TimeSpan.FromSeconds(1)); 
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement()
    //Feature has an x% chance to be activated
    .AddFeatureFilter<PercentageFilter>()
    //Feature is activated between x and y
    .AddFeatureFilter<TimeWindowFilter>();
    //Feature is activated for specific users/groups
    //Had issues with this, but I'm really curious about this and would like to get it to work.
    //.AddFeatureFilter<TargetingFilter>();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAzureAppConfiguration();

app.Run();