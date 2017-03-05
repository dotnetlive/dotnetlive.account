using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;

namespace DotNetLive.AccountApi
{
    public class TokenSettings
    {
        /// <summary>
        ///  The Issuer (iss) claim for generated tokens.
        /// </summary>
        public string Issuer { get; set; } = "DotNetLive";

        /// <summary>
        /// The Audience (aud) claim for the generated tokens.
        /// </summary>
        public string Audience { get; set; } = "DotNetLive Users";

        /// <summary>
        /// The expiration time for the generated tokens.
        /// </summary>
        /// <remarks>The default is five minutes (300 seconds).</remarks>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
        public string SigningKey { get; set; } = "mysupersecret_secretkey!123";

        public SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
        }

        public SigningCredentials GetSigningCredentials()
        {
             return new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256);
        }
    }
}
