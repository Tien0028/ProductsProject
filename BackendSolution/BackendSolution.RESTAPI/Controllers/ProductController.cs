using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSolution.Core.ApplicationService;
using BackendSolution.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BackendSolution.RESTAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get() => _productService.GetProducts();

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return _productService.ReadProductById(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product pro)
        {
            return _productService.Create(pro);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            return _productService.Delete(id);
        }

    }
}
