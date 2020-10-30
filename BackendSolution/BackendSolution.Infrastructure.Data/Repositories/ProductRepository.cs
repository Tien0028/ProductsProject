using BackendSolution.Core.DomainService;
using BackendSolution.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackendSolution.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BackendContext _btx;
        public static int ProductID = 1;

        private static List<Product> _listOfProducts = new List<Product>();

        public ProductRepository(BackendContext btx)
        {
            _btx = btx;
        }

        public Product CreateProduct(Product product)
        {
            Product pro = _btx.Add(product).Entity;
            _btx.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            Product pro = GetAllProducts().Find(p => p.ID == id);
            GetAllProducts().Remove(pro);
            if(pro != null)
            {
                return pro;
            }
            return null;
        }

        public List<Product> GetAllProducts()
        {
            return _btx.Products.ToList();
        }

        public Product GetProductByID(int id)
        {
            return _btx.Products.FirstOrDefault(pro => pro.ID == id);
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            return _listOfProducts;
        }

        public Product Update(Product updateProduct)
        {
            var pro = GetProductByID(updateProduct.ID);
            if(pro != null)
            {
                pro.Name = updateProduct.Name;
                pro.Price = updateProduct.Price;
                pro.Color = updateProduct.Color;
                pro.Type = updateProduct.Type;
                pro.CreatedDate = pro.CreatedDate;

            }
            return pro;
        }
    }
}
