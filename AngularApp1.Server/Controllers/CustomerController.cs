using AngularApp1.Server.Entities;
using AngularApp1.Server.Services;
using AngularApp1.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _CustomerService;

        public CustomerController(CustomerService CustomerService)
        {
            _CustomerService = CustomerService;
        }

        // Get a Customer by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var Customer = await _CustomerService.GetCustomer(id);
            if (Customer == null)
            {
                return NotFound(); 
            }
            return Ok(Customer); 
        }

        // Get all Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var Customers = await _CustomerService.GetList();
            return Ok(Customers); 
        }

        // Update a Customer
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var result = await _CustomerService.UpdateCustomer(model);
            if (result > 0)
            {
                return NoContent(); 
            }
            return NotFound(); 
        }

        // Add a new Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var Customer = await _CustomerService.AddCustomer(model);
            if (Customer != null)
            {
                return CreatedAtAction(nameof(Get), new { id = Customer.Id }, Customer); 
            }
            return BadRequest("Customer creation failed."); 
        }

        // Delete a Customer by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _CustomerService.DeleteCustomer(id);
            if (result > 0)
            {
                return NoContent(); 
            }
            return NotFound(); 
        }
    }
}
