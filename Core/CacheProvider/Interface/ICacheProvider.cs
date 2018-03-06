using System;
using System.Collections.Generic;
using Core.CacheProvider.Model;

namespace Core.CacheProvider.Interface
{
    public interface ICacheProvider : IDisposable
    {
        void FlushAllCache();
        T CheckCache<T>(string cachName);
        bool AddToCache<T>(string cachName, T itemToCache);
        bool RemoveFromCache(string cachName);
        IEnumerable<CachedItem> GetAllCacheObjects();
    }
}
