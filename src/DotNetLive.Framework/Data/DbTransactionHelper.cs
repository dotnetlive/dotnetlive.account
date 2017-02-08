using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetLive.Framework.Data
{
    public static class DbTransactionHelper
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void CommitTransaction(IServiceProvider serviceProvider)
        {
            var commandDbConnection = serviceProvider.GetService<CommandDbConnection>();
            commandDbConnection.TransactionCommit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void RollbackTransaction(IServiceProvider serviceProvider)
        {
            var commandDbConnection = serviceProvider.GetService<CommandDbConnection>();
            commandDbConnection.TransactionRollback();
        }

        public static void DisposeDbConnection(IServiceProvider serviceProvider)
        {
            //Dispose Query Connection
            using (var commandDbConnection = serviceProvider.GetService<CommandDbConnection>()) { }

            //Dispose Command Query
            using (serviceProvider.GetService<QueryDbConnection>()) { }
        }
    }
}
