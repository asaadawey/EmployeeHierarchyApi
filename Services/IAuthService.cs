using EmployeeHierarchyApi.DTOs;
using EmployeeHierarchyApi.Models;

namespace EmployeeHierarchyApi.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequest request);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        bool ValidateToken(string token);
        string GenerateJwtToken(User user);
    }
}