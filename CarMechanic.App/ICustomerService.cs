using CarMechanic.Shared;

namespace CarMechanic;

public interface ICustomerService
{
    Task<Customer> GetCustomerId(string customerId);
    Task<List<Customer>> GetCustomers();
    Task AddCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task DeleteCustomer(string customerId);
}