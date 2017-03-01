using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiuFangJwtAuth.Config;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LiuFangJwtAuth.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        // GET: api/values
        [HttpGet]
        public JsonResult Get(string userName, string pwd)
        {
            var result = StoreUser.CheckUserExist(userName, pwd);
            if (result == false)
                return Json("用户或者密码错误");
            else
            {
                var date = DateTime.UtcNow;
                Claim[] claims = new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, "test"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, date.ToString(), ClaimValueTypes.Integer64),
                };
                JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "LiuFang",
                audience: userName,
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: new Microsoft.IdentityModel.Tokens
                .SigningCredentials
                (new SymmetricSecurityKey(Encoding.ASCII.GetBytes("LiuFangLiuFangLiuFangLiuFangLiuFang")), SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = DateTime.Now.AddHours(12),
                    token_type = "Bearer"
                };
                var userClaims = Config.StoreSignedUser.GetUserClaims(userName);
                Config.StoreSignedUser.AddSignedUser(new SignedUser(encodedJwt, userName, userClaims));
                return Json(response);
            }
        }
    }
}
