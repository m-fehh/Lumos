using AutoMapper;
using Lumos.Application.Dtos;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Dtos.Management.Tenants;
using Lumos.Data.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Configurations
{
    public static class Mapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Users, UsersDto>().ReverseMap();
            configuration.CreateMap<Tenants, TenantsDto>().ReverseMap();
            configuration.CreateMap<Units, UnitsDto>().ReverseMap();
        }
    }
}
