using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace DotNetLive.Framework.Data
{
    /// <summary>
    /// Command DbConnection
    /// <!--This connection should open the transaction for command operation-->
    /// this should be register as per scope
    /// </summary>
    public class CommandDbConnection : IDisposable
    {
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private static object dbTransCreateLocker = new object();
        private ILogger<CommandDbConnection> _logger;

        public Guid ConnectionId { get; private set; }

        public CommandDbConnection(IOptions<DbSettings> dbSettings, ILogger<CommandDbConnection> logger)
        {
            ConnectionId = Guid.NewGuid();
            _dbConnection = new NpgsqlConnection();
            _dbConnection.ConnectionString = dbSettings.Value.CommandDbConnectionString;
            _logger = logger;
        }

        public IDbConnection DbConnection
        {
            get
            {
                //design: only open when needed.
                if (_dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection.Open();
                    _logger.LogDebug("Opened a new db connection");
                }
                return _dbConnection;
            }
        }

        public IDbTransaction DbTransaction
        {
            get
            {
                if (_dbTransaction != null)
                    return _dbTransaction;

                lock (dbTransCreateLocker)
                {
                    if (_dbTransaction == null)
                    {
                        _dbTransaction = DbConnection.BeginTransaction(IsolationLevel.Serializable);
                        _logger.LogDebug("Opened a new db transaction");
                    }
                }
                return _dbTransaction;
            }
        }

        public void TransactionCommit()
        {
            if (_dbTransaction != null)
            {
                var trans = _dbTransaction as NpgsqlTransaction;
                if (!trans.IsCompleted)
                {
                    trans.Commit();
                    _logger.LogDebug("db transaction commited");
                }
            }
        }

        public void TransactionRollback()
        {
            if (_dbTransaction != null)
            {
                var trans = _dbTransaction as NpgsqlTransaction;
                if (!trans.IsCompleted)
                {
                    trans.Rollback();
                    _logger.LogDebug("db transaction rollbacked");
                }
            }
        }

        public void Dispose()
        {
            if (_dbTransaction != null)
            {
                var trans = _dbTransaction as NpgsqlTransaction;
                if (!trans.IsCompleted)
                    trans.Commit();
                _dbTransaction.Dispose();
                _logger.LogDebug("db transaction disposed");
            }

            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
                _logger.LogDebug("db connection disposed");
            }
        }
    }
}
