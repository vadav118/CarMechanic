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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Work>> GetWorkById(int id)
    {
        var work = await _workService.GetWorkById(id);
        if (work is null)
        {
            return NotFound();
        }
        return Ok(work);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Work work)
    {
        var existingWork = await _workService.GetWorkById(work.Id);
        if (existingWork is not null)
        {
            return Conflict();
        }
        await _workService.AddWork(work);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Work work)
    {
        if (id != work.Id)
        {
            return  BadRequest();
        }

        var oldWork = await _workService.GetWorkById(id);

        if (oldWork is null)
        {
            return NotFound();
        }

        await _workService.UpdateWork(work);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWork(int id)
    {
        var existingWork = await _workService.GetWorkById(id);
        if (existingWork is null)
        {
            return NotFound();
        }
        await _workService.DeleteWork(existingWork.Id);
        return Ok();
    }
}