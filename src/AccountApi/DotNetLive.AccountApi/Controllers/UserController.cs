using DotNetLive.AccountApi.Models;
using DotNetLive.AccountApi.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetLive.AccountApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    //[Authorize]
    public class UserController : Controller
    {
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

        [HttpGet, Route("{userId}")]
        //[Authorize("readAccess")]
        public LoginResult GetUserInfo([FromQuery]Guid userId)
        {
            return new LoginResult();
        }
    }
}
