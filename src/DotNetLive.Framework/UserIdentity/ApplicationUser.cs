using DotNetLive.Framework.UserIdentity;
using DotNetLive.Framework.UserIdentity.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DotNetLive.Framework.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser
    {
        private readonly List<UserClaim> _claims;
        private readonly List<UserLogin> _logins;

        public ApplicationUser(string userName, string email) : this(userName)
        {
            if (email != null)
            {
                Email = new UserEmail(email);
            }
        }

        public ApplicationUser(string userName, UserEmail email) : this(userName)
        {
            if (email != null)
            {
                Email = email;
            }
        }

        public ApplicationUser(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            Id = GenerateId(userName);
            UserName = userName;
            CreatedOn = new Occurrence();

            _claims = new List<UserClaim>();
            _logins = new List<UserLogin>();
        }

        public ApplicationUser()
        {

        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string NormalizedUserName { get; private set; }
        public UserEmail Email { get; private set; }

        public UserPhoneNumber PhoneNumber { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public bool IsTwoFactorEnabled { get; private set; }

        public IEnumerable<UserClaim> Claims
        {
            get
            {
                return _claims;
            }

            private set
            {
                if (value != null)
                {
                    _claims.AddRange(value);
                }
            }
        }

        public IEnumerable<UserLogin> Logins
        {
            get
            {
                return _logins;
            }

            private set
            {
                if (value != null)
                {
                    _logins.AddRange(value);
                }
            }
        }

        public int AccessFailedCount { get; private set; }
        public bool IsLockoutEnabled { get; private set; }
        public FutureOccurrence LockoutEndDate { get; private set; }

        public Occurrence CreatedOn { get; private set; }
        public Occurrence DeletedOn { get; private set; }

        public virtual void EnableTwoFactorAuthentication()
        {
            IsTwoFactorEnabled = true;
        }

        public virtual void DisableTwoFactorAuthentication()
        {
            IsTwoFactorEnabled = false;
        }

        public virtual void EnableLockout()
        {
            IsLockoutEnabled = true;
        }

        public virtual void DisableLockout()
        {
            IsLockoutEnabled = false;
        }

        public virtual void SetEmail(string email)
        {
            var mongoUserEmail = new UserEmail(email);
            SetEmail(mongoUserEmail);
        }

        public virtual void SetEmail(UserEmail mongoUserEmail)
        {
            Email = mongoUserEmail;
        }

        public virtual void SetNormalizedUserName(string normalizedUserName)
        {
            if (normalizedUserName == null)
            {
                throw new ArgumentNullException(nameof(normalizedUserName));
            }

            NormalizedUserName = normalizedUserName;
        }

        public virtual void SetPhoneNumber(string phoneNumber)
        {
            var mongoUserPhoneNumber = new UserPhoneNumber(phoneNumber);
            SetPhoneNumber(mongoUserPhoneNumber);
        }

        public virtual void SetPhoneNumber(UserPhoneNumber mongoUserPhoneNumber)
        {
            PhoneNumber = mongoUserPhoneNumber;
        }

        public virtual void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public virtual void SetSecurityStamp(string securityStamp)
        {
            SecurityStamp = securityStamp;
        }

        public virtual void SetAccessFailedCount(int accessFailedCount)
        {
            AccessFailedCount = accessFailedCount;
        }

        public virtual void ResetAccessFailedCount()
        {
            AccessFailedCount = 0;
        }

        public virtual void LockUntil(DateTime lockoutEndDate)
        {
            LockoutEndDate = new FutureOccurrence(lockoutEndDate);
        }

        public virtual void AddClaim(Claim claim)
        {
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            AddClaim(new UserClaim(claim));
        }

        public virtual void AddClaim(UserClaim mongoUserClaim)
        {
            if (mongoUserClaim == null)
            {
                throw new ArgumentNullException(nameof(mongoUserClaim));
            }

            _claims.Add(mongoUserClaim);
        }

        public virtual void RemoveClaim(UserClaim mongoUserClaim)
        {
            if (mongoUserClaim == null)
            {
                throw new ArgumentNullException(nameof(mongoUserClaim));
            }

            _claims.Remove(mongoUserClaim);
        }

        public virtual void AddLogin(UserLogin mongoUserLogin)
        {
            if (mongoUserLogin == null)
            {
                throw new ArgumentNullException(nameof(mongoUserLogin));
            }

            _logins.Add(mongoUserLogin);
        }

        public virtual void RemoveLogin(UserLogin mongoUserLogin)
        {
            if (mongoUserLogin == null)
            {
                throw new ArgumentNullException(nameof(mongoUserLogin));
            }

            _logins.Remove(mongoUserLogin);
        }

        public void Delete()
        {
            if (DeletedOn != null)
            {
                throw new InvalidOperationException($"User '{Id}' has already been deleted.");
            }

            DeletedOn = new Occurrence();
        }

        private static string GenerateId(string userName)
        {
            return userName.ToLower();
        }
    }
}
