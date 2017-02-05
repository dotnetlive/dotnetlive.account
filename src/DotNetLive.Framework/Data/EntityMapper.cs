using DotNetLive.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DotNetLive.Framework.Data
{
    public static class EntityMapper<T>
    {
        private static Type entityType;

        static EntityMapper()
        {
            entityType = typeof(T);
        }

        public static string GetColumnNames(string tableAlise = "")
        {
            List<string> columns = new List<string>();
            var allProperties = entityType.GetProperties()
                .Where(x => x.GetCustomAttributes<KeyAttribute>(true).Any() || x.GetCustomAttributes<ColumnAttribute>(true).Any());
            foreach (var pi in allProperties)
            {
                var columnAttribute = pi.GetCustomAttribute<ColumnAttribute>(true);
                if (columnAttribute != null)
                {
                    columns.Add($"{columnAttribute.Name} as {pi.Name}");
                    continue;
                }
            }

            if (tableAlise.IsNotEmpty())
            {
                columns.ForEach(item =>
                {
                    item = $"{tableAlise}.{item}";
                });
            }
            return string.Join(",", columns);
        }

        public static string GetKeyName()
        {
            var keyProperty = entityType.GetProperties().SingleOrDefault(x => x.GetCustomAttribute<KeyAttribute>(true) != null);
            var columnAttribute = keyProperty.GetCustomAttribute<ColumnAttribute>(true);
            return columnAttribute.Name;
        }

        public static string GetTableName()
        {
            var attr = entityType.GetTypeInfo().GetCustomAttribute<TableAttribute>();
            return attr == null ? entityType.Name.ToLower() : attr.Name;
        }
    }
}
