using CarMechanic.Shared;
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
    }
    
}