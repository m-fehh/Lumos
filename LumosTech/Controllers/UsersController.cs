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
    public class UsersController : LumosControllerBase<Users, UsersDto, long>
    {
        private readonly ITenantsAppService _tenantAppServices;
        private readonly IUnitsAppService _UnitsAppServices;
        private readonly IUsersAppService _usersAppServices;

        public UsersController(LumosSession session, IMapper mapper, ITenantsAppService tenantAppServices, IUnitsAppService UnitsAppService, IUsersAppService usersAppServices, LumosAppServiceBase<Users> userService) : base(session, mapper, userService)
        {
            _tenantAppServices = tenantAppServices;
            _UnitsAppServices = UnitsAppService;
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
            return View(new UsersDto());
        }

        [HttpPost]
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        public override async Task<IActionResult> InsertAsync(UsersDto model)
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

                entity.Password = _usersAppServices.HashPassword(entity.Password);
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
