using DotNetLive.Framework.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetLive.Account.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table("auth.permission")]
    public class Permission : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
