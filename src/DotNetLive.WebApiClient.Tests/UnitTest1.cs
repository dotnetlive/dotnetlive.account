using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetLive.WebApiClient.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WebApiClient_Basic_Test_should_pass()
        {
            var rsp = ApiClient.Get<List<LoginResult>>("http://api.account.dotnet.live", "user");
            Assert.True(rsp.Success);
            Assert.Equal(10, rsp.ResponseResult.Count);
        }

        [Fact]
        public void WebApiClient_Basic_Test_should_failure()
        {
            var rsp = ApiClient.Get<List<LoginResult>>("http://api.account.dotnet.live", "users");
            Assert.False(rsp.Success);
        }
    }

    public class LoginResult
    {
        public string Token { get; set; }
    }
}
