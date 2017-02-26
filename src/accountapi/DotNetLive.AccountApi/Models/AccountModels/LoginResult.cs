using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi.Models.AccountModels
{
    public class LoginResult
    {
        [JsonProperty("token")]
        public string Token { get; internal set; }
        [JsonProperty("expires_in")]
        public double ExpiresIn { get; internal set; }
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
