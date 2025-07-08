using EmployeeUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeUI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> SearchAsync(string name, string department);
    }
}
