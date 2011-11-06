using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox;

namespace AutoBox.Specification.Repositories.Abstraction
{
    public interface IProductRepository
    {
        Product Create(string title, int quantity);
        Product Get(Guid Id);
        IList<Product> GetAll();
    }
}
