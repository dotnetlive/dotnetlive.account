using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetLive.Framework.WebApiClient.Query;
using DotNetLive.Framework.WebApiClient;
using Newtonsoft.Json;

namespace DotNetLive.AccountWeb.ApiClients
{
    public class AccountApiClient
    {
        public static ApiResponse<LoginResult> Login(LoginQuery loginQuery)
        {
            loginQuery.AddParameter("withBearerPrefix", "true");
            var rsp = ApiClient.NExecute<LoginResult>("http://localhost:57165/", "account/login", loginQuery);
            return rsp;
        }
    }

    public class LoginResult
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }

        public TimeSpan ExpiresInTimeSpan { get { return TimeSpan.FromSeconds(ExpiresIn); } }

        public LoginUser LoginUser { get; set; }
    }

    public class LoginUser
    {
        public Guid SysId { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
