using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient.Query
{
    public class ListQuery : LoginQuery
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, string>> _sortTypes = new ConcurrentDictionary<Type, Dictionary<string, string>>();

        private int _page;

        public ListQuery()
        {
        }

        public ListQuery(SessionQuery query)
            : base(query.SessionKey)
        {
        }

        public ListQuery(SessionQuery query, int pageIndex = 1, int pageSize = 25, string keywords = null)
            : base(query.SessionKey)
        {
            this.Keywords = keywords;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public virtual Type QueryEntityType
        {
            get { return null; }
        }

        [Query(Name = "keywords")]
        public string Keywords { get; set; }

        [Query(Name = "pageSize")]
        public int? PageSize { get; set; }

        [Query(Name = "pageIndex")]
        public int PageIndex
        {
            get { return _page; }
            set { _page = value <= 0 ? 1 : value; }
        }

        public string SortField { get; set; }

        public string SortDirection { get; set; }

        [Query(Name = "sortField")]
        public string RequestSortField { get; private set; }

        [Query(Name = "sortDir")]
        public string RequestSortDirection { get; private set; }

        public override string ToString()
        {
            RequestSortField = SortField;
            RequestSortDirection = SortDirection;
            if (QueryEntityType != null)
            {
                Dictionary<string, string> map;
                if (!_sortTypes.TryGetValue(QueryEntityType, out map))
                {
                    map = new Dictionary<string, string>();
                    PropertyInfo[] infos = QueryEntityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo info in infos)
                    {
                        var attributes = info.GetCustomAttributes(typeof(SortAttribute), true);
                        if (attributes.Any())
                        {
                            foreach (object attribute in attributes)
                            {
                                if (attribute is SortAttribute)
                                {
                                    map.Add(info.Name, (attribute as SortAttribute).Name);
                                    break;
                                }
                            }
                        }
                    }
                    _sortTypes.TryAdd(QueryEntityType, map);
                }

                string name;
                if (!string.IsNullOrEmpty(SortField) && map.TryGetValue(SortField, out name))
                {
                    RequestSortField = name;
                    RequestSortDirection = SortDirection;
                }
            }
            return base.ToString();
        }
    }
}
