using DotNetLive.AccountApi.AuthorizationPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

        private void ConfigureAuth(IServiceCollection services)
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

        private void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    AuthenticationScheme = "Cookie",
            //    CookieName = "access_token",
            //    TicketDataFormat = new CustomJwtDataFormat(
            //        SecurityAlgorithms.HmacSha256,
            //        tokenValidationParameters)
            //});
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
}
