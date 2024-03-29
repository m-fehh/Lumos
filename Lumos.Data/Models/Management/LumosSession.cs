namespace Lumos.Data.Models.Management
{
    public class LumosSession
    {
        private long? _userId;
        private long? _tenantId;
        private string _userName;
        private bool _isHost;

        public long? UserId => _userId;
        public long? TenantId => _tenantId;
        public string UserName => _userName;
        public bool IsHost => _isHost;
        public bool IsAuthenticated => _userId.HasValue || _isHost;

        public void SetUserAndTenant(long? userId, long? tenantId, string userName)
        {
            _userId = userId;
            _tenantId = tenantId;
            _userName = userName;
            _isHost = false;
        }

        public void SetHostMode()
        {
            _userId = null;
            _tenantId = null;
            _isHost = true;
            _userName = "ADMIN - HOST";
        }

        public void Clear()
        {
            _userId = null;
            _tenantId = null;
            _userName = null;
            _isHost = false;
        }

        public bool IsInHostMode()
        {
            return _isHost;
        }
    }
}
