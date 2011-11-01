using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBox.Specification
{
    internal static class Store
    {
        public static IList<Product> All
        {
            get
            {
                Init();
                return products;
            }
        }

        public static void Clear()
        {
            products.Clear();
        }

        public static void Add(Product product)
        {
            Init();
            products.Add(product);
        }

        static void Init()
        {
            if (products == null)
                products = new List<Product>();
        }

        [ThreadStatic]
        private static IList<Product> products = new List<Product>();
    }
}
