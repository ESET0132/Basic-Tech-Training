using ProductManagementSystem.Models.DTOs;
using System.Threading.Tasks;

namespace ProductManagementSystem.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> AssignRoleAsync(string email, string roleName);
    }
}