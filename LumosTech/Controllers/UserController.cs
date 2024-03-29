using AutoMapper;
using Lumos.Application.Dtos;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Mvc.Controllers
{
    public class UserController : LumosControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UserController(LumosSession session, IUserAppService userAppService, IMapper mapper)
            : base(session, mapper)
        {
            _userAppService = userAppService;
        }



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

        public IActionResult GetAllUsers(int page, int pageSize)
        {
            var users = _userAppService.GetAllPaginatedAsync(page, pageSize);
            return Json(new { data = users });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save(UserDto model)
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
                var userEntity = _mapper.Map<User>(model);
                userEntity.PasswordHash = _userAppService.HashPassword(model.PasswordHash);

                await _userAppService.CreateAsync(userEntity);
                return Ok();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o usuário. Por favor, contate o suporte.");
                return BadRequest(ModelState);
            }
        }
    }
}
