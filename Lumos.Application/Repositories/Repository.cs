using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data;
using Lumos.Data.Models.Management;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lumos.Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly LumosContext _context;

        public Repository(LumosContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync<TId>(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<PaginationResult<TEntity>> GetAllPaginatedAsync(UserDataTableParams dataTableParams, long? tenantId, long? organizationId, bool isHost)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (isHost || tenantId.HasValue || organizationId.HasValue)
            {
                query = ApplyTenantOrganizationFilters(query, tenantId, organizationId, isHost);

            }

            // Aplicar filtros
            if (!string.IsNullOrEmpty(dataTableParams.Search?.Value))
            {
                string searchTerm = dataTableParams.Search.Value.ToLower();
                var entities = await query.ToListAsync();
                entities = entities.Where(entity => IsPropertyContainsValue(entity, searchTerm)).ToList();
                query = entities.AsQueryable();
            }

            int totalRecords = query.Count();

            if (dataTableParams.Order != null && dataTableParams.Order.Count > 0)
            {
                foreach (var order in dataTableParams.Order)
                {
                    string propertyName = query.ElementType.GetProperties()[order.Column].Name;
                    var property = typeof(TEntity).GetProperty(propertyName);

                    if (property != null)
                    {
                        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
                        Expression propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        LambdaExpression orderByExpression = Expression.Lambda(propertyAccess, parameter);

                        string methodName = order.Dir == "asc" ? "OrderBy" : "OrderByDescending";
                        MethodCallExpression orderByCallExpression  = Expression.Call(typeof(Queryable), methodName,
                            new Type[] { typeof(TEntity), property.PropertyType },
                            query.Expression, Expression.Quote(orderByExpression));

                        query = query.Provider.CreateQuery<TEntity>(orderByCallExpression);
                    }
                }
            }


            // Paginação
            int pageNumber = dataTableParams.Start / dataTableParams.Length + 1;
            int pageSize = dataTableParams.Length;

            List<TEntity> result = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginationResult = new PaginationResult<TEntity>
            {
                Entities = result,
                TotalRecords = totalRecords
            };

            return paginationResult;
        }


        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TId> InsertAndGetIdAsync<TId>(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            var idProperty = typeof(TEntity).GetProperty("Id");
            if (idProperty == null)
            {
                throw new InvalidOperationException("A entidade não possui uma propriedade 'Id' válida para retornar o novo ID.");
            }

            return (TId)Convert.ChangeType(idProperty.GetValue(entity), typeof(TId));
        }


        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<TId>(TId id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        #region  PRIVATE METHODS  
        private bool IsPropertyContainsValue(TEntity entity, string searchTerm)
        {
            var properties = entity.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyValue = propertyInfo.GetValue(entity);
                if (propertyValue != null && propertyValue.ToString().ToLower().Contains(searchTerm))
                {
                    return true;
                }
            }
            return false;
        }

        private IQueryable<TEntity> ApplyTenantOrganizationFilters(IQueryable<TEntity> query, long? tenantId, long? organizationId, bool isHost)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity), "entity");

            if (isHost)
            {
                // Include explicitamente a propriedade de navegação "Tenant" se existir
                if (typeof(TEntity).GetProperty("Tenant") != null)
                {
                    query = query.Include("Tenant");
                }

                // Include explicitamente a propriedade de navegação "Organization" se existir
                if (typeof(TEntity).GetProperty("Organization") != null)
                {
                    query = query.Include("Organization");
                }
            }

            else
            {
                if (tenantId.HasValue)
                {
                    MemberExpression tenantProperty = Expression.Property(param, "TenantId");
                    ConstantExpression tenantValue = Expression.Constant(tenantId);
                    BinaryExpression tenantFilter = Expression.Equal(tenantProperty, tenantValue);
                    Expression<Func<TEntity, bool>> tenantLambda = Expression.Lambda<Func<TEntity, bool>>(tenantFilter, param);
                    query = query.Where(tenantLambda);

                    // Include explicitamente a propriedade de navegação Tenant
                    query = query.Include("Tenant");
                }

                if (organizationId.HasValue)
                {
                    MemberExpression organizationProperty = Expression.Property(param, "OrganizationId");
                    ConstantExpression organizationValue = Expression.Constant(organizationId.Value);
                    BinaryExpression organizationFilter = Expression.Equal(organizationProperty, organizationValue);
                    Expression<Func<TEntity, bool>> organizationLambda = Expression.Lambda<Func<TEntity, bool>>(organizationFilter, param);
                    query = query.Where(organizationLambda);

                    // Include explicitamente a propriedade de navegação Organization
                    query = query.Include("Organization");
                }
            }

            return query;
        }

        #endregion
    }

    public static class ExpressionHelper
    {
        public static Expression<Func<TEntity, object>> GetPropertyExpression<TEntity>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression property = Expression.Property(parameter, propertyName);
            UnaryExpression conversion = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(conversion, parameter);
        }
    }
}
