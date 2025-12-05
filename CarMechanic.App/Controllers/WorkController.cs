using CarMechanic.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanic.Controllers;

[ApiController]
[Route("[controller]")]

public class WorkController: ControllerBase
{
    private readonly IWorkService _workService;
    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Work>>> GetAllWorks()
    {
        var works = await _workService.GetAllWorks();
        return Ok(works);
    }
    
    [HttpGet("{id}")]
    
}