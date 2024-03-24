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
    public class Restaurant_DetailsController : ControllerBase
    {
        private readonly Restaurant_BookingDbContext _context;

        public Restaurant_DetailsController(Restaurant_BookingDbContext context)
        {
            _context = context;
        }

        // GET: api/restaurant_details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantDetails()
        {
            return await _context.Restaurant.ToListAsync();
        }

        // GET: api/restaurant_details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurantDetails(int id)
        {
            var restaurantDetails = await _context.Restaurant.FindAsync(id);

            if (restaurantDetails == null)
            {
                return NotFound();
            }

            return restaurantDetails;
        }

        // POST: api/restaurant_details
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurantDetails(Restaurant restaurantDetails)
        {
            if (await _context.Restaurant.AnyAsync(c => c.Email_Id == restaurantDetails.Email_Id))
            {
                return Conflict("Email Id already exists");
            }
            _context.Restaurant.Add(restaurantDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantDetails", new { id = restaurantDetails.Restaurant_Id }, restaurantDetails);
        }

        // PUT: api/restaurant_details/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantDetails(int id, Restaurant restaurantDetails)
        {
            if (id != restaurantDetails.Restaurant_Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurantDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantDetailsExists(id))
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

        // DELETE: api/restaurant_details/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurantDetails(int id)
        {
            var restaurantDetails = await _context.Restaurant.FindAsync(id);
            if (restaurantDetails == null)
            {
                return NotFound();
            }

            _context.Restaurant.Remove(restaurantDetails);
            await _context.SaveChangesAsync();

            return restaurantDetails;
        }

        private bool RestaurantDetailsExists(int id)
        {
            return _context.Restaurant.Any(e => e.Restaurant_Id == id);
        }
    }
}