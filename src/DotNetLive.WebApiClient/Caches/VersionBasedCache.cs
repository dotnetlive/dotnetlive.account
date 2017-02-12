using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Odn.WebApiClient.Caches
{
    public abstract class VersionBasedCache<TItem>
      where TItem : class
    {
        private const string KeysConcatFormat = "{0}:{1}#cache - {2}";

        private readonly TimeSpan _expirationTimeout;
        private readonly bool _sliding;
        private readonly object _readSyncObject = new object();


        #region to override in children
        public abstract int CacheIdentifier
        {
            get;
        }

        public virtual bool UserDependent
        {
            get
            {
                return false;
            }
        }

        public long GetVersion()
        {
            return 1;
        }

        protected virtual TItem LoadLatestData(string user)
        {
            return null;
        }

        protected virtual CacheItemPriority Priority
        {
            get
            {
                return CacheItemPriority.Normal;
            }
        }
        #endregion

        protected VersionBasedCache(TimeSpan expirationTimeout, bool sliding)
        {
            _expirationTimeout = expirationTimeout;
            _sliding = sliding;
        }

        #region Access
        protected TItem GetData()
        {
            return GetData(null);
        }

        protected TItem GetData(string userRef)
        {
            userRef = FilterInputUserArg(userRef);
            string keyName = GetCacheItemKey(userRef);

            lock (_readSyncObject)
            {
                CacheItem cacheItem = HttpContext.Current.Cache.Get(keyName) as CacheItem;
                long latestVersion = GetLatestVersion(userRef);

                //expired data in the cache
                if (cacheItem != null && latestVersion != cacheItem.Version)
                {
                    HttpContext.Current.Cache.Remove(keyName);
                    cacheItem = null;
                }

                //try to load new data	
                if (cacheItem == null)
                {
                    TItem newData = LoadLatestData(userRef);
                    if (newData == null)
                        return null;
                    cacheItem = new CacheItem(newData, latestVersion);
                    PutToCache(userRef, cacheItem);
                }

                return cacheItem.Data;
            }
        }

        protected void SetData(TItem data)
        {
            SetData(data, null);
        }

        protected void SetData(TItem data, string userRef)
        {
            userRef = FilterInputUserArg(userRef);
            string keyName = GetCacheItemKey(userRef);
            long latestVersion = GetLatestVersion(userRef);
            CacheItem cacheItem = new CacheItem(data, latestVersion);

            PutToCache(userRef, cacheItem);
        }

        protected void RemoveData(string userRef)
        {
            HttpContext.Current.Cache.Remove(GetCacheItemKey(userRef));
        }

        #endregion

        #region Implementation details
        private string FilterInputUserArg(string user)
        {
            string result = user;
            if (UserDependent)
            {
                //if (user == null && ApplicationUser.IsCurrentExists)
                //{
                //    result = ApplicationUser.Current.UserName;
                //}
                //else
                //{
                //    result = user;
                //}
                result = user;
            }

            return result;
        }

        private string GetCacheItemKey(string user)
        {
            return string.Format(KeysConcatFormat, CacheIdentifier, "global", user);
        }

        private void PutToCache(string user, CacheItem item)
        {
            TimeSpan slidingExpirationTimeout = Cache.NoSlidingExpiration;
            DateTime expirationTime = Cache.NoAbsoluteExpiration;

            if (_sliding)
                slidingExpirationTimeout = _expirationTimeout;
            else
                expirationTime = DateTime.UtcNow + _expirationTimeout;

            CacheItemRemovedCallback itemRevomeCallback = null;

            HttpContext.Current.Cache.Insert(GetCacheItemKey(user), item, null, expirationTime, slidingExpirationTimeout, Priority, itemRevomeCallback);

        }

        protected virtual long GetLatestVersion(string user)
        {
            return 1;
        }

        #endregion

        #region CacheItem
        protected class CacheItem
        {
            private TItem _data;
            public TItem Data
            {
                get { return _data; }
            }

            private long _version;
            public long Version
            {
                get { return _version; }
            }

            public CacheItem(TItem data, long version)
            {
                _data = data;
                _version = version;
            }
        }
        #endregion

    }
}
