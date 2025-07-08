using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public DepartmentController(EmployeeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all departments.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        /// <summary>
        /// Get a department by ID.
        /// </summary>
        /// <param name="id">Department ID</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        /// <summary>
        /// Create a new department.
        /// </summary>
        /// <param name="department">Department object</param>
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
        }

        /// <summary>
        /// Update an existing department.
        /// </summary>
        /// <param name="id">Department ID</param>
        /// <param name="department">Updated department object</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a department by ID.
        /// </summary>
        /// <param name="id">Department ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
