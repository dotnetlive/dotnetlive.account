using System;
using System.Reflection;

public static class TypeExtensions
{
    public static bool IsGenericType(this Type obj)
    {
        return obj.GetTypeInfo().IsGenericType;
    }

    public static bool IsInterface(this Type obj)
    {
        return obj.GetTypeInfo().IsInterface;
    }
}