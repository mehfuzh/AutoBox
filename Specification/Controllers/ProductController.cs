using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Specification.Abstraction;

namespace AutoBox.Specification.Controllers
{
    public class ProductController
    {
        private readonly IProductRepository repository;
        private readonly IAccountController accountRepo;

        public ProductController(IProductRepository repository , IAccountController accountRepo)
        {
            this.repository = repository;
            this.accountRepo = accountRepo;
        }

        public bool Initalized()
        {
            return repository != null && accountRepo != null;
        }

        public Product Create(string title, int quantity)
        {
            return repository.Create(title, quantity);
        }

        public IList<Product> GetAllProducts()
        {
            return repository.GetAll();
        }
    }
}
