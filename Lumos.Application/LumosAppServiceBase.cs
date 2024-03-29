﻿using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;

namespace Lumos.Application
{
    public interface ITransientDependency
    {
    }

    public abstract class LumosAppServiceBase<TEntity> : ITransientDependency where TEntity : class
    {
        protected readonly LumosSession _session;
        protected readonly IRepository<TEntity> _repository;

        public LumosAppServiceBase(LumosSession session, IRepository<TEntity> repository)
        {
            _session = session;
            _repository = repository;
        }

        public Task<TEntity> GetAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<PaginationResult<TEntity>> GetAllPaginatedAsync(UserDataTableParams dataTableParams)
        {
            return await _repository.GetAllPaginatedAsync(dataTableParams);
        }

        public Task CreateAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}


/*
 public class ProductAppService : LumosAppServiceBase<Product>
{
    public ProductAppService(LumosSession session, IRepository<Product> repository) : base(session, repository)
    {
    }

    // Métodos específicos do ProductAppService, se necessário
}
 
 */