using Lumos.Data.Models.Management;
using LumosTech.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LumosTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly LumosSession _session;

        public HomeController(LumosSession session)
        {
            _session = session;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = _session.UserName?.ToString().ToUpper();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}