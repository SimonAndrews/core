using System;

namespace Core.CacheProvider.Model
{
    public class CachedItem
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public dynamic Item { get; set; }
    }
}
