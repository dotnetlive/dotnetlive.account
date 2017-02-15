using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi.Models.UserModels
{
    public class UserCreateModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class UserUpdateModel : UserCreateModel
    {
        public Guid SysId { get; set; }
    }
}
