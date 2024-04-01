using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc.Controllers
{
    public class UsersController : LumosControllerBase<Users, UserDto, long>
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
    }
}
