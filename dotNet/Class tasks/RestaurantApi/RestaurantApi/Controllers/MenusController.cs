using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DbContext;
using RestaurantApi.Models.Entities;
using RestaurantApi.Models.DTOs;
using System.Security.Claims;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public MenusController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "RestaurantOwner,Admin")]
        public async Task<ActionResult> CreateMenu([FromBody] CreateMenuDto menuRequest)
        {
            try
            {
                // Get current user ID from JWT token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { message = "Invalid user token" });
                }

                // Check if restaurant exists and user owns it (or is admin)
                var restaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => r.Id == menuRequest.RestaurantId);

                if (restaurant == null)
                {
                    return BadRequest($"Restaurant with ID {menuRequest.RestaurantId} does not exist");
                }

                // Check if user owns the restaurant (unless they're admin)
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                if (userRole != "Admin" && restaurant.OwnerId != userId)
                {
                    return Forbid("You can only add menus to your own restaurants");
                }

                var menu = new Menu
                {
                    Name = menuRequest.Name,
                    Description = menuRequest.Description,
                    Price = menuRequest.Price,
                    Category = menuRequest.Category,
                    IsAvailable = menuRequest.IsAvailable,
                    RestaurantId = menuRequest.RestaurantId
                };

                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Menu created successfully",
                    id = menu.Id,
                    name = menu.Name,
                    price = menu.Price
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _context.Menus
                .Include(m => m.Restaurant)
                .ToListAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menus
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpGet("restaurant/{restaurantId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenusByRestaurant(int restaurantId)
        {
            var menus = await _context.Menus
                .Where(m => m.RestaurantId == restaurantId)
                .Include(m => m.Restaurant)
                .ToListAsync();
            return Ok(menus);
        }
    }
}