using System.Collections.Generic;

namespace DotNetLive.Framework.Models
{
    public class Page<T>
    {
        public Page()
        {
        }

        public Page(List<T> records, Paging paging)
        {
            Records = records;
            Paging = paging;
        }

        public Paging Paging { get; set; }
        public List<T> Records { get; set; }
    }
}
