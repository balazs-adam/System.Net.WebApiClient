using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Net.WebApiClient.Extensions
{
    static class QueryStringExtensions
    {
        public const string DefaultSeparator = "&";
        public const string DefaultDateTimeFormat = "yyyy-MM-ddThh:mm:ss";

        public static string ToQueryString(this object request, string separator = DefaultSeparator, string dateTimeFormat = DefaultDateTimeFormat)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            //Property-k elkérése az objektumról
            var properties = request
                .GetType()
                .GetRuntimeProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            //IEnumerable property-k neveinek elkérése (kivéve string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            //IEnumerable property-k neveinek szeparátor karakterrel elválasztott összefűzése.
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType()
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive() || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            //Kulcs-érték párok '&' jellel elválasztott összefűzése 
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString((x.Value is DateTime ? (x.Value as DateTime?).Value.ToString(dateTimeFormat) : x.Value.ToString())))));
        }
    }
}
