using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.Models;
using StudentManagementApi.Data;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddEmployee([FromForm] EmployeeModel empModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (empModel.DateOfJoining > DateTime.UtcNow)
            {
                return BadRequest("DateOfJoining cannot be in the future.");
            }

            empModel.Id = EmployeeData.EmpData.Any() ? EmployeeData.EmpData.Max(x => x.Id) + 1 : 1;
            EmployeeData.EmpData.Add(empModel);

            return CreatedAtAction(
                nameof(GetActiveEmployes),
                new { id = empModel.Id },
                empModel
            );
        }

        [HttpGet("active")]
        public IActionResult GetActiveEmployes()
        {
            var activeEmployees = EmployeeData.EmpData.Where(e => e.IsActive).ToList();

            return Ok(activeEmployees);
        }
    }
}