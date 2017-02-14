using DotNetLive.Account.Entities;
using DotNetLive.Framework.Data.Repositories;
using System;

namespace DotNetLive.Account.Services
{
    /// <summary>
    /// 用户查询服务
    /// </summary>
    public class UserQueryService
    {
        private IQueryRepository _queryRepository;

        public UserQueryService(IQueryRepository queryRepository)
        {
            this._queryRepository = queryRepository;
        }

        public User GetUserById(Guid userId)
        {
            return _queryRepository.Get<User>(userId);
        }
    }
}
