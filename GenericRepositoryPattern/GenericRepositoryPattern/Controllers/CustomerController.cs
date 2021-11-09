using GenericRepositoryPattern.Entities;
using GenericRepositoryPattern.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepositoryPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private IRepository<Customer> _customerRepository;

        public CustomerController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            return Ok(await _customerRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int customerId) 
        {
            return Ok(await _customerRepository.GetByIdAsync(customerId));
        } 

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            _customerRepository.Add(customer);

            return Ok(await _customerRepository.SaveChangesAsync());
        }

        [HttpGet]
        [Route("{customerName}")]
        public async Task<ActionResult<Customer>> GetCustomerByName(string customerName)
        {
            return Ok(await _customerRepository.FindByConditionAsync(a =>a.Name == customerName));
        }
    }
}
