using AutoMapper;
using Lumos.Application.Dtos;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Configurations
{
    public static class Mapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDto>().ReverseMap();
            configuration.CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
