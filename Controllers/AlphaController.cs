using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace ConfigTest.Controllers
{
    public class AlphaController : Controller
    {
        [FeatureGate(FeatureFlags.Alpha)]
        public IActionResult Index()
        {
            return Content("Text from Alpha Controller", "text/plain");
        }
    }
}
