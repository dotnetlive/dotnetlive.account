using AutoMapper;
using DotNetLive.Account.Entities;
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

    public class UserModel
    {
        public Guid SysId { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsLockoutEnabled { get; set; }
        public DateTime LockoutEndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SysUser, UserModel>();

            CreateMap<UserCreateModel, SysUser>();

            CreateMap<UserUpdateModel, SysUser>();
        }
    }
}
