using DotNetLive.Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetLive.Account.Entities
{
    /// <summary>
    /// 用户登陆终端
    /// </summary>
    [Table("auth.user_device")]
    public class UserDevice : BaseEntity
    {
        public Guid UserSysId { get; set; }

        /// <summary>
        /// 生成的token
        /// </summary>
        [Column("token")]
        public string Token { get; set; }

        /// <summary>
        /// token过期时间
        /// </summary>
        [Column("expire_time")]
        public DateTime ExpireTime { get; set; }
        public DateTime IssueTime { get; set; }
    }
}
