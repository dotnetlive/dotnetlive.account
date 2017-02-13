using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetLive.Framework.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class BaseEntity<TKey>
    {
        /// <summary>
        /// 系统主键
        /// </summary>
        [Key, Column("sysid", TypeName = "uuid")]
        public TKey SysId { get; set; }
    }

    /// <summary>
    /// GUID类型为主键的实体积累
    /// </summary>
    public class BaseEntity : BaseEntity<Guid>
    {
        public BaseEntity()
        {
            this.SysId = Guid.NewGuid();
        }
    }
}
