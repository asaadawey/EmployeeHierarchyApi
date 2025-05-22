using EmployeeHierarchyApi.Data.Entities;

namespace EmployeeHierarchyApi.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> CreateAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<IEnumerable<Employee>> GetSubordinatesAsync(int managerId);
    Task<IEnumerable<Employee>> GetManagementChainAsync(int employeeId);
    Task<bool> ExistsAsync(int id);
    Task<bool> WouldCreateCycleAsync(int employeeId, int? newManagerId);
}