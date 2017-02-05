using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace DotNetLive.Framework.Data
{
    /// <summary>
    /// Query Db Connection
    /// <!--This connection should only use for query action-->
    /// </summary>
    public class QueryDbConnection : IDisposable
    {
        private IDbConnection _dbConnection;
        public Guid ConnectionId { get; private set; }

        public QueryDbConnection(IOptions<DbSettings> dbSettings)
        {
            ConnectionId = Guid.NewGuid();
            _dbConnection = new NpgsqlConnection(dbSettings.Value.QueryDbConnectionString);
        }

        public IDbConnection DbConnection
        {
            get
            {
                //design: only open when needed.
                if (_dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection.Open();
                }
                return _dbConnection;
            }
        }

        public void Dispose()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }
    }
}
