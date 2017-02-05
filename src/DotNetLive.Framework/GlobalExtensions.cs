using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetLive.Framework
{
    public static class GlobalExtensions
    {
        public static void ForEach<T>(this T[] obj, Action<T> action)
        {
            foreach (var item in obj)
            {
                action(item);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> obj)
        {
            return obj == null || !obj.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> obj)
        {
            return obj != null && obj.Any();
        }

        public static bool IsGenericType(this Type obj)
        {
            return obj.GetTypeInfo().IsGenericType;
        }

        public static bool IsInterface(this Type obj)
        {
            return obj.GetTypeInfo().IsInterface;
        }
    }
}
