using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DbContext;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public TestController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var restaurantsCount = await _context.Restaurants.CountAsync();

                return Ok(new
                {
                    DatabaseExists = canConnect,
                    RestaurantsCount = restaurantsCount,
                    Message = "Database and tables are ready!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}