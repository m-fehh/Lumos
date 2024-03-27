namespace Lumos.Data.Models.Management
{
    public class LumosSession
    {
        public long? UserId { get; private set; }
        public long? TenantId { get; private set; }
        public bool IsAuthenticated => UserId.HasValue;
        public bool IsHost => TenantId.Value > 1 || !TenantId.HasValue;

        public LumosSession() 
        {
            UserId = null;
            TenantId = null;
        }

        public void SetUserId(long userId) 
        {
            UserId = userId;
        }

        public void SetTenantId(long tenantId) 
        { 
            TenantId = tenantId;
        }
    }
}
