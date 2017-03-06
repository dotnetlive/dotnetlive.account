using AutoMapper;
using DotNetLive.Account.Entities;
using DotNetLive.AccountApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi.Models
{
    public class ModelsMaping : Profile
    {
        public ModelsMaping()
        {
            CreateMap<SysUser, LoginUser>();
        }
    }
}
