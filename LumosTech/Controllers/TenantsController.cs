using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc.Controllers
{
    public class TenantsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
