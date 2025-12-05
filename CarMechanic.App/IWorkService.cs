using CarMechanic.Shared;

namespace CarMechanic;


public interface IWorkService
{
    Task<Work> GetWorkById(string workId);
    Task<List<Work>> GetAllWorks();
    Task AddWork(Work work);
    Task UpdateWork(Work work);
    Task DeleteWork(string workId);
}