using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc.Controllers
{
    public class UsersController : LumosControllerBase<Users, UserDto, long>
    {
        private readonly ITenantsAppService _tenantAppServices;

        public UsersController(LumosSession session, IMapper mapper, ITenantsAppService tenantAppServices, LumosAppServiceBase<Users> userService) : base(session, mapper, userService) 
        {
            _tenantAppServices = tenantAppServices;

        }

        public IActionResult Index()
        {
            SetViewBagValues();
            return View();
        }


        public async Task<IActionResult> Create()
        {
            SetViewBagValues();

            if (IsInHostMode())
            {
                var allTenants = await _tenantAppServices.GetAllAsync();
                ViewBag.Tenants = allTenants;
            }

            return View(new UserDto());
        }
    }
}
