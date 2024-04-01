﻿using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUsersAppService : ITransientDependency
    {
        Task<Users?> ValidateUserCredentials(string email, string password);
        Task<PaginationResult<Users>> GetAllPaginatedAsync(UserDataTableParams dataTableParams, long? tenantId, long? organizationId, bool isHost);
        string HashPassword(string password);
        Task CreateAsync(Users entity);
    }
}
