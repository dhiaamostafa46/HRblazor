using HR.Data;
using HR.Models;
using Microsoft.EntityFrameworkCore;

namespace HR.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<List<Employee>> GetEmployeesAsync()
        //{
        //    // Fetch employees from the database using EF Core
        //    return await _context.Employees.ToListAsync(); // Assuming your DbSet<Employee> is named Employees
        //}

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var emps = await _context.Employees.ToListAsync();
            return emps;
        }
              public async Task<List<Employee>> GetAllWithRequestsAsync()
        {
            var emps = await _context.Employees
                .Include(e=>e.LeaveRequests)
                .ThenInclude(l=>l.LeaveType)
                .ToListAsync();
            return emps;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
          
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
        }
    }
}
