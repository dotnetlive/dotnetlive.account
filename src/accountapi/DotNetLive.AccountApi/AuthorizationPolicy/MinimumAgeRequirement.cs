using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace DotNetLive.AccountApi.AuthorizationPolicy
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }

        public int MinimumAge { get; set; }
    }

}
