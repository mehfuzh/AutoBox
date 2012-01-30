using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using AutoBox.Handlers;
using System.Globalization;
using AutoBox.Abstraction;

namespace AutoBox
{
    internal static class Utility
    {
        /// <summary>
        /// Cretes the unique md5 key from object hashcode.
        /// </summary>
        public static string GetUniqueKey(this CacheMethodHandler handler, string prefix)
        {
            return string.Format("{0}+{1}", prefix, CalculateMD5Hash(handler.GetHashCode().ToString(CultureInfo.CurrentCulture)));
        }

        /// <summary>
        /// Checks if the service is new.
        /// </summary>
        public static bool IsNewService(this Type serviceType, IContainer container)
        {
            return serviceType.IsInterface && container.Resolve(serviceType) == null;
        }

        public static string CalculateMD5Hash(this string input)
        {
            // step 1, calculate MD5 hash from input
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);
                // step 2, convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
