using DotNetLive.Framework.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetLive.Account.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("auth.sysrole")]
    public class SysRole : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
