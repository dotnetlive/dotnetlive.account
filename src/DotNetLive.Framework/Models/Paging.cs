using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.Framework.Models
{
    public class Paging
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        public int PageIndex { get; set; }

        /// <summary>
        /// records per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// whether has next page
        /// </summary>
        public bool HasNextPage { get { return PageIndex < Pages; } }

        /// <summary>
        /// total num of pages
        /// </summary>
        public int Pages
        {
            get { return Total % PageSize > 0 ? Convert.ToInt32(Total / PageSize + 1) : Convert.ToInt32(Total / PageSize); }
        }
    }
}