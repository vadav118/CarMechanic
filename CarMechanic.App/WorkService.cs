using CarMechanic.Shared;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic;


public class WorkService: IWorkService
{
    private readonly ILogger<WorkService> _logger;
    private readonly CarMechanicContext _dbContext;

    public WorkService(ILogger<WorkService> logger, CarMechanicContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task AddWork(Work work)
    {
        if (work is null)
        {
            return;
        }
        await _dbContext.Works.AddAsync(work);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Work Added: {@Work}", work);
    }

    public async Task DeleteWork(string workId)
    {   
        var work = await _dbContext.Works.FindAsync(workId);
        if (work is null)
        {
            return;
        }
        
        _dbContext.Works.Remove(work);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Work Deleted: {@Work}", work);
    }

    public async Task<List<Work>> GetAllWorks()
    {
        return await _dbContext.Works.ToListAsync();
    }

    public async Task<Work> GetWorkById(string id)
    {
        var work = await _dbContext.Works.FindAsync(id);
        return work;
    }

    public async Task UpdateWork(Work work)
    {
        var oldWork = await GetWorkById(work.WorkId);
        if (oldWork is not null)
        {
            oldWork.CustomerId = work.CustomerId;
            oldWork.LicensePlate = work.LicensePlate;
            oldWork.Category = work.Category;
            oldWork.Status = work.Status;
            oldWork.Fault = work.Fault;
            oldWork.ManufacturingDate = work.ManufacturingDate;
            oldWork.WorkDescription = work.WorkDescription;
        }
        _dbContext.Works.Update(oldWork);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Work Updated: {@Work}", work);
    }
}