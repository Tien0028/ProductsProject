using BackendSolution.Core.DomainService;
using BackendSolution.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackendSolution.Core.ApplicationService.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _proRepo;

        public ProductService(IProductRepository proRepo)
        {
            _proRepo = proRepo;
        }

        public Product Create(Product product)
        {
            if(product.Name == null || product.Name.Length < 1)
            {
                throw new InvalidDataException("You failed, dumbass!");
            }
            return _proRepo.CreateProduct(product);
        }

        public Product CreateProduct(string name, double price, string color, string type, DateTime createDate)
        {
            var product = new Product()
            {
                Name = name,
                Color = color,
                Type = type,
                CreatedDate = createDate,
                Price = price
            };
            return product;
        }

        public Product Delete(int id)
        {
            if (id < 1)
            {
                throw new InvalidDataException("ID must be higher than one! Otherwise, you failed!");
            }
            return _proRepo.DeleteProduct(id);
        }

        public List<Product> GetProducts()
        {
            return _proRepo.GetAllProducts();
        }

        public Product ReadProductById(int id)
        {
            return _proRepo.GetProductByID(id);
        }

        public Product Update(Product product)
        {
            if(product.Name.Length < 1)
            {
                throw new InvalidDataException("Name must contain 1 character");
            }
            if(product == null)
            {
                throw new InvalidDataException("Could not find anything matching the id" + product.ID);
            }
            return _proRepo.Update(product);
        }
    }
}
