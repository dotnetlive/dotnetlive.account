using DotNetLive.Account.Entities;
using DotNetLive.Framework.Data;
using DotNetLive.Framework.Data.Repositories;
using System;
using System.Collections.Generic;

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

        public SysUser GetUserById(Guid userId)
        {
            return _queryRepository.Get<SysUser>(userId);
        }

        public IEnumerable<SysUser> SearchUser()
        {
            return _queryRepository.GetAll<SysUser>();
        }

        public SysUser GetUserByEmail(string username)
        {
            return _queryRepository.Get<SysUser>($"select {EntityMapper<SysUser>.GetColumnNames("t")} from {EntityMapper<SysUser>.GetTableName()} t where t.Email = @email", new { email = username });
        }
    }
}
