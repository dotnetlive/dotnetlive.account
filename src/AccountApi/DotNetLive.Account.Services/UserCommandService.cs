using DotNetLive.Account.Entities;
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

        public Guid CreateUser(User user)
        {
            _commandRepository.Add(user);
            return user.SysId;
        }
    }
}
