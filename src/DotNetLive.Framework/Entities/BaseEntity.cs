using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetLive.Framework.Entities
{
    public class BaseEntity<TKey>
    {
        [Key, Column("sysid", TypeName = "uuid")]
        public TKey SysId { get; set; }
    }

    public class BaseEntity : BaseEntity<Guid>
    {
        public BaseEntity()
        {
            this.SysId = Guid.NewGuid();
        }
    }
}
