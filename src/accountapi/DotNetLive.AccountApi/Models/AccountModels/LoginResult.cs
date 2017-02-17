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
    }
}
