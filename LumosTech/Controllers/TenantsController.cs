using AutoMapper;
using Lumos.Application;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;
using Lumos.Application.Dtos.Management.Tenant;
using System.ComponentModel.DataAnnotations;
using Lumos.Application.Interfaces.Management;

namespace Lumos.Mvc.Controllers
{
    public class TenantsController : LumosControllerBase<Tenants, TenantDto, long>
    {
        private readonly IOrganizationsAppService _organizationsAppService;

        public TenantsController(LumosSession session, IMapper mapper, IOrganizationsAppService organizationsAppService, LumosAppServiceBase<Tenants> tenantService) : base(session, mapper, tenantService)
        {
            _organizationsAppService = organizationsAppService;
        }

        public IActionResult Index()    
        {
            SetViewBagValues();
            return View();
        }

        public IActionResult Create()
        {
            SetViewBagValues();
            return View(new CreateTenantDto());
        }

        [HttpPost]
        public async Task<IActionResult> InsertTenantAsync(CreateTenantDto model)
        {
            var validationResults = new List<ValidationResult>();

            var isValidTenant = Validator.TryValidateObject(model.Tenant, new ValidationContext(model.Tenant), validationResults, true);
            var isValidOrganization = Validator.TryValidateObject(model.Organization, new ValidationContext(model.Organization), validationResults, true);

            var isValid = isValidTenant && isValidOrganization;

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
            try
            {
                var tenant = _mapper.Map<Tenants>(model.Tenant);
                var organization = _mapper.Map<Organizations>(model.Organization);

                var tenantId = await _appService.InsertAndGetIdAsync<long>(tenant);
                organization.TenantId = tenantId;

                await _organizationsAppService.CreateAsync(organization);

                var response = new
                {
                    redirectTo = Url.Action("Index", "Tenants")
                };
                return Ok(response);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar os dados! Contate o suporte.");
                return BadRequest(ModelState);
            }
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteTenantAsync(long id) { }
    }
}
