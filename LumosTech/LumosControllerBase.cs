using AutoMapper;
using Lumos.Application;
using Lumos.Application.Configurations;
using Lumos.Data.Models.Management;
using Lumos.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Mvc
{
    public class LumosControllerBase<TEntity, TDto> : Controller where TEntity : class where TDto : class
    {
        private readonly LumosSession _session;
        protected readonly IMapper _mapper;
        protected readonly LumosAppServiceBase<TEntity> _appService;

        public LumosControllerBase(LumosSession session, IMapper mapper, LumosAppServiceBase<TEntity> appService)
        {
            _session = session;
            _mapper = mapper;
            _appService = appService;
        }

        protected void SetViewBagValues()
        {
            ViewBag.UserName = _session.UserName?.ToString().ToUpper();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllPaginated([FromBody] UserDataTableParams dataTableParams)
        {
            if (dataTableParams == null)
            {
                return BadRequest("Parâmetros inválidos.");
            }

            var result = await _appService.GetAllPaginatedAsync(dataTableParams);

            var dtos = _mapper.Map<List<TDto>>(result.Entities);

            var data = new
            {
                draw = dataTableParams.Draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = dtos
            };

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TDto model)
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
                var entity = _mapper.Map<TEntity>(model);
                await _appService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar os dados! Contate o suporte.");
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TDto model)
        {

        }
    }
}
