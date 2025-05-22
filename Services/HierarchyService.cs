using EmployeeHierarchyApi.DTOs;
using EmployeeHierarchyApi.Repositories;

namespace EmployeeHierarchyApi.Services;

public class HierarchyService : IHierarchyService
{
    private readonly IEmployeeRepository _repository;

    public HierarchyService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    // Create tree
    public async Task<IEnumerable<HierarchyNodeDto>> GetHierarchyTreeAsync()
    {
        var allEmployees = await _repository.GetAllAsync();

        // Build dictionary for all employees
        var employeeDict = allEmployees.ToDictionary(e => e.Id, e => new HierarchyNodeDto
        {
            Id = e.Id,
            Name = e.Name,
            Title = e.Title,
            ManagerId = e.ManagerId,
            Subordinates = new List<HierarchyNodeDto>()
        });

        var rootNodes = new List<HierarchyNodeDto>();

        foreach (var employee in allEmployees)
        {
            var node = employeeDict[employee.Id];

            if (employee.ManagerId.HasValue && employeeDict.ContainsKey(employee.ManagerId.Value))
            {
                employeeDict[employee.ManagerId.Value].Subordinates.Add(node);
            }
            else
            {
                rootNodes.Add(node);
            }
        }

        return rootNodes;
    }

    public async Task MoveEmployeeAsync(int employeeId, int? newManagerId)
    {
        var employee = await _repository.GetByIdAsync(employeeId);
        if (employee == null)
        {
            throw new ArgumentException("Employee not found");
        }

        if (newManagerId.HasValue && !await _repository.ExistsAsync(newManagerId.Value))
        {
            throw new ArgumentException("New manager not found");
        }

        if (await _repository.WouldCreateCycleAsync(employeeId, newManagerId))
        {
            throw new ArgumentException("Cannot create circular reference in hierarchy");
        }

        employee.ManagerId = newManagerId;
        await _repository.UpdateAsync(employee);
    }
}