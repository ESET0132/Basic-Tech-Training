namespace RestaurantApi.Models.DTOs
{
    public class CreateOrderItemDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int MenuId { get; set; }
    }
}
