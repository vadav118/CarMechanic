using System.Net.Http.Json;
using CarMechanic.Shared;

namespace CarMechanic.UI;

public class CustomerService: ICustomerService
{
    private readonly HttpClient _httpClient;
    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _httpClient.GetFromJsonAsync<List<Customer>>("/Customer");
    }
    
    public async Task<List<Work>> GetWorksByCustomerId(int customerId)
    {
        return await _httpClient.GetFromJsonAsync<List<Work>>($"/Customer/{customerId}");
    }

    public async Task<Customer> GetCustomerById(int customerId)
    {
        return await _httpClient.GetFromJsonAsync<Customer>($"/Customer/{customerId}");
    }

    public async Task AddCustomer(Customer customer)
    {
        await _httpClient.PostAsJsonAsync("/Customer", customer);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        await _httpClient.PutAsJsonAsync("/Customer", customer);
    }

    public async Task DeleteCustomer(int customerId)
    {
        await _httpClient.DeleteAsync($"/Customer/{customerId}");
    }
}