using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Core.CacheProvider.Interface;
using Core.CacheProvider.Model;

namespace Core.CacheProvider
{
    public class CacheProvider : ICacheProvider
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _cachePolicy;

        public CacheProvider()
        {
            _cachePolicy = new CacheItemPolicy
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = TimeSpan.FromHours(1)
            };
        }

        public void FlushAllCache()
        {
            foreach (var cachedItem in _cache)
            {
                _cache.Remove(cachedItem.Key);
            }
        }

        public T CheckCache<T>(string cachName)
        {
            return (T)_cache[cachName];
        }

        public bool AddToCache<T>(string cachName, T itemToCache)
        {
            return !_cache.Contains(cachName) && _cache.Add(cachName, itemToCache, _cachePolicy);
        }

        public bool RemoveFromCache(string cachName)
        {
            _cache.Remove(cachName);
            return true;
        }

        public IEnumerable<CachedItem> GetAllCacheObjects()
        {
            var results = new List<CachedItem>();

            foreach (var cachedItem in _cache)
            {
                if (cachedItem.Value.GetType().GetGenericArguments() != null && cachedItem.Value.GetType().GetGenericArguments().Any())
                {
                    results.Add(new CachedItem
                    {
                        Name = cachedItem.Key,
                        Type = cachedItem.Value.GetType().GetGenericArguments()[0],
                        Item = cachedItem.Value
                    });
                }
            }

            return results;
        }

        public void Dispose()
        {
            if (_cache != null)
            {
                _cache.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
