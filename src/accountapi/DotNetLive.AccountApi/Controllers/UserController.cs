using AutoMapper;
using DotNetLive.Account.Entities;
using DotNetLive.Account.Services;
using DotNetLive.AccountApi.Models.AccountModels;
using DotNetLive.AccountApi.Models.UserModels;
using DotNetLive.Framework.WebFramework.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetLive.AccountApi.Controllers
{
    /// <summary>
    ///用户API
    /// </summary>
    [Produces("application/json")]
    [Route("api/user")]
    //[Authorize]
    public class UserController : Controller
    {
        private UserQueryService _userQueryService;
        private UserCommandService _userCommandService;
        private IMapper _mapper;

        public UserController(UserQueryService userQueryService,
            UserCommandService userCommandService,
            IMapper mapper)
        {
            this._userQueryService = userQueryService;
            this._userCommandService = userCommandService;
            this._mapper = mapper;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("search")]
        public IEnumerable<UserModel> GetUserList()
        {
            return _mapper.Map<IEnumerable<UserModel>>(_userQueryService.SearchUser());
        }

        /// <summary>
        /// 获取单个用户的信息
        /// </summary>
        /// <param name="userSysId"></param>
        /// <returns></returns>
        [HttpGet, Route("get")]
        //[Authorize("readAccess")]
        public UserModel GetUserInfo([FromQuery]Guid userSysId)
        {
            return _mapper.Map<UserModel>(_userQueryService.GetUserById(userSysId));
        }

        [HttpPost, Route("create")]
        public Guid CreateUser([FromBody]UserCreateModel userCreateModel)
        {
            if (userCreateModel == null)
                throw new Exception("新创建的用户不能为空");
            return _userCommandService.CreateUser(_mapper.Map<SysUser>(userCreateModel));
        }

        [HttpPut, Route("update")]
        public void UpdateUser([FromBody]UserUpdateModel userUpdateModel)
        {
            _userCommandService.UpdateUser(_mapper.Map<SysUser>(userUpdateModel));
        }

        [HttpPost, HttpDelete, Route("delete")]
        public void DeleteUser([FromQuery]Guid userSysId)
        {
            _userCommandService.DeleteUser(userSysId);
        }
    }
}
