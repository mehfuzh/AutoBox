using System;
using System.Collections.Generic;
using System.Linq;
using AutoBox.Abstraction;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using AutoBox.Factories;

namespace AutoBox.Handlers
{
    /// <summary>
    /// Cache handler
    /// </summary>
    public class CacheMethodHandler : IHandler
    {
        /// <summary>
        /// Initializes the new instance of <see cref="CacheMethodHandler"/> class.
        /// </summary>
        /// <param name="key">Target key for which the object will be stored.</param>
        /// <param name="cacheDuration">Target cache duration</param>
        /// <param name="invalidated">Defines if the cache should be invalidated</param>
        /// <param name="arguments">Target arguments</param>
        public CacheMethodHandler(string key, TimeSpan cacheDuration, bool invalidated,  object[] arguments)
        {
            this.key = key;
            this.cacheDuration = cacheDuration;
            this.invalidated = invalidated;
            this.arguments = arguments;
        }

        /// <summary>
        /// Returns the cached object. When new it the first stores then returns the cached item.
        /// </summary>
        /// <param name="target">Target object</param>
        /// <param name="invocation">Target invocation</param>
        public object Invoke(object target, IMethodInvocation invocation)
        {
            var provider = CacheProviderFactory.Create();

            string cacheKey = this.GetUniqueKey();

            object result = invalidated ? null : provider.GetObject(cacheKey);

            JsonSerializer serializer = new JsonSerializer() 
            { 
                NullValueHandling = NullValueHandling.Ignore 
            };
               
            if (result == null)
            {
                result = new BaseMethodHandler().Invoke(target, invocation);

                var builder = new StringBuilder();

                using (var writer = new StringWriter(builder))
                {
                	serializer.Serialize(new JsonTextWriter(writer), result);
                }

                string content = builder.ToString();

                provider.SetObject(cacheKey, content, DateTime.Now.AddMilliseconds(cacheDuration.TotalMilliseconds));

                return result;
            }

            using (var reader = new StringReader((string)result))
            {
                result = serializer.Deserialize(new JsonTextReader(reader), invocation.Method.ReturnType);
            }

            return result;
        }

        /// <summary>
        /// Returns hashcode for the current instance.
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = this.key.GetHashCode();
            for (int index = 0; index < arguments.Length; index++)
            {
                if (arguments[index] != null)
                {
                    hashCode += arguments[index].GetHashCode();
                }
            }

            return hashCode;
        }

        private readonly bool invalidated;
        private readonly string key;
        private readonly TimeSpan cacheDuration;
        private readonly object[] arguments;
    }
}
