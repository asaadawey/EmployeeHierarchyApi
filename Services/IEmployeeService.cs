using EmployeeHierarchyApi.DTOs;

namespace EmployeeHierarchyApi.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto);
    Task<EmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto);
    Task DeleteEmployeeAsync(int id);
    Task<IEnumerable<EmployeeDto>> GetSubordinatesAsync(int managerId);
    Task<IEnumerable<EmployeeDto>> GetManagementChainAsync(int employeeId);
}