using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Models;
using StudentManagementApi.Data;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Student.ToListAsync();
            return Ok(students);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if(student == null)
            {
                return NotFound("Student Record Not Found");
            }

            return Ok(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateData(Students student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateData), new {id = student.Id}, student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Students student)
        {
            if (id != student.Id)
                return BadRequest("Entry Not found");

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
                return NotFound();

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}