using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient.Query
{
    public class LoginQuery : SessionQuery
    {
        public LoginQuery()
        {
            DeviceType = 1;
        }

        public LoginQuery(string sessionKey)
        {
            this.SessionKey = sessionKey;
        }

        public LoginQuery(LoginQuery query)
            : this()
        {
            if (query != null)
            {
                LoginId = query.LoginId;
                Password = query.Password;
                SessionKey = query.SessionKey;
                DeviceType = query.DeviceType;
            }
        }

        [Query(Name = "loginIdorEmail")]
        public string LoginId { get; set; }

        public string Password { get; set; }

        [QueryAttribute(Name = "deviceType")]
        public int DeviceType { get; set; }

        [Query(Name = "hashedPassword")]
        public string HashedPassword
        {
            // get { return Password; }
            get { return !string.IsNullOrEmpty(Password) ? MD5CryptoProvider.GetMD5Hash(Password) : null; }
        }

        /// <summary>
        /// 是否记住用户(延长sessionKey过期时间)
        /// </summary>
        [Query(Name = "isRemeber")]
        public bool IsRemeber { get; set; }

        class MD5CryptoProvider
        {
            public static string GetMD5Hash(string input)
            {
                MD5 md5Hasher = MD5.Create();

                byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

                var sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }

            public static bool VerifyMD5Hash(string input, string hash)
            {
                string hashOfInput = GetMD5Hash(input);

                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
