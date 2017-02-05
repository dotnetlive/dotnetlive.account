using DotNetLive.Framework.Entities;
using System;

namespace DotNetLive.Framework.Data.Repositories
{
    public interface ICommandRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : BaseEntity<Guid>;

        T Get<T>(Guid key) where T : BaseEntity<Guid>;

        void Delete<T>(Guid key) where T : BaseEntity<Guid>, new();

        void Delete<T>(T entity) where T : BaseEntity<Guid>, new();

        void Execute(string sql, object parms = null);
    }
}
