using StudentManagementApi.DTOs;

namespace StudentManagementApi.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}