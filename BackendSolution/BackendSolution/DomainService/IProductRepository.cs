﻿using BackendSolution.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Core.DomainService
{
    public interface IProductRepository
    {
        IEnumerable<Product> ReadAllProducts();
        List<Product> GetAllProducts();
        Product CreateProduct(Product product);
        Product GetProductByID(int id);
        Product Update(Product updateProduct);
        Product DeleteProduct(int id);
        Product GetProduct(long id);
        void AddProduct(Product addProduct);
        void EditProduct(Product editProduct);
        void RemoveProduct(long id);
    }
}
