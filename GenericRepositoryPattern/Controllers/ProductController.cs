using GenericRepositoryPattern.Entities;
using GenericRepositoryPattern.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllCustomers()
        {
            return Ok(await _productRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<ActionResult<Product>> GetProductById(int productId) 
        {
            return Ok(await _productRepository.GetByIdAsync(productId));
        } 

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            _productRepository.Add(product);

            return Ok(await _productRepository.SaveChangesAsync());
        }

        [HttpGet]
        [Route("{productName}")]
        public async Task<ActionResult<Customer>> GetProductByName(string productName)
        {
            return Ok(await _productRepository.FindByConditionAsync(a =>a.Name == productName));
        }
    }
}
