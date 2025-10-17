namespace RestaurantApi.Models.DTOs
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public int RestaurantId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
}
