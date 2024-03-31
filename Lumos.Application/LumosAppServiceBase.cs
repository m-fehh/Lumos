using Lumos.Application.Configurations;
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

        public async Task<TEntity> GetByIdAsync<TId>(TId id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginationResult<TEntity>> GetAllPaginatedAsync(UserDataTableParams dataTableParams, long? tenantId, long? organizationId)
        {
            return await _repository.GetAllPaginatedAsync(dataTableParams, tenantId, organizationId);
        }

        public Task CreateAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }


        public async Task<TId> InsertAndGetIdAsync<TId>(TEntity entity)
        {
            return await _repository.InsertAndGetIdAsync<TId>(entity);
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


