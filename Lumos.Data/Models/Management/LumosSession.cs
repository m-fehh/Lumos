namespace Lumos.Data.Models.Management
{
    public class LumosSession
    {
        public long? UserId { get; private set; }
        public long? TenantId { get; private set; }
        public bool IsHost { get; private set; }
        public bool IsAuthenticated => UserId.HasValue || IsHost;

        public LumosSession()
        {
            UserId = null;
            TenantId = null;
            IsHost = false;
        }

        public void SetUserId(long? userId)
        {
            UserId = userId;
            IsHost = false; 
        }

        public void SetTenantId(long? tenantId)
        {
            TenantId = tenantId;
            IsHost = false; 
        }

        public void SetHostMode()
        {
            UserId = null;
            TenantId = null;
            IsHost = true;
        }

        public bool IsInHostMode()
        {
            return IsHost;
        }
    }

}
