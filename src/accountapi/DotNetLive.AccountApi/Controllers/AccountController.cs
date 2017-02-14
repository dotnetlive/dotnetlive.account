using DotNetLive.AccountApi.Models;
using DotNetLive.AccountApi.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotNetLive.AccountApi.Controllers
{
    /// <summary>
    /// 账户管理API
    /// </summary>
    [Produces("application/json")]
    [Route("api/account")]
    [Authorize()]
    [ProducesResponseType(typeof(LoginResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public class AccountController : Controller
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //header:[[token:string[64]]]
        [HttpGet, Route("login"), AllowAnonymous]
        public LoginResult Login([FromQuery]string username, [FromQuery]string passwordHash, [FromHeader]string token)
        {
            return new LoginResult()
            {
                UserName = username,
                PasswordHash = passwordHash,
                Token = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        [HttpGet, Route("logoff")]
        public void Logoff()
        {

        }

        /// <summary>
        /// 验证Token
        /// </summary>
        [HttpGet, Route("tokenvalidate")]
        public void TokenValidate()
        {

        }
    }
}
