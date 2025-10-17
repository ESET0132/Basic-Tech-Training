namespace RestaurantApi.Models.DTOs
{
    public class CreateMenuDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public int RestaurantId { get; set; }
    }
}
