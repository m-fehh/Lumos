using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc.Controllers
{
    public class UsersController : LumosControllerBase<Users, UserDto>
    {
        public UsersController(LumosSession session, IMapper mapper, LumosAppServiceBase<Users> userService) : base(session, mapper, userService) { }

        public IActionResult Index()
        {
            SetViewBagValues();
            return View();
        }


        public IActionResult Create()
        {
            SetViewBagValues();
            return View(new UserDto());
        }


        //[HttpPost]
        //public override IActionResult GetUserData([FromBody] UserDataTableParams dataTableParams)
        //{
        //    // Dados fictícios para exemplo
        //    var users = new List<object>
        //    {
        //        new { Id = 1, Nome = "João", CPF = "123.456.789-00", Email = "joao@example.com" },
        //        new { Id = 2, Nome = "Maria", CPF = "987.654.321-00", Email = "maria@example.com" }
        //    };

        //    // Definindo a estrutura de dados esperada pela DataTable
        //    var data = new
        //    {
        //        draw = Request.Form["draw"],
        //        recordsTotal = users.Count,
        //        recordsFiltered = users.Count,
        //        data = users
        //    };

        //    //return Json(data);
        //    return Content(JsonConvert.SerializeObject(data), "application/json");
        //}

    }
}
