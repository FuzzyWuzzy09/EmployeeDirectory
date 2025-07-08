using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public AttendanceController(EmployeeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mark attendance for an employee.
        /// </summary>
        /// <param name="attendance">Attendance object containing EmployeeId, Date, and Status</param>
        [HttpPost]
        public async Task<ActionResult<Attendance>> MarkAttendance(Attendance attendance)
        {
            var employeeExists = await _context.Employees.AnyAsync(e => e.EmployeeId == attendance.EmployeeId);
            if (!employeeExists)
            {
                return BadRequest("Invalid Employee ID");
            }
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return Created("", attendance); // or just return Ok(attendance);
        }

        /// <summary>
        /// Get attendance records for an employee within a date range.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="from">Start date of the range</param>
        /// <param name="to">End date of the range</param>
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance(int employeeId,[FromQuery] DateTime from,[FromQuery] DateTime to)
        {
            var records = await _context.Attendances.Where(a => a.EmployeeId == employeeId && a.Date >= from && a.Date <= to).ToListAsync();
            return records;
        }
    }
}
