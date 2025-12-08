using CarMechanic.Shared;

namespace CarMechanic.UI;

public interface IWorkService
{   
    Task<List<Work>> GetAllWorks();
    Task<Work> GetWorkById(int workId);
    Task AddWork(Work work);
    Task UpdateWork(int id,Work work);
    Task DeleteWork(int workId);
}