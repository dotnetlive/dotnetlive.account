using System;
using System.Security.Claims;

namespace DotNetLive.Framework.UserIdentity.Models
{
    public class UserClaim : IEquatable<UserClaim>, IEquatable<Claim>
    {
        public UserClaim(Claim claim)
        {
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }

        public UserClaim(string claimType, string claimValue)
        {
            if (claimType == null)
            {
                throw new ArgumentNullException(nameof(claimType));
            }
            if (claimValue == null)
            {
                throw new ArgumentNullException(nameof(claimValue));
            }

            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public string ClaimType { get; private set; }
        public string ClaimValue { get; private set; }

        public bool Equals(UserClaim other)
        {
            return other.ClaimType.Equals(ClaimType)
                && other.ClaimValue.Equals(ClaimValue);
        }

        public bool Equals(Claim other)
        {
            return other.Type.Equals(ClaimType)
                && other.Value.Equals(ClaimValue);
        }
    }
}
