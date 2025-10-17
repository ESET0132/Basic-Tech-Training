using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DbContext;
using RestaurantApi.Models.Entities;
using RestaurantApi.Models.DTOs;

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
        public async Task<ActionResult> CreateMenu([FromBody] CreateMenuDto menuRequest)
        {
            try
            {
           
                var menu = new Menu
                {
                    Name = menuRequest.Name,
                    Description = menuRequest.Description,
                    Price = menuRequest.Price,
                    Category = menuRequest.Category,
                    IsAvailable = menuRequest.IsAvailable,
                    RestaurantId = menuRequest.RestaurantId
                 
                };

           
                var restaurantExists = await _context.Restaurants.AnyAsync(r => r.Id == menu.RestaurantId);
                if (!restaurantExists)
                {
                    return BadRequest($"Restaurant with ID {menu.RestaurantId} does not exist");
                }

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
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _context.Menus.ToListAsync();
            return Ok(menus);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }
    }
}