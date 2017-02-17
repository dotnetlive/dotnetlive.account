using DotNetLive.Account.Entities;
using DotNetLive.Account.Services;
using DotNetLive.AccountApi.Models;
using DotNetLive.AccountApi.Models.AccountModels;
using DotNetLive.Framework.WebFramework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public AccountController(AccountService accountSrevice,
            UserQueryService userQueryService,
            IOptions<TokenSettings> tokenSettings)
        {
            this.AccountService = accountSrevice;
            this.UserQueryService = userQueryService;
            this.TokenSettings = tokenSettings.Value;
        }

        public AccountService AccountService { get; private set; }
        public UserQueryService UserQueryService { get; private set; }
        public TokenSettings TokenSettings { get; private set; }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //header:[[token:string[64]]]
        [HttpGet, Route("login"), AllowAnonymous]
        public async Task<LoginResult> Login([FromQuery]string username, [FromQuery]string passwordHash, [FromHeader]string token, [FromQuery] bool withBearerPrefix = true)
        {
            var userInfo = UserQueryService.GetUserByEmail(username);
            if (userInfo == null)
            {
                throw new ApiException("User not exists", 500);
            }

            if (!passwordHash.Equals(userInfo.PasswordHash, StringComparison.OrdinalIgnoreCase))
            {
                throw new ApiException("Authentication Fail, Please confirm your username and password", 500);
            }

            return await GenerateToken(userInfo, withBearerPrefix);
        }

        private async Task<LoginResult> GenerateToken(SysUser user, bool withBearerPrefix = true)
        {
            var now = DateTime.UtcNow;

            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.SysId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName));

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSettings.SigningKey));

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: TokenSettings.Issuer,
                audience: TokenSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TokenSettings.Expiration),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var loginResult = new LoginResult
            {
                Token = withBearerPrefix ? "Bearer " : string.Empty + encodedJwt,
                ExpiresIn = TokenSettings.Expiration.TotalSeconds
            };

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


        /// <summary>
        /// Get this datetime as a Unix epoch timestamp (seconds since Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>Seconds since Unix epoch.</returns>
        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}
