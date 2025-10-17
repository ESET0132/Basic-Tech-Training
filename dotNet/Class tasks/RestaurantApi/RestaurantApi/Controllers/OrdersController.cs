using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DbContext;
using RestaurantApi.Models.Entities;
using RestaurantApi.Models.DTOs;

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
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Menu)
                .ToListAsync();
        }

    
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderDto orderDto)
        {
            try
            {
                
                var restaurantExists = await _context.Restaurants.AnyAsync(r => r.Id == orderDto.RestaurantId);
                if (!restaurantExists)
                {
                    return BadRequest($"Restaurant with ID {orderDto.RestaurantId} does not exist");
                }

               
                var order = new Order
                {
                    CustomerName = orderDto.CustomerName,
                    CustomerPhone = orderDto.CustomerPhone,
                    CustomerEmail = orderDto.CustomerEmail,
                    OrderDate = orderDto.OrderDate,
                    TotalAmount = orderDto.TotalAmount,
                    Status = orderDto.Status,
                    RestaurantId = orderDto.RestaurantId
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

               
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

                return CreatedAtAction("GetOrders", new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}