using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using AutoBox.Handlers;
using System.Globalization;

namespace AutoBox
{
    internal static class Utility
    {
        /// <summary>
        /// Cretes the unique md5 key from object hashcode.
        /// </summary>
        public static string GetUniqueKey(this CacheMethodHandler handler)
        {
            return string.Format("{0}_{1}", typeof(AutoBox).Namespace, CalculateMD5Hash(handler.GetHashCode().ToString(CultureInfo.CurrentCulture)));
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
