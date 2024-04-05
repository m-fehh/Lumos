using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc.Controllers
{
    public class UnitsController : LumosControllerBase<Units, UnitsDto, long>
    {
        private readonly ITenantsAppService _tenantAppServices;

        public UnitsController(LumosSession session, IMapper mapper, ITenantsAppService tenantAppServices, LumosAppServiceBase<Units> Unitservice) : base(session, mapper, Unitservice)
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
            return View(new UnitsDto());
        }
    }
}
