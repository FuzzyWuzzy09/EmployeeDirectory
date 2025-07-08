using EmployeeUI.Models;

namespace EmployeeUI.Services
{
    public interface IAttendanceService
    {
        Task<bool> MarkAttendanceAsync(AttendanceEntry entry);
        Task<List<Attendance>> GetFilteredAsync(AttendanceFilter filter);
    }

}
