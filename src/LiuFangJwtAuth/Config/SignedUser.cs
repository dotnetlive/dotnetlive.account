using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiuFangJwtAuth.Config
{
    /// <summary>
    /// 已经登录的用户
    /// </summary>
    public class SignedUser
    {
        //jwt签名凭证
        public string userToken { get; set; }
        //用户唯一id名
        public string userName { get; set; }
        //用户声明属性
        public List<Claim> userClaims { get; set; }
        public SignedUser(string userToken,string userName,List<Claim> userClaims)
        {
            this.userToken = userToken;
            this.userName = userName;
            this.userClaims = userClaims;
        }
    }
}
