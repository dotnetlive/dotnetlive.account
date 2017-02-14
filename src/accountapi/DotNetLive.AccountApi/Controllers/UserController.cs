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

        public UserController(UserQueryService userQueryService)
        {
            this._userQueryService = userQueryService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize("readAccess")]
        public IList<LoginResult> GetUserList()
        {
            var result = new List<LoginResult>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(new LoginResult() { Token = Guid.NewGuid().ToString() });
            }
            return result;
        }

        /// <summary>
        /// 获取单个用户的信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route("{userId}")]
        //[Authorize("readAccess")]
        public LoginResult GetUserInfo([FromQuery]Guid userId)
        {
            return new LoginResult();
        }
    }
}
