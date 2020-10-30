using BackendSolution.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Core.ApplicationService
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        Product CreateProduct(string name, double price, string color, string type, DateTime createDate);
        Product Create(Product product);
        Product Update(Product product);
        Product Delete(int id);
        Product ReadProductById(int id);

    }
}
