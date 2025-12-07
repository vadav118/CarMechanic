using CarMechanic.Shared;

namespace CarMechanic;

public interface ICustomerService
{
    Task<Customer> GetCustomerById(int customerId);
    Task<List<Customer>> GetCustomers();
    Task<List<Work>> GetWorks(int customerId);
    Task AddCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task DeleteCustomer(int customerId);
}