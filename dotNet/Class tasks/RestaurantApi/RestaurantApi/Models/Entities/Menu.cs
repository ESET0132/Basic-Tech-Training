using System.Text.Json.Serialization;

namespace RestaurantApi.Models.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public int RestaurantId { get; set; }

        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}