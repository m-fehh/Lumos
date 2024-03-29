using AutoMapper;
using Lumos.Data.Models.Management;
using Lumos.Mvc;
using LumosTech.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LumosTech.Controllers
{
    public class HomeController : LumosControllerBase
    {
        public HomeController(LumosSession session, IMapper mapper)
            : base(session, mapper)
        {
        }

        public IActionResult Index()
        {
            SetViewBagValues();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}