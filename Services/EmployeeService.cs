
using EmployeeHierarchyApi.Data.Entities;
using EmployeeHierarchyApi.DTOs;
using EmployeeHierarchyApi.Repositories;
using EmployeeHierarchyApi.Services;

namespace EmployeeHierarchyApi.Services;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    private readonly IEmployeeRepository _repository = repository;

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _repository.GetAllAsync();
        return employees.Select(MapToDto);
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto)
    {
        // Validate manager exists if provided
        if (createDto.ManagerId.HasValue && !await _repository.ExistsAsync(createDto.ManagerId.Value))
        {
            throw new ArgumentException("Manager not found");
        }

        var employee = new Employee
        {
            Name = createDto.Name,
            Title = createDto.Title,
            ManagerId = createDto.ManagerId
        };

        var created = await _repository.CreateAsync(employee);
        return MapToDto(created);
    }

    public async Task<EmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new ArgumentException("Employee not found");
        }

        // Validate manager exists if provided
        if (updateDto.ManagerId.HasValue && !await _repository.ExistsAsync(updateDto.ManagerId.Value))
        {
            throw new ArgumentException("Manager not found");
        }

        // Check for circular reference
        if (await _repository.WouldCreateCycleAsync(id, updateDto.ManagerId))
        {
            throw new ArgumentException("Cannot create circular reference in hierarchy");
        }

        employee.Name = updateDto.Name;
        employee.Title = updateDto.Title;
        employee.ManagerId = updateDto.ManagerId;

        var updated = await _repository.UpdateAsync(employee);
        return MapToDto(updated);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
        {
            throw new ArgumentException("Employee not found");
        }

        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EmployeeDto>> GetSubordinatesAsync(int managerId)
    {
        var subordinates = await _repository.GetSubordinatesAsync(managerId);
        return subordinates.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetManagementChainAsync(int employeeId)
    {
        var chain = await _repository.GetManagementChainAsync(employeeId);
        return chain.Select(MapToDto);
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Title = employee.Title,
            ManagerId = employee.ManagerId,
            ManagerName = employee.Manager?.Name,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        };
    }
}
