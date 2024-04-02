namespace Lumos.Data.Models.Management
{
    public class LumosSession
    {
        private long? _userId;
        private long? _tenantId;
        private long? _organizationId;
        private string _userName;
        private bool _isHost;

        public long? UserId => _userId;
        public long? TenantId => _tenantId;
        public long? OrganizationId => _organizationId;
        public string UserName => _userName;
        public bool IsHost => _isHost;
        public bool IsAuthenticated => _userId.HasValue || _isHost;

        public void SetUserAndTenant(long? userId, long? tenantId, string userName, long? organizationId)
        {
            _userId = userId;
            _tenantId = tenantId;
            _organizationId = organizationId;
            _userName = userName;
            _isHost = false;
        }

        public void SetHostMode()
        {
            _userId = null;
            _tenantId = null;
            _organizationId = null;
            _isHost = true;
            _userName = "ADMIN - HOST";
        }

        public void Clear()
        {
            _userId = null;
            _tenantId = null;
            _userName = null;
            _isHost = false;
            _organizationId = null;
        }

        public bool IsInHostMode()
        {
            return _isHost;
        }
    }
}
