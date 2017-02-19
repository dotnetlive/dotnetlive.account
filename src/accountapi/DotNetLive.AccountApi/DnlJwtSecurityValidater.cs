using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace DotNetLive.AccountApi
{
    public class DnlJwtSecurityValidater : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        public string AuthenticationScheme { get; }

        public DnlJwtSecurityValidater(string authenticationScheme) : base()
        {
            AuthenticationScheme = authenticationScheme;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var cp = base.ValidateToken(token, validationParameters, out validatedToken);

            var jti = cp.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);
            //Do check in Redis is the jti in the appliedTokenList, 
            //Refreshed: 已经申请刷新则直接过期旧的Token
            var identity = new ClaimsIdentity(cp.Claims, AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
