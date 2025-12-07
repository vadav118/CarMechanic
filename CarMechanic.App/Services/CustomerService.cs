using CarMechanic.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace CarMechanic;

public class CustomerService: ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly CarMechanicContext _dbContext;
    
    public CustomerService(ILogger<CustomerService> logger, CarMechanicContext dbContext)
        {
        _logger = logger;
        _dbContext = dbContext;
        }

    public async Task AddCustomer(Customer customer)
    {
        if (customer is null)
        {
            return;
        }

        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Customer added {@Customer}",customer);
    }

    public async Task DeleteCustomer(int customerId)
    {
        var customer = await _dbContext.Customers.FindAsync(customerId);
        if (customer is null)
        {
            return;
        }
        
        _dbContext.Works.RemoveRange(_dbContext.Works.Where(W => W.CustomerId == customer.Id).ToList());
        _dbContext.Customers.Remove(customer);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Customer removed {@Customer}",customer);
    }
    
    public async Task<List<Customer>> GetCustomers()
    {
        return await _dbContext.Customers.ToListAsync();
    }
    public async Task<Customer> GetCustomerById(int customerId)
    {
        var customer = await _dbContext.Customers.FindAsync(customerId);  
        return customer;
    }

    public async Task<List<Work>> GetWorks(int  customerId)
    {
        var works = await _dbContext.Works.Where(W => W.CustomerId == customerId).ToListAsync();
        return works;
    }
 
    public async Task UpdateCustomer(Customer customer)
    {
        var oldCustomer = await GetCustomerById(customer.Id);

        if (oldCustomer is not null)
        {
            oldCustomer.Name = customer.Name;
            oldCustomer.EmailAddress = customer.EmailAddress;
            oldCustomer.Address = customer.Address;
            
        }
        
        _dbContext.Customers.Update(oldCustomer);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Customer updated {@Customer}",oldCustomer);
    }
}