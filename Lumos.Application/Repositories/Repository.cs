﻿using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data;
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

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<PaginationResult<TEntity>> GetAllPaginatedAsync(UserDataTableParams dataTableParams)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

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
                    Expression<Func<TEntity, object>> propertyExpression = ExpressionHelper.GetPropertyExpression<TEntity>(propertyName);
                    query = order.Dir == "asc" ? query.OrderBy(propertyExpression) : query.OrderByDescending(propertyExpression);
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

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

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
