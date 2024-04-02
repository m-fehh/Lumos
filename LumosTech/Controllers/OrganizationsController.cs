using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace Lumos.Mvc.Controllers
{
    public class OrganizationsController : LumosControllerBase<Organizations, OrganizationDto, long>
    {
        private readonly ITenantsAppService _tenantAppServices;

        public OrganizationsController(LumosSession session, IMapper mapper, ITenantsAppService tenantAppServices, LumosAppServiceBase<Organizations> organizationService) : base(session, mapper, organizationService)
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

            return View(new OrganizationDto());
        }
    }
}
