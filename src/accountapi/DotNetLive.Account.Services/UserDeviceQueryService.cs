using DotNetLive.Framework.Data.Repositories;

namespace DotNetLive.Account.Services
{
    public class UserDeviceQueryService
    {
        private IQueryRepository _queryRepository;

        public UserDeviceQueryService(IQueryRepository queryRepository)
        {
            this._queryRepository = queryRepository;
        }
    }
}
