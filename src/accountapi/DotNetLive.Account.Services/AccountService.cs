using System;

namespace DotNetLive.Account.Services
{
    public class AccountService
    {
        public UserQueryService UserQueryService { get; private set; }

        public AccountService(UserQueryService userQuerySerivce)
        {
            this.UserQueryService = userQuerySerivce;
        }

        public bool Login(string username, string passwordHash)
        {
            var user = UserQueryService.GetUserByEmail(username);

            return user != null && passwordHash.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
