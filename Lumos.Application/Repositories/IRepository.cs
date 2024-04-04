using Lumos.Application.Configurations;
using Lumos.Application.Models;

namespace Lumos.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync<TId>(TId id);
        Task<List<TEntity>> GetAllAsync();
        Task<PaginationResult<TEntity>> GetAllPaginatedAsync(UserDataTableParams dataTableParams, long? tenantId, List<long> organizationId, bool isHost);
        Task AddAsync(TEntity entity);
        Task<TId> InsertAndGetIdAsync<TId>(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync<TId>(TId id);
    }
}
