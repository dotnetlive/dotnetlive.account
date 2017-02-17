using DotNetLive.AccountApi.AuthorizationPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi
{
    public partial class Startup
    {
        // The secret key every token will be signed with.
        // Keep this safe on the server!
        private static readonly string secretKey = "mysupersecret_secretkey!123";

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //知识点:Custom Policy-Based Authorization
                //https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies
                options.AddPolicy("Admin", authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });

                options.AddPolicy("readAccess", authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });

                options.AddPolicy("writeAccess", authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });

                options.AddPolicy("Over21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
                options.AddPolicy("BadgeEntry", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                    (c.Type == DotNetLiveClaimTypes.BadgeId || c.Type == DotNetLiveClaimTypes.TemporaryBadgeId)
                    && c.Issuer == "https://microsoftsecurity")
                ));
            });

            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddSingleton<IAuthorizationHandler, HasTemporaryStickerHandler>();
            services.AddSingleton<IAuthorizationHandler, BadgeEntryHandler>();
        }

        private void ConfigureAuthorization(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var tokenSettings = serviceProvider.GetService<IOptions<TokenSettings>>().Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = tokenSettings.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = tokenSettings.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            var options = new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            };

            //这里默认是:System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler, System.IdentityModel.Tokens.Jwt, Version=5.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(new CustomSecurityValidater("DotNetLive Jwt Auth"));
            app.UseJwtBearerAuthentication(options);
        }

        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            // Don't do this in production, obviously!
            if (username == "TEST" && password == "TEST123")
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }

    public class CustomSecurityValidater : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        public string AuthenticationScheme { get; }

        public CustomSecurityValidater(string authenticationScheme) : base()
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

    public class JwtObject
    {
        public Guid SysId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
