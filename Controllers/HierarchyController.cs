using EmployeeHierarchyApi.DTOs;
using EmployeeHierarchyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHierarchyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HierarchyController : ControllerBase
{
    private readonly IHierarchyService _hierarchyService;

    public HierarchyController(IHierarchyService hierarchyService)
    {
        _hierarchyService = hierarchyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HierarchyNodeDto>>> GetHierarchy()
    {
        var hierarchy = await _hierarchyService.GetHierarchyTreeAsync();
        return Ok(hierarchy);
    }

    [HttpPost("move")]
    public async Task<IActionResult> MoveEmployee([FromBody] MoveEmployeeDto moveDto)
    {
        try
        {
            await _hierarchyService.MoveEmployeeAsync(moveDto.EmployeeId, moveDto.NewManagerId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class MoveEmployeeDto
{
    public int EmployeeId { get; set; }
    public int? NewManagerId { get; set; }
}