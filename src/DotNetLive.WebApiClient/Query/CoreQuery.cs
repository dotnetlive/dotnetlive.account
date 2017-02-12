using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace DotNetLive.WebApiClient.Query
{
    /// <summary>
    /// 所有Query的基类
    /// </summary>
    public class CoreQuery
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyDescription>> Types = new ConcurrentDictionary<Type, List<PropertyDescription>>();

        protected List<string> CustomParameters = new List<string>();

        public dynamic ParmsObj { get; set; }

        public virtual bool IgnoreEnvelope { get { return true; } }

        public static CoreQuery Instance { get { return new CoreQuery(); } }

        public void AddParameter(string key, string value)
        {
            CustomParameters.Add(string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value)));
        }

        public override string ToString()
        {
            List<string> result = new List<string>();
            result.AddRange(LoadParmsObj() ?? new List<string>(0));

            var type = GetType();
            List<PropertyDescription> properties;
            if (!Types.TryGetValue(type, out properties))
            {
                var infos = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                properties = (from propertyInfo in infos where propertyInfo.Name != "ParmsObj" select new PropertyDescription(propertyInfo)).ToList();
                Types.TryAdd(type, properties);
            }
            var parameters = properties.Select(item => item.GetFormattedValue(this))
                .Where(item => !string.IsNullOrEmpty(item)).ToList();

            result.AddRange(parameters);
            result.AddRange(CustomParameters);

            return string.Join("&", result.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
        }

        private List<string> LoadParmsObj()
        {
            if (ParmsObj == null) return null;
            List<string> results = new List<string>();
            foreach (PropertyInfo propertyInfo in ParmsObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object obj = propertyInfo.GetValue(ParmsObj, null);
                string value = null;
                if (obj != null)
                {
                    const string format = "{0}";
                    value = WebUtility.UrlEncode(string.Format(format, obj));
                    if (string.IsNullOrWhiteSpace(value))
                        continue;//如果是NULL或者Empty, 不加入参数
                    value = string.Format("{0}={1}", WebUtility.UrlEncode(propertyInfo.Name), value);
                }
                results.Add(value ?? string.Empty);
            }
            return results;
        }

        protected class PropertyDescription
        {
            public PropertyDescription()
            {
                Attributes = new List<QueryAttribute>();
            }

            public PropertyDescription(PropertyInfo info)
                : this()
            {
                Info = info;
                var attributes = info.GetCustomAttributes(typeof(QueryAttribute), true);
                if (!attributes.Any()) return;
                foreach (var attribute in attributes.OfType<QueryAttribute>())
                {
                    Attributes.Add(attribute);
                }
            }

            public PropertyInfo Info { get; private set; }

            public List<QueryAttribute> Attributes { get; private set; }

            public string GetFormattedValue(object current)
            {
                var obj = Info.GetValue(current, null);
                string value = null;
                if (obj != null && Attributes.Count > 0)
                {
                    QueryAttribute attribute = Attributes[0];
                    obj = attribute.Converter.Convert(obj, attribute.Format);
                    var format = string.IsNullOrEmpty(attribute.Format) ? "{0}" : string.Format("{{0:{0}}}", attribute.Format);
                    value = WebUtility.UrlEncode(string.Format(format, obj));
                    value = string.Format("{0}={1}", WebUtility.UrlEncode(attribute.Name), value);
                }
                return value ?? string.Empty;
            }
        }
    }
}
