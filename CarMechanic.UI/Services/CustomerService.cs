using System.Net.Http.Json;
using CarMechanic.Shared;

namespace CarMechanic.UI.Services;

public class CustomerService: ICustomerService
{
    private readonly HttpClient _httpClient;
    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customer");
    }
    
    public async Task<List<Work>> GetWorksByCustomerId(int customerId)
    {
        return await _httpClient.GetFromJsonAsync<List<Work>>($"api/Customer/{customerId}/works");
    }

    public async Task<Customer> GetCustomerById(int customerId)
    {
        return await _httpClient.GetFromJsonAsync<Customer>($"api/Customer/{customerId}");
    }

    public async Task AddCustomer(Customer customer)
    {
        await _httpClient.PostAsJsonAsync("api/Customer", customer);
    }

    public async Task UpdateCustomer(int id, Customer customer)
    {
        await _httpClient.PutAsJsonAsync($"api/Customer/{id}", customer);
    }

    public async Task DeleteCustomer(int customerId)
    {
        await _httpClient.DeleteAsync($"api/Customer/{customerId}");
    }
}