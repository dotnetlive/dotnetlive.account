using Dapper;
using DotNetLive.Framework.Dapper.Extensions;
using DotNetLive.Framework.Entities;
using System;
using System.Data;

namespace DotNetLive.Framework.Data.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private IDbConnection _conn
        {
            get
            {
                return Trans.Connection;
            }
        }

        private IDbTransaction Trans
        {
            get
            {
                return CommandDbConn.DbTransaction;
            }
        }

        private CommandDbConnection CommandDbConn;

        public CommandRepository(CommandDbConnection conn)
        {
            CommandDbConn = conn;
        }

        public void Add<T>(T entity) where T : class
        {
            _conn.Insert<T, Guid>(entity, CommandDbConn.DbTransaction);
        }

        public T Get<T>(Guid key) where T : BaseEntity<Guid>
        {
            return _conn.Get<T>(key, CommandDbConn.DbTransaction);
        }

        public void Update<T>(T entity) where T : BaseEntity<Guid>
        {
            _conn.Update<T>(entity, CommandDbConn.DbTransaction);
        }

        public void Delete<T>(Guid key) where T : BaseEntity<Guid>, new()
        {
            var entity = new T()
            {
                SysId = key
            };
            _conn.Delete(entity, CommandDbConn.DbTransaction);
        }

        public void Delete<T>(T entity) where T : BaseEntity<Guid>, new()
        {
            _conn.Delete(entity, CommandDbConn.DbTransaction);
        }

        public void Execute(string sql, object parms)
        {
            _conn.Execute(sql, parms, CommandDbConn.DbTransaction);
        }
    }
}
