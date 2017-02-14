using DotNetLive.Account.Entities;
using DotNetLive.Account.Services;
using DotNetLive.AccountApi.Models.AccountModels;
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

        public UserController(UserQueryService userQueryService, UserCommandService userCommandService)
        {
            this._userQueryService = userQueryService;
            this._userCommandService = userCommandService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("search")]
        public IEnumerable<SysUser> GetUserList()
        {
            return _userQueryService.SearchUser();
        }

        /// <summary>
        /// 获取单个用户的信息
        /// </summary>
        /// <param name="userSysId"></param>
        /// <returns></returns>
        [HttpGet, Route("get")]
        //[Authorize("readAccess")]
        public SysUser GetUserInfo([FromQuery]Guid userSysId)
        {
            return _userQueryService.GetUserById(userSysId);
        }

        [HttpPost, Route("create")]
        public Guid CreateUser([FromBody]SysUser user)
        {
            if (user == null)
                throw new Exception("新创建的用户不能为空");
            return _userCommandService.CreateUser(user);
        }

        [HttpPut, Route("update")]
        public void UpdateUser([FromBody]SysUser user)
        {
            _userCommandService.UpdateUser(user);
        }

        [HttpDelete, Route("delete")]
        public void DeleteUser([FromQuery]Guid userSysId)
        {
            _userCommandService.DeleteUser(userSysId);
        }
    }
}
