using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlApi;
using BO;

namespace DroneDeliveryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IBL _bl;

        public CustomerController(IBL bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerToList>> GetCustomers()
        {
            try
            {
                return Ok(_bl.GetAllCustomers(_ => true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            try
            {
                _bl.AddCustomer(customer);
                return StatusCode(201, "Customer added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding customer: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest("Customer ID in URL does not match ID in body.");
                }
                _bl.UpdateCustomer(customer);
                return Ok("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating customer: {ex.Message}");
            }
        }
    }
}