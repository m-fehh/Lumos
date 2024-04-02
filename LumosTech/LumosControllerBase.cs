using AutoMapper;
using Lumos.Application;
using Lumos.Application.Configurations;
using Lumos.Application.Dtos.Management.Tenant;
using Lumos.Data.Models.Management;
using Lumos.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Lumos.Mvc
{
    public class LumosControllerBase<TEntity, TDto, TEntityId> : Controller where TEntity : class where TDto : class
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
            ViewBag.IsHost = IsInHostMode();
        }

        protected bool IsInHostMode()
        {
            return _session.IsInHostMode();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetAllPaginated([FromBody] UserDataTableParams dataTableParams)
        {
            if (dataTableParams == null)
            {
                return BadRequest("Parâmetros inválidos.");
            }

            var tenantId = RequiresTenantOrganizationFilter<TDto>() ? _session.TenantId ?? null : null;
            var organizationId = RequiresTenantOrganizationFilter<TDto>() ? _session.OrganizationId ?? null : null;

            var result = await _appService.GetAllPaginatedAsync(dataTableParams, tenantId, organizationId, _session.IsHost);

            var dtos = _mapper.Map<List<TDto>>(result.Entities);

            var data = new
            {
                draw = dataTableParams.Draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = dtos
            };

            // Criar as configurações de serialização
            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Serializar o objeto usando as configurações criadas
            var json = JsonConvert.SerializeObject(data, serializerSettings);

            return Content(json, "application/json");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertAsync(TDto model)
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
                //// Verificar se os dados já existem antes de inserir
                //var exists = await EntityExistsAsync(e => IsDuplicate(e, model));
                //if (exists)
                //{
                //    ModelState.AddModelError(string.Empty, "Os dados já existem no banco de dados.");
                //    return BadRequest(ModelState);
                //}


                var entity = _mapper.Map<TEntity>(model);

                var tenantIdProperty = typeof(TDto).GetProperty("TenantId");
                if (tenantIdProperty != null)
                {
                    var tenantIdValue = tenantIdProperty.GetValue(model);

                    if (tenantIdValue == null)
                    {
                        long tenantId = _session.TenantId.GetValueOrDefault();
                        typeof(TEntity).GetProperty("TenantId")?.SetValue(entity, tenantId);
                    }
                    else
                    {
                        typeof(TEntity).GetProperty("TenantId")?.SetValue(entity, tenantIdValue);
                    }
                }

                var organizationIdProperty = typeof(TDto).GetProperty("OrganizationId");
                if (organizationIdProperty != null)
                {
                    var organizationIdValue = organizationIdProperty.GetValue(model);
                    if (organizationIdValue == null)
                    {
                        long organizationId = _session.OrganizationId.GetValueOrDefault();
                        typeof(TEntity).GetProperty("OrganizationId")?.SetValue(entity, organizationId);
                    }
                    else
                    {
                        typeof(TEntity).GetProperty("OrganizationId")?.SetValue(entity, organizationIdValue);
                    }
                }

                await _appService.CreateAsync(entity);

                var controllerName = typeof(TEntity).Name.Replace("Entity", string.Empty);
                var response = new
                {
                    redirectTo = Url.Action("Index", controllerName)
                };

                // Retorna a resposta JSON
                return Ok(response);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar os dados! Contate o suporte.");
                return BadRequest(ModelState);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAsync<TId>(TId id, TDto model)
        //{
            //if (model == null)
            //{
                //return BadRequest("Modelo inválido.");
            //}

            //var validationResults = new List<ValidationResult>();
            //var isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

            //if (!isValid)
            //{
                //foreach (var validationResult in validationResults)
                //{
                    //ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
                //}

                //return BadRequest(ModelState);
            //}

            //try
            //{
                //var entity = await _appService.GetByIdAsync(id);
                //if (entity == null)
                //{
                    //return NotFound();
                //}

                //_mapper.Map(model, entity);

                //await _appService.UpdateAsync(entity);

                //return Ok();
            //}
            //catch (Exception)
            //{
                //ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar os dados! Contate o suporte.");
                //return BadRequest(ModelState);
            //}
        //}

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(TEntityId id)
        {
            try
            {
                var entity = await _appService.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                var isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");
                if (isDeletedProperty != null)
                {
                    isDeletedProperty.SetValue(entity, true);
                    await _appService.UpdateAsync(entity);
                }
                else
                {
                    await _appService.DeleteAsync(entity);
                }

                return Ok();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir os dados! Contate o suporte.");
                return BadRequest(ModelState);
            }
        }



        #region PRIVATE METHODS 

        private bool IsDuplicate(TEntity entity, TDto dto)
        {
            // Obtém as propriedades da entidade e da DTO
            PropertyInfo[] entityProperties = typeof(TEntity).GetProperties();
            PropertyInfo[] dtoProperties = typeof(TDto).GetProperties();

            // Lista de nomes das propriedades a serem excluídas da comparação
            List<string> excludedProperties = new List<string> { "TenantId", "OrganizationId", "IsDeleted" };

            foreach (var entityProperty in entityProperties)
            {
                if (!excludedProperties.Contains(entityProperty.Name))
                {
                    var entityValue = entityProperty.GetValue(entity);
                    var dtoProperty = dtoProperties.FirstOrDefault(p => p.Name == entityProperty.Name);
                    if (dtoProperty != null)
                    {
                        var dtoValue = dtoProperty.GetValue(dto);

                        if (entityValue != null && dtoValue != null && entityValue.Equals(dtoValue))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        private async Task<bool> EntityExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _appService.GetAllAsync();
            return entities.Any(predicate.Compile());
        }

        private bool RequiresTenantOrganizationFilter<T>()
        {
            var classesWithoutFilter = new List<Type> {
                typeof(TenantDto)
            };

            var result = !classesWithoutFilter.Contains(typeof(T));
            return result;
        }

        #endregion
    }
}
