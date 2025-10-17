using System.Text.Json.Serialization;

namespace RestaurantApi.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;

        [JsonIgnore]
        public virtual Menu Menu { get; set; } = null!;
    }
}