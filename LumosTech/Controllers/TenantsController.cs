using AutoMapper;
using Lumos.Application;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Lumos.Application.Interfaces.Management;
using Lumos.Application.Dtos.Management.Tenants;

namespace Lumos.Mvc.Controllers
{
    public class TenantsController : LumosControllerBase<Tenants, TenantsDto, long>
    {
        private readonly IUnitsAppService _UnitsAppService;

        public TenantsController(LumosSession session, IMapper mapper, IUnitsAppService UnitsAppService, LumosAppServiceBase<Tenants> tenantService) : base(session, mapper, tenantService)
        {
            _UnitsAppService = UnitsAppService;
        }


        public override IActionResult Create()
        {
            SetViewBagValues();
            return View(new CreateTenantDto());
        }

        [HttpPost]
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        public async Task<IActionResult> InsertTenantAsync(CreateTenantDto model)
        {
            var validationResults = new List<ValidationResult>();

            var isValidTenant = Validator.TryValidateObject(model.Tenant, new ValidationContext(model.Tenant), validationResults, true);
            var isValidUnit = Validator.TryValidateObject(model.Unit, new ValidationContext(model.Unit), validationResults, true);

            var isValid = isValidTenant && isValidUnit;
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
                var entityTenant = _mapper.Map<Tenants>(model.Tenant);
                var entityUnit = _mapper.Map<Units>(model.Unit);

                var tenantId = await _appService.InsertAndGetIdAsync<long>(entityTenant);
                entityUnit.TenantId = tenantId;

                await _UnitsAppService.CreateAsync(entityUnit);

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
    }
}
