using System.Net.Http.Json;
using CarMechanic.Shared;

namespace CarMechanic.UI;

public class WorkService: IWorkService
{
    private readonly HttpClient _httpClient;
    public WorkService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Work>> GetAllWorks()
    {
        return await _httpClient.GetFromJsonAsync<List<Work>>("/work");
    }

    public async Task<Work> GetWorkById(int workId)
    {
        return await _httpClient.GetFromJsonAsync<Work>($"/work/{workId}");
    }
    
    public async Task AddWork(Work work)
    {
        await _httpClient.PostAsJsonAsync("/works", work);
    }
    
    public async Task UpdateWork(int id, Work work)
    {
        await _httpClient.PutAsJsonAsync($"/works/{id}", work);
    }

    public async Task DeleteWork(int workId)
    {
        await _httpClient.DeleteAsync($"/works/{workId}");
    }
}