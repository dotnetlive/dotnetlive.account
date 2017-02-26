using DotNetLive.Account.Services;
using DotNetLive.AccountApi.AuthorizationPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

        private void ConfigureAuthentication(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var tokenSettings = serviceProvider.GetService<IOptions<TokenSettings>>().Value;

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = tokenSettings.GetSecurityKey(),

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
            var tokenValidater = new DnlJwtSecurityValidater("DotNetLive Jwt Auth", (jwtId, userSysId) =>
                {
                    var userService = serviceProvider.GetService<UserQueryService>();
                    var user = userService.GetUserById(userSysId);
                    return user != null;
                });
            options.SecurityTokenValidators.Add(tokenValidater);
            app.UseJwtBearerAuthentication(options);
        }
    }
}
