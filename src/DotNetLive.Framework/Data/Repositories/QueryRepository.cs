using Dapper;
using DotNetLive.Framework.Dapper.Extensions;
using DotNetLive.Framework.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DotNetLive.Framework.Data.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private IDbConnection _conn;
        private ILogger<QueryRepository> _logger;

        public QueryRepository(QueryDbConnection conn, ILogger<QueryRepository> logger)
        {
            this._conn = conn.DbConnection;
            this._logger = logger;
        }

        public T Get<T>(string sql, object param)
        {
            return _conn.QuerySingleOrDefault<T>(sql, param);
        }

        public T Get<T>(Guid key) where T : class
        {
            return _conn.Get<T>(key);
        }

        /// <summary>
        /// 一个简单的List查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>(string sql = "", object parms = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(sql.Trim()))
            {
                sql = $"select {EntityMapper<T>.GetColumnNames("t")} from {EntityMapper<T>.GetTableName()} t ";
            }
            return _conn.Query<T>(sql, parms);
        }

        /// <summary>
        /// 只根据where后面的条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IEnumerable<T> WhereSearch<T>(string condition = " where 1=1 ", object parms = null) where T : class
        {

            var sql = $"select {EntityMapper<T>.GetColumnNames("t")} from {EntityMapper<T>.GetTableName()} t ";
            if (!string.IsNullOrWhiteSpace(condition.Trim()))
            {
                sql = $"{sql} {condition}";
            }
            return _conn.Query<T>(sql, parms);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extraColumns"></param>
        /// <param name="joinStatement"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Page<T> PagerSearch<T>(string extraColumns = "", string joinStatement = "", string where = "", string order = "", int pageIndex = 1, int pageSize = 20, object parms = null) where T : class
        {
            StringBuilder sbColumnsSql = new StringBuilder();
            sbColumnsSql.Append($"select {EntityMapper<T>.GetColumnNames("t")}");
            if (!string.IsNullOrWhiteSpace(extraColumns))
            {
                sbColumnsSql.Append($",{extraColumns}");
            }

            var sbFromAndWhereSql = new StringBuilder();
            sbFromAndWhereSql.Append($" from {EntityMapper<T>.GetTableName()} t ");
            if (!string.IsNullOrWhiteSpace(joinStatement))
            {
                sbFromAndWhereSql.Append($" {joinStatement} ");
            }

            sbFromAndWhereSql.Append($" where 1=1 ");
            if (!string.IsNullOrWhiteSpace(where))
            {
                sbFromAndWhereSql.Append($" and {where}");
            }

            var sbOrderSql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(order))
            {
                sbOrderSql.Append($" order by {order}");
            }

            var itemsSelectSql = $"{sbColumnsSql.ToString()} {sbFromAndWhereSql.ToString()} {sbOrderSql.ToString()}";
            var countSql = $"select count(1) {sbFromAndWhereSql.ToString()} ";

            _logger.LogInformation(itemsSelectSql);
            _logger.LogInformation(countSql);
            using (var multi = _conn.QueryMultiple($"{itemsSelectSql};{countSql}", parms))
            {
                var items = multi.Read<T>().ToList();
                var total = multi.ReadSingle<int>();
                return new Page<T>(items, new Paging() { Total = total, PageIndex = pageIndex, PageSize = pageSize });
            }
        }
            }
}
