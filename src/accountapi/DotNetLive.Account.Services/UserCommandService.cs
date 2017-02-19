using DotNetLive.Account.Entities;
using DotNetLive.Framework.Data;
using DotNetLive.Framework.Data.Repositories;
using System;

namespace DotNetLive.Account.Services
{
    public class UserCommandService
    {
        private ICommandRepository _commandRepository;

        public UserCommandService(ICommandRepository commandRepository)
        {
            this._commandRepository = commandRepository;
        }

        public Guid CreateUser(SysUser user)
        {
            _commandRepository.Add(user);
            return user.SysId;
        }

        public void UpdateUser(SysUser user)
        {
            _commandRepository.Update(user);
        }

        public void DeleteUser(Guid userSysId)
        {
            _commandRepository.Delete<SysUser>(userSysId);
        }

        public void ResetPassword(Guid userSysId, string newPasswordHash)
        {
            _commandRepository.Execute($"update {EntityMapper<SysUser>.GetTableName()} set passwordhash = @newPasswordHash where sysid = @userSysId", new { userSysId, newPasswordHash });
        }
    }
}
