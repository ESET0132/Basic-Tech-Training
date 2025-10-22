using System.Text.Json.Serialization;

namespace RestaurantApi.Models.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime OpeningDate { get; set; }
        public int OwnerId { get; set; } // Foreign key to User

        // Navigation properties
        [JsonIgnore]
        public virtual User Owner { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}