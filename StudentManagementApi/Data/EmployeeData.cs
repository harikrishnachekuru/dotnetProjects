using StudentManagementApi.Models;

namespace StudentManagementApi.Data
{
    public static class EmployeeData
    {
        public static List<EmployeeModel> EmpData = new()
        {
            new EmployeeModel
            {
                Id = 1,
                Name = "Krishna",
                Email = "krishna@gmail.com",
                IsActive = true,
                DateOfJoining = DateTime.UtcNow.AddYears(-1)
            }
        };
    }
}