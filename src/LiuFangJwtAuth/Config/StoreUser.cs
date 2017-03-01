using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiuFangJwtAuth.Config
{
    public class StoreUser
    {
        //模拟用户
        public static List<DataUser> list = new List<DataUser>()
            {
                new DataUser("111111","111111",new List<Claim> {
                    new Claim("admin","admin")
                }),
                new DataUser("222222","222222",new List<Claim> {
                    new Claim("test","test")
                }),
                new DataUser("333333","333333",new List<Claim> {
                    new Claim("test","test")
                })
            };
        public static IEnumerable<DataUser> GetLocalUser()
        {
            return list;
        }
        /// <summary>
        /// 判读用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool CheckUserExist(string userName,string pwd)
        {
            var model = list.Where(m => (m.name == userName && m.pwd == pwd)).FirstOrDefault();
            if (model != null)
                return true;
            else
                return false;
        }
    }
}
