using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}