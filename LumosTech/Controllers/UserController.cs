using AutoMapper;
using Lumos.Application;
using Lumos.Application.Dtos;
using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Mvc.Controllers
{
    public class UserController : LumosControllerBase<User, UserDto>
    {
        private readonly IUserAppService _userAppService;

        public UserController(LumosSession session, IUserAppService userAppService, IMapper mapper, LumosAppServiceBase<User> userService)
            : base(session, mapper, userService)
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

        //public IActionResult GetAllUsers(int page, int pageSize)
        //{
        //    //var users = _userAppService.GetAllPaginatedAsync(page, pageSize);

        //    var users = new List<User>
        //    {
        //        new User { FullName = "João Silva", Cpf = "123.456.789-00", Email = "joao@example.com" },
        //        new User { FullName = "Maria Souza", Cpf = "987.654.321-00", Email = "maria@example.com" }
        //    };

        //    return Json(new { data = users });
        //}

        //    public IActionResult GetAllUsers(int draw, int start, int length, int page, int pageSize)
        //    {
        //        // Simula uma busca paginada (substitua isso pela lógica real de busca paginada)
        //        var totalRecords = 100; // Total de registros no banco de dados
        //        var users = new List<User>
        //{
        //    new User { FullName = "João Silva", Cpf = "123.456.789-00", Email = "joao@example.com" },
        //    new User { FullName = "Maria Souza", Cpf = "987.654.321-00", Email = "maria@example.com" }
        //};

        //        var filteredUsers = users.Skip(start).Take(length).ToList();

        //        return Json(new
        //        {
        //            draw = draw,
        //            recordsTotal = totalRecords,
        //            recordsFiltered = totalRecords, // Se não houver filtro, é o mesmo que recordsTotal
        //            data = filteredUsers
        //        });
        //    }

        //[HttpPost]
        //public override IActionResult GetUserData([FromBody] UserDataTableParams dataTableParams)
        //{
        //    // Dados fictícios para exemplo
        //    var users = new List<object>
        //    {
        //        new { Id = 1, Nome = "João", CPF = "123.456.789-00", Email = "joao@example.com" },
        //        new { Id = 2, Nome = "Maria", CPF = "987.654.321-00", Email = "maria@example.com" }
        //    };

        //    // Definindo a estrutura de dados esperada pela DataTable
        //    var data = new
        //    {
        //        draw = Request.Form["draw"],
        //        recordsTotal = users.Count,
        //        recordsFiltered = users.Count,
        //        data = users
        //    };

        //    //return Json(data);
        //    return Content(JsonConvert.SerializeObject(data), "application/json");
        //}


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
