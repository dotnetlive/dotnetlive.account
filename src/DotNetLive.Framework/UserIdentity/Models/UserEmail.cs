using System;

namespace DotNetLive.Framework.UserIdentity.Models
{
    public class UserEmail : UserContactRecord
    {
        public UserEmail(string email) : base(email)
        {
        }

        public string NormalizedValue { get; private set; }

        public virtual void SetNormalizedEmail(string normalizedEmail)
        {
            if (normalizedEmail == null)
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            NormalizedValue = normalizedEmail;
        }
    }
}
