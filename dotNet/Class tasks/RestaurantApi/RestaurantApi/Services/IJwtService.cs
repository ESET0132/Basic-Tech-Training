using RestaurantApi.Models.Entities;

namespace RestaurantApi.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
    }
}
