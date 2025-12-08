using System.Net.Http.Json;
using CarMechanic.Shared;

namespace CarMechanic.UI.Services;

public class WorkService: IWorkService
{
    private readonly HttpClient _httpClient;
    public WorkService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Work>> GetAllWorks()
    {
        return await _httpClient.GetFromJsonAsync<List<Work>>("api/Work");
    }

    public async Task<Work> GetWorkById(int workId)
    {
        return await _httpClient.GetFromJsonAsync<Work>($"api/Work/{workId}");
    }
    
    public async Task AddWork(Work work)
    {
        await _httpClient.PostAsJsonAsync("api/Work", work);
    }
    
    public async Task UpdateWork(int id, Work work)
    {
        await _httpClient.PutAsJsonAsync($"api/Work/{id}", work);
    }

    public async Task DeleteWork(int workId)
    {
        await _httpClient.DeleteAsync($"api/Work/{workId}");
    }
}