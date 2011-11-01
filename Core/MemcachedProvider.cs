using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using BeIT.MemCached;

namespace AutoBox
{
    /// <summary>
    /// Defines provider for memcached store.
    /// </summary>
    internal class MemcachedProvider : ICacheProvider
    {
        public MemcachedProvider(string cacheStore)
        {
            string name = string.Format("{0}-{1}", typeof(MemcachedProvider).Name, AppDomain.CurrentDomain.Id);
            
            if (!MemcachedClient.Exists(name))
                MemcachedClient.Setup(name, cacheStore.Split(new char[] { ',' }));
            
            memcachedClient = MemcachedClient.GetInstance(name);
        }

        /// <summary>
        /// Gets the cached object for the specific key.
        /// </summary>
        public object GetObject(string key)
        {
            return memcachedClient.Get(key);
        }

        /// <summary>
        /// Sets the object to cache store for the specific key and sets the expiry date.
        /// </summary>
        public void SetObject(string key, object value, DateTime expiry)
        {
            memcachedClient.Set(key, value, expiry);
        }

        private readonly MemcachedClient memcachedClient;
    }
}
