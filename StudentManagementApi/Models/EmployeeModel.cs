using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required]
        public DateTime DateOfJoining { get; set; }
    }
}

