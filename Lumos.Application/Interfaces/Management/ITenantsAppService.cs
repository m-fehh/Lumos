﻿using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface ITenantsAppService : ITransientDependency
    {
        Task<List<Tenants>> GetAllAsync();
        Task<TId> InsertAndGetIdAsync<TId>(Tenants entity);
        Task<Tenants> GetByIdAsync<TId>(TId id);
    }
}
