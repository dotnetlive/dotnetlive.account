using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi.AuthorizationPolicy
{
    public class EnterBuildingRequirement : IAuthorizationRequirement
    {
    }

    public class BadgeEntryHandler : AuthorizationHandler<EnterBuildingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == DotNetLiveClaimTypes.BadgeId &&
                                           c.Issuer == "http://microsoftsecurity"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class HasTemporaryStickerHandler : AuthorizationHandler<EnterBuildingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == DotNetLiveClaimTypes.TemporaryBadgeId &&
                                           c.Issuer == "https://microsoftsecurity"))
            {
                // We'd also check the expiration date on the sticker.
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public static class DotNetLiveClaimTypes
    {
        public const string BadgeId = "http://schemas.dotnet.live/ws/2017/02/identity/claims/badgeid";
        public const string TemporaryBadgeId = "http://schemas.dotnet.live/ws/2017/02/identity/claims/temporarybadgeid";
    }

}
