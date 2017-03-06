using DotNetLive.Account.Entities;
using DotNetLive.Framework.Data.Repositories;
using System;

namespace DotNetLive.Account.Services
{
    public class UserDeviceCommandService
    {
        private ICommandRepository _commandRepository;

        public UserDeviceCommandService(ICommandRepository commandRepository)
        {
            this._commandRepository = commandRepository;
        }

        public Guid CreateUserDevice(UserDevice userDevice)
        {
            _commandRepository.Add(userDevice);
            return userDevice.SysId;
        }
    }
}
