using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Mvc.Controllers
{
    public class UsersController : LumosControllerBase<Users, UserDto, long>
    {
        private readonly ITenantsAppService _tenantAppServices;
        private readonly IOrganizationsAppService _organizationsAppServices;
        private readonly IUsersAppService _usersAppServices;

        public UsersController(LumosSession session, IMapper mapper, ITenantsAppService tenantAppServices, IOrganizationsAppService organizationsAppService, IUsersAppService usersAppServices, LumosAppServiceBase<Users> userService) : base(session, mapper, userService)
        {
            _tenantAppServices = tenantAppServices;
            _organizationsAppServices = organizationsAppService;
            _usersAppServices = usersAppServices;
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

        [HttpPost]
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        public override async Task<IActionResult> InsertAsync(UserDto model)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

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
                var entity = _mapper.Map<Users>(model);

                entity.PasswordHash = _usersAppServices.HashPassword(entity.PasswordHash);
                await _usersAppServices.CreateAsync(entity);

                var response = new
                {
                    redirectTo = Url.Action("Index", "Users")
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
