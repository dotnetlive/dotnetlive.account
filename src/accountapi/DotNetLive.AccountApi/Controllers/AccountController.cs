using AutoMapper;
using DotNetLive.Account.Entities;
using DotNetLive.Account.Services;
using DotNetLive.AccountApi.Models;
using DotNetLive.AccountApi.Models.AccountModels;
using DotNetLive.Framework.WebApi.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public AccountController(IOptions<TokenSettings> tokenSettings,
            AccountService accountSrevice,
            UserQueryService userQueryService,
            UserCommandService userCommandService,
            UserDeviceCommandService userDeviceCommandService,
            IMapper mapper
            )
        {
            this.TokenSettings = tokenSettings.Value;
            this.AccountService = accountSrevice;
            this.UserQueryService = userQueryService;
            this.UserCommandService = userCommandService;
            UserDeviceCommandService = userDeviceCommandService;
            this.Mapper = mapper;
        }

        public AccountService AccountService { get; private set; }
        public UserQueryService UserQueryService { get; private set; }
        public TokenSettings TokenSettings { get; private set; }
        public UserCommandService UserCommandService { get; private set; }
        public UserDeviceCommandService UserDeviceCommandService { get; private set; }
        public IMapper Mapper { get; private set; }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //header:[[token:string[64]]]
        [HttpGet, Route("login"), AllowAnonymous]
        public async Task<LoginResult> Login([FromQuery]string email, [FromQuery]string passwordHash, [FromHeader]string token)
        {
            var userInfo = UserQueryService.GetUserByEmail(email);
            if (userInfo == null)
            {
                throw new ApiException("User not exists", 500);
            }

            if (!string.Equals(passwordHash, userInfo.PasswordHash, StringComparison.OrdinalIgnoreCase))
            {
                throw new ApiException("Authentication Fail, Please confirm your username and password", 500);
            }

            return await GenerateToken(userInfo, true);
        }

        private async Task<LoginResult> GenerateToken(SysUser user, bool withBearerPrefix = true)
        {
            var now = DateTime.UtcNow;
            var userDevice = new UserDevice()
            {
                UserSysId = user.SysId,
                IssueTime = now,
                ExpireTime = now.Add(TokenSettings.Expiration)
            };


            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, userDevice.SysId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Sid, user.SysId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSettings.SigningKey));

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: TokenSettings.Issuer,
                audience: TokenSettings.Audience,
                claims: claims,
                notBefore: userDevice.IssueTime,
                expires: userDevice.ExpireTime,
                signingCredentials: TokenSettings.GetSigningCredentials());

            userDevice.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
            //UserDeviceCommandService.CreateUserDevice(userDevice); //Perisit UserDevice

            var loginResult = new LoginResult
            {
                Token = userDevice.Token,
                ExpiresIn = TokenSettings.Expiration.TotalSeconds,
                LoginUser = Mapper.Map<LoginUser>(user)
            };

            if (withBearerPrefix)
            {
                loginResult.Token = loginResult.Token.Insert(0, "Bearer ");
            }

            return await Task.FromResult(loginResult);
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
        [HttpGet, Route("refreshToken")]
        public async Task<LoginResult> RefreshToken()
        {
            var userInfo = UserQueryService.GetUserByEmail(User.Identity.Name);
            if (userInfo == null)
            {
                throw new ApiException("User not exists", 500);
            }
            return await GenerateToken(userInfo);
        }

        [HttpPut, Route("superuser/resetUserPassword")]
        public async Task AdminResetPassword([FromQuery]Guid userSysId, [FromQuery]string newPasswordHash)
        {
            UserCommandService.ResetPassword(userSysId, newPasswordHash);
            await Task.FromResult(0);
        }

        /// <summary>
        /// 任意用户修改用户密码(近作为开发阶段使用)
        /// </summary>
        /// <param name="userSysId"></param>
        /// <param name="newPasswordHash"></param>
        /// <returns></returns>
        [HttpPut, Route("anonymous/resetUserPassword"), AllowAnonymous]
        public async Task AnonymousResetPassword([FromQuery]Guid userSysId, [FromQuery]string newPasswordHash)
        {
            UserCommandService.ResetPassword(userSysId, newPasswordHash);
            await Task.FromResult(0);
        }


        /// <summary>
        /// Get this datetime as a Unix epoch timestamp (seconds since Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>Seconds since Unix epoch.</returns>
        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}
