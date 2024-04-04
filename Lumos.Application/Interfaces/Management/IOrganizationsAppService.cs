﻿using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IOrganizationsAppService : ITransientDependency
    {
        Task CreateAsync(Organizations entity);
    }
}
