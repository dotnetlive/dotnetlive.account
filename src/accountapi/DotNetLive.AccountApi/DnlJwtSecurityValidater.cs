using DotNetLive.Account.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using DotNetLive.Account.Entities;

namespace DotNetLive.AccountApi
{
    public class DnlJwtSecurityValidater : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        public string AuthenticationScheme { get; }
        public Func<string, Guid, bool> ExtendedValidateToken { get; private set; }

        public DnlJwtSecurityValidater(string authenticationScheme, Func<string, Guid, bool> estendedValidateTokenExperssion = null) : base()
        {
            AuthenticationScheme = authenticationScheme;
            ExtendedValidateToken = estendedValidateTokenExperssion;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var cp = base.ValidateToken(token, validationParameters, out validatedToken);

            var jwtId = cp.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

            #region Viery User is valid
            Guid userSysId;
            var userSysIdString = cp.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userSysIdString.Value, out userSysId))
            {
                return null;
            }
            if (ExtendedValidateToken != null && !ExtendedValidateToken(jwtId, userSysId))
            {
                return null;
            }
            #endregion

            var identity = new ClaimsIdentity(cp.Claims, AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
