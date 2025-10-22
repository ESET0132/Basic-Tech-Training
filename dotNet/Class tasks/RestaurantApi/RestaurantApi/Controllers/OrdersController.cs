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
    public class OrdersController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public OrdersController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Menu)
                .ToListAsync();
        }

        [HttpGet("my-orders")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user token" });
            }

            var orders = await _context.Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.Restaurant)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Menu)
                .ToListAsync();

            return orders;
        }

        [HttpGet("restaurant/{restaurantId}")]
        [Authorize(Roles = "RestaurantOwner,Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetRestaurantOrders(int restaurantId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user token" });
            }

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Check if user owns the restaurant (unless they're admin)
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);
            if (restaurant == null)
            {
                return NotFound("Restaurant not found");
            }

            if (userRole != "Admin" && restaurant.OwnerId != userId)
            {
                return Forbid("You can only view orders for your own restaurants");
            }

            var orders = await _context.Orders
                .Where(o => o.RestaurantId == restaurantId)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Menu)
                .ToListAsync();

            return orders;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderDto orderDto)
        {
            try
            {
                // Get current user ID from JWT token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { message = "Invalid user token" });
                }

                // Check if restaurant exists
                var restaurantExists = await _context.Restaurants.AnyAsync(r => r.Id == orderDto.RestaurantId);
                if (!restaurantExists)
                {
                    return BadRequest($"Restaurant with ID {orderDto.RestaurantId} does not exist");
                }

                // Create order
                var order = new Order
                {
                    CustomerName = orderDto.CustomerName,
                    CustomerPhone = orderDto.CustomerPhone,
                    CustomerEmail = orderDto.CustomerEmail,
                    OrderDate = orderDto.OrderDate,
                    TotalAmount = orderDto.TotalAmount,
                    Status = orderDto.Status,
                    RestaurantId = orderDto.RestaurantId,
                    CustomerId = userId // Link to authenticated user
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Add order items
                foreach (var itemDto in orderDto.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice,
                        OrderId = order.Id,
                        MenuId = itemDto.MenuId
                    };
                    _context.OrderItems.Add(orderItem);
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMyOrders", new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}