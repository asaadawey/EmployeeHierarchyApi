using EmployeeHierarchyApi.Data;
using EmployeeHierarchyApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHierarchyApi.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    private readonly ApplicationDbContext _context = context;

    // Get all employees
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Manager)
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Manager)
            .Include(e => e.Subordinates)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    // Create and returns the created id
    public async Task<Employee> CreateAsync(Employee employee)
    {
        employee.CreatedAt = DateTime.UtcNow;
        employee.UpdatedAt = DateTime.UtcNow;

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(employee.Id) ?? employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        employee.UpdatedAt = DateTime.UtcNow;

        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return await GetByIdAsync(employee.Id) ?? employee;
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            // Update subordinates to have no manager
            var subordinates = await _context.Employees
                .Where(e => e.ManagerId == id)
                .ToListAsync();

            foreach (var subordinate in subordinates)
            {
                subordinate.ManagerId = employee.ManagerId;
                subordinate.UpdatedAt = DateTime.UtcNow;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Employee>> GetSubordinatesAsync(int managerId)
    {
        return await _context.Employees
            .Where(e => e.ManagerId == managerId)
            .Include(e => e.Subordinates)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetManagementChainAsync(int employeeId)
    {
        var employeeList = new List<Employee>();
        var current = await GetByIdAsync(employeeId);

        // Keep looping until i find the least employee position
        while (current != null)
        {
            employeeList.Add(current);
            current = current.ManagerId.HasValue ? await GetByIdAsync(current.ManagerId.Value) : null;
        }

        return employeeList;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Employees.AnyAsync(e => e.Id == id);
    }

    // Check if it will create circulate refrence
    public async Task<bool> WouldCreateCycleAsync(int employeeId, int? newManagerId)
    {
        if (!newManagerId.HasValue) return false;

        var managementChain = await GetManagementChainAsync(newManagerId.Value);
        return managementChain.Any(e => e.Id == employeeId);
    }
}