using AutoMapper;
using Lumos.Application;
using Lumos.Application.Configurations;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Mvc.Controllers
{
    public class UsersController : LumosControllerBase<Users, UsersDto, long>
    {
        private readonly IUnitsAppService _unitsAppServices;
        private readonly IUsersAppService _usersAppServices;

        public UsersController(LumosSession session, IMapper mapper, IUnitsAppService UnitsAppService, IUsersAppService usersAppServices, LumosAppServiceBase<Users> userService) : base(session, mapper, userService)
        {
            _unitsAppServices = UnitsAppService;
            _usersAppServices = usersAppServices;
        }

        public override async Task<ActionResult> EditModal(long id)
        {
            var entity = await _appService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<UsersDto>(entity);
            dto.DecryptPassword();
            return PartialView("_EditModal", dto);
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

                if (!string.IsNullOrEmpty(model.SerializedUnitsList))
                {
                    var unitsId = JsonConvert.DeserializeObject<List<long>>(model.SerializedUnitsList);
                    if (unitsId?.Count > 0)
                    {
                        var units = await _unitsAppServices.GetByListIdsAsync(unitsId);
                        if (units?.Any() == true)
                        {
                            entity.Units.AddRange(units);
                        }
                    }
                }

                var encryptionService = new AesEncryptionService();

                entity.Password = encryptionService.Encrypt(entity.Password);
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
