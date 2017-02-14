using DotNetLive.Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetLive.Account.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("auth.sys_user")]
    public class User : BaseEntity
    {
        [Column("email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// 角色
    /// </summary>
    [Table("auth.sys_role")]
    public class Role : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 用户登陆终端
    /// </summary>
    [Table("auth.user_device")]
    public class UserDevice : BaseEntity
    {
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
    }
}
