using DotNetLive.AccountApi.Models;
using DotNetLive.AccountApi.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotNetLive.AccountApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        [HttpGet, Route("{userId}")]
        [Authorize("readAccess")]
        public LoginResult GetUserInfo([FromQuery]Guid userId)
        {
            return new LoginResult();
        }
    }
}
