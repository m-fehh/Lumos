namespace Lumos.Application.Dtos.Management.Tenant
{
    public class CreateTenantDto
    {
        public TenantDto Tenant { get; set; }
        public OrganizationDto Organization { get; set; }
    }
}
