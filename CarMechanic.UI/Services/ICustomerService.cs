using CarMechanic.Shared;

namespace CarMechanic.UI;

public interface ICustomerService
{
    Task<Customer> GetCustomerById(int customerId);
    Task<List<Customer>> GetAllCustomers();
    Task<List<Work>> GetWorksByCustomerId(int customerId);
    Task AddCustomer(Customer customer);
    Task UpdateCustomer(int id,Customer customer);
    Task DeleteCustomer(int customerId);
}