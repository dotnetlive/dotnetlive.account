using DotNetLive.AccountWeb.Models;
using DotNetLive.AccountWeb.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLive.AccountWeb.ApiControllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        //     /account/login[POST] data:{username:string,passwordHash:string[48]
        //}
        //header:[[token:string[64]]]
        [HttpGet, Route("login")]
        [ProducesResponseType(typeof(LoginResult), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public void Login([FromQuery]string username, [FromQuery]string passwordHash, [FromHeader]string token)
        {

        }
    }
}
