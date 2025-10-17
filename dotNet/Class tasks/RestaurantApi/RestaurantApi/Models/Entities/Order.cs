using System.Text.Json.Serialization;

namespace RestaurantApi.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public int RestaurantId { get; set; }

        [JsonIgnore]
        public virtual Restaurant Restaurant { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}