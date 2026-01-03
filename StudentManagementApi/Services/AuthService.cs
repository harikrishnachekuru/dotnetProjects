using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Models;
using StudentManagementApi.Data;
using StudentManagementApi.DTOs;

namespace StudentManagementApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly JwtService _jwtService;

        public AuthService(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var exists = await _context.Users.AnyAsync(x => x.UserName == dto.UserName);

            if (exists)
            {
                throw new Exception("UserName already exists");
            }

            var user = new User
            {
                UserName = dto.UserName
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == dto.UserName);

            if (user == null)
                throw new Exception("Invalid UserName or Password");

            var result = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    dto.Password
                );

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid UserName or Password");

            return _jwtService.GenerateToken(user);
        }
    }
}