using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.DTOs;
using StudentManagementApi.Services;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok("User Registered Successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
           var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
    }
}