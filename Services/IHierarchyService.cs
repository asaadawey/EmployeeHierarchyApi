using EmployeeHierarchyApi.DTOs;

namespace EmployeeHierarchyApi.Services;

public interface IHierarchyService
{
    Task<IEnumerable<HierarchyNodeDto>> GetHierarchyTreeAsync();
    Task MoveEmployeeAsync(int employeeId, int? newManagerId);
}