using AutoMapper;
using Lumos.Application.Dtos.Management;
using Lumos.Application.Dtos.Management.Tenant;
using Lumos.Data.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Configurations
{
    public static class Mapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Users, UserDto>().ReverseMap();
            configuration.CreateMap<Address, AddressDto>().ReverseMap();

            configuration.CreateMap<Tenants, TenantDto>().ReverseMap();

            configuration.CreateMap<Organizations, OrganizationDto>().ReverseMap();
        }
    }
}
