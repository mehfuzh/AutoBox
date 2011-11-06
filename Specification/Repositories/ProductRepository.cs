using System;
using System.Collections.Generic;
using System.Linq;
using AutoBox.Specification.Repositories.Abstraction;

namespace AutoBox.Specification.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Product Create(string title, int quantity)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Invalid title for the product");

            var newProduct = new Product { Id = Guid.NewGuid(), Title = title, Quantity = quantity };
            
            Store.Add(newProduct);
            
            return newProduct;
        }

        public Product Get(Guid Id)
        {
            return Store.All.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IList<Product> GetAll()
        {
            return Store.All;
        }
    }
}
