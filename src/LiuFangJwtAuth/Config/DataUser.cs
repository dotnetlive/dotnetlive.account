using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiuFangJwtAuth.Config
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class DataUser
    {
        public string name { get; set; }
        public string pwd { get; set; }
        public List<Claim> claims { get; set; }
        public DataUser(string name,string pwd, List<Claim> claims)
        {
            this.name = name;
            this.pwd = pwd;
            this.claims = claims;
        }
    }
}
