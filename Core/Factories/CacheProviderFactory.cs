using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using System.Configuration;
using System.Collections;

namespace AutoBox.Factories
{
    internal static class CacheProviderFactory
    {
        /// <summary>
        /// Creates the cache provider from config.
        /// </summary>
        /// <returns></returns>
        public static ICacheProvider Create()
        {
            var instance = AutoBoxSection.Instance;

            if (instance.CacheProvider == typeof(MemcachedProvider).Name)
                return new MemcachedProvider(instance.CacheStore);

            return null;
        }
    }
}
