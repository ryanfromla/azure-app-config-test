using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace ConfigTest.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IFeatureManager _featureManager;

    public HomeController(IConfiguration configuration, IFeatureManager featureManager)
    {
        _configuration = configuration;
        _featureManager = featureManager;
    }

    public async Task<IActionResult> IndexAsync() 
    { 
        if(await _featureManager.IsEnabledAsync(nameof(FeatureFlags.Alpha)))
        {
            return Content(_configuration.GetValue<string>("AlphaConfigValue"), "text/plain"); 
        }

        var source = "code";

        if(await _featureManager.IsEnabledAsync(nameof(FeatureFlags.Beta)))
        {
            source = _configuration.GetValue<string>("BetaConfigValue");
        }

        return Content($"This is text from {source}", "text/plain");
    }
}
