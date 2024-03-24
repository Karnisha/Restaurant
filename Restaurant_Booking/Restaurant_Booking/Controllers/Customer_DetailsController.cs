using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Booking.Data;
using Restaurant_Booking.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer_DetailsController : ControllerBase
    {
        private readonly Restaurant_BookingDbContext _context;

        public Customer_DetailsController(Restaurant_BookingDbContext context)
        {
            _context = context;
        }

        // GET: api/customer_details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerDetails()
        {
            return await _context.Customer.ToListAsync();
        }

        // GET: api/customer_details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerDetails(int id)
        {
            var customerDetails = await _context.Customer.FindAsync(id);

            if (customerDetails == null)
            {
                return NotFound();
            }

            return customerDetails;
        }

        // POST: api/customer_details
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomerDetails(Customer customerDetails)
        {
            if (await _context.Customer.AnyAsync(c => c.Customer_Name == customerDetails.Customer_Name))
            {
                return Conflict("Username already exists");
            }
            _context.Customer.Add(customerDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerDetails", new { id = customerDetails.Customer_Id }, customerDetails);
        }

        // PUT: api/customer_details/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDetails(int id, Customer customerDetails)
        {
            if (id != customerDetails.Customer_Id)
            {
                return BadRequest();
            }

            _context.Entry(customerDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/customer_details/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomerDetails(int id)
        {
            var customerDetails = await _context.Customer.FindAsync(id);
            if (customerDetails == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customerDetails);
            await _context.SaveChangesAsync();

            return customerDetails;
        }

        private bool CustomerDetailsExists(int id)
        {
            return _context.Customer.Any(e => e.Customer_Id == id);
        }
    }
}