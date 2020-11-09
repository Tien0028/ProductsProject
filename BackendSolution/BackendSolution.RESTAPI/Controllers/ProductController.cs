using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSolution.Core.ApplicationService;
using BackendSolution.Core.DomainService;
using BackendSolution.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendSolution.RESTAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _proRepo;

        public ProductController(IProductService productService, IProductRepository proRepo)
        {
            _productService = productService;
            _proRepo = proRepo;
        }

        //GET: api/Product
       //[Authorize]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _productService.GetProducts();
        }

        // GET api/Product/5
        //[Authorize(Roles = "Adminstrator")]
        [HttpGet("{id}", Name ="Get")]
        public IActionResult Get(long id)
        {
            //return _productService.ReadProductById(id);
            var pro = _proRepo.GetProduct(id);
            if(pro == null)
            {
                return NotFound();
            }
            return new ObjectResult(pro);

        }

        // POST api/Product
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Post([FromBody] Product pro)
        {
           if(pro == null)
            {
                return BadRequest();
            }
            _proRepo.AddProduct(pro);

            return CreatedAtRoute("Get", new { id = pro.ID }, pro);
        }

        // DELETE api/ApiWithActions/5
        //[Authorize(Roles ="Administrator")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //return _productService.Delete(id);
            var pro = _proRepo.GetProduct(id);
            if(pro == null)
            {
                return NotFound();
            }
            _proRepo.RemoveProduct(id);
            return new NoContentResult();
        }

    }
}
