using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace AutoBox
{
    internal sealed class AutoBoxSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the current instance from config.
        /// </summary>
        public static AutoBoxSection Instance
        {
            get
            {
                return section;
            }
        }

        /// <summary>
        /// Gets or sets the cache store location.
        /// </summary>
        [ConfigurationProperty("cacheStore")]
        public string CacheStore
        {
            get
            {
                return (string)this["cacheStore"];
            }
            set
            {
                this["cacheStore"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the cache provider.
        /// </summary>
        [ConfigurationProperty("cacheProvider", DefaultValue="MemcachedProvider")]
        public string CacheProvider
        {
            get
            {
                return (string)this["cacheProvider"];
            }
            set
            {
                this["cacheProvider"] = value;
            }
        }

        private readonly static AutoBoxSection section = ConfigurationManager.GetSection("autoBox") as AutoBoxSection;
    }
}
