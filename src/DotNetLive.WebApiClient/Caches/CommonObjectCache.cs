using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odn.WebApiClient.Caches
{
    /// <summary>
    /// 全局公共对象Cache，过期时间是5分钟
    /// </summary>
    public class CommonObjectCache : VersionBasedCache<Object>
    {
        private static readonly TimeSpan ExpirationTime = new TimeSpan(0, 5, 0);

        private static readonly CommonObjectCache _instance = new CommonObjectCache();
        private static CommonObjectCache Instance
        {
            get { return CommonObjectCache._instance; }
        }

        private CommonObjectCache()
            : base(ExpirationTime, false)
        {

        }

        public override int CacheIdentifier
        {
            get
            {
                return (int)CacheType.Object;
            }
        }

        public override bool UserDependent
        {
            get
            {
                return false;
            }
        }

        public Object Get(string data)
        {
            return GetData(data);
        }


        public void Set(string dataKey, Object data)
        {
            SetData(data, dataKey);
        }

        public void Remove(string dataKey)
        {
            RemoveData(dataKey);
        }

        /// <summary>
        /// 缓存某一个对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public static TResult CacheResult<TResult>(Func<TResult> action, string dataKey)
       where TResult : class
        {
            if (string.IsNullOrWhiteSpace(dataKey))
                throw new Exception("DataKey不能为空");

            var cachedData = CommonObjectCache.Instance.Get(dataKey);
            if (cachedData == null)
            {
                var result = action();
                CommonObjectCache.Instance.Set(dataKey, result);
                return result;
            }
            return cachedData as TResult;
        }

        public static TResult Get<TResult>(string dataKey) where TResult : class
        {
            if (string.IsNullOrWhiteSpace(dataKey))
                throw new Exception("DataKey不能为空");

            var cachedData = CommonObjectCache.Instance.Get(dataKey);
            return cachedData as TResult;
        }

        /// <summary>
        /// 移除某个对象
        /// </summary>
        /// <param name="dataKey"></param>
        public static void RemoveKey(string dataKey)
        {
            CommonObjectCache.Instance.Remove(dataKey);
        }
    }
}
