using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System.Net.WebApiClient.Extensions
{
    static class TypeExtensions
    {
        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsPrimitive(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive;
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }
    }
}
