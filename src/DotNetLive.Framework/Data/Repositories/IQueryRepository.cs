using DotNetLive.Framework.Models;
using System;
using System.Collections.Generic;

namespace DotNetLive.Framework.Data.Repositories
{
    public interface IQueryRepository
    {
        T Get<T>(Guid key) where T : class;
        T Get<T>(string sql, object param = null);

        IEnumerable<T> GetAll<T>(string sql = "", object parms = null) where T : class;
        IEnumerable<T> WhereSearch<T>(string whereCondition = " where 1=1 ", object parms = null) where T : class;
        Page<T> PagerSearch<T>(string extraColumns = "", string joinStatement = "", string where = "", string order = "", int pageIndex = 1, int pageSize = 20, object parms = null) where T : class;

        //query problems need reslove
        //1. Child object query problem
        //2. Easy refactor request all property need by code, not hard code, this requires a linq to sql command translator

        //Child object query problem for PostgreSQL
        //http://johnatten.com/2015/04/22/use-postgres-json-type-and-aggregate-functions-to-map-relational-data-to-json/
    }
}
