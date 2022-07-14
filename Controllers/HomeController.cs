using Microsoft.AspNetCore.Mvc;

namespace config_test.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return Content(_configuration.GetValue<string>("ConfigValue"), "text/plain");
    }
}
