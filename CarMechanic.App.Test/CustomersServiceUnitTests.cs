using CarMechanic.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Serilog;
using Xunit.Abstractions;


namespace CarMechanic.App.Test;

public class CustomersServiceUnitTests
{
    private CarMechanicContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<CarMechanicContext>()
            .UseInMemoryDatabase(databaseName:dbName)
            .Options;
        return new CarMechanicContext(options);
    }

    private CustomerService CreateService(CarMechanicContext context)
    {
        var logger = Substitute.For<ILogger<CustomerService>>();

        return new CustomerService(logger, context);
    }
    
    [Fact]
    public async Task GivenValidCustomer()
    {
        var context = CreateContext("AddCustomerDb");
        var service = CreateService(context);

        var customer = new Customer
        {
            Id = 1,
            Name = "De Daniiiiiii",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        
        await service.AddCustomer(customer);
        var saved = await context.Customers.FindAsync(1);
        Assert.NotNull(saved);
        Assert.Equal(1, saved.Id);
    }
    
    [Fact]
    public async Task GivenExistingCustomer()
    {
        var context = CreateContext("GetCustomersDb");
        

        var customer = new Customer
        {
            Id = 1,
            Name = "De Daniiiiiii",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var result = await service.GetCustomerById(1);
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    
    [Fact]
    public async Task GetNullWork()
    {
        var context = CreateContext("GetNullCustomerDb");
        var service = CreateService(context);
        var result = await service.GetCustomerById(404);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task DeleteCustomersAndRemoved()
    {
        var context = CreateContext("DeleteCustomerDb");
        var customer = new Customer
        {
            Id = 1,
            Name = "De Daniiiiiii",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        var service = CreateService(context);
        
        await service.DeleteCustomer(1);
        var result = await context.Works.FindAsync(1);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateCustomersModified()
    {
        var context = CreateContext("UpdateCustomerDb");
        var customer = new Customer
        {
            Id = 1,
            Name = "De Daniiiiiii",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var update = new Customer
        {
            Id = 1,
            Name = "Dani",
            EmailAddress = "Dani@de.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        
        await service.UpdateCustomer(update);
        var result = await context.Customers.FindAsync(1);
        
        Assert.Equal("Dani", result.Name);
        Assert.Equal("Dani@de.com", result.EmailAddress);

    }
    
    [Fact]
    public async Task ReturnAllCustomers()
    {
        var context = CreateContext("ReturnAllCustomersDb");
        var customer1 = new Customer
        {
            Id = 11,
            Name = "De Daniiiiiii",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        
        context.Customers.Add(customer1);
        
        var customer2 = new Customer
        {
            Id = 125,
            Name = "Dani",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        
        context.Customers.Add(customer2);
        
        await context.SaveChangesAsync();
        
        var service = CreateService(context);

        var list = await service.GetCustomers();
        
        Assert.Equal(2, list.Count);
    }
    
    [Fact]
    public async Task ReturnAllWorkToCustomers()
    {
        var context = CreateContext("ReturnAllCustomersDb");
        var customer1 = new Customer
        {
            Id = 1,
            Name = "De Daniiiiiii",
            EmailAddress = "Daniiiii@deeee.com",
            Address = "Daniiiiiiiii falva 6969",
        };
        
        context.Customers.Add(customer1);
        
        var work1 = new Work
        {
            Id = 1,
            CustomerId = 1,
            LicensePlate = "XYZ-123",
            ManufacturingYear = 2000,
            Category = WorkCategory.BodyWork,
            WorkDescription = "Pls let me leave",
            Fault = 10,
            Status = WorkStatus.Working
        };
        
        context.Works.Add(work1);
        
        var work2 = new Work
        {
            Id = 2,
            CustomerId = 1,
            LicensePlate = "AAA-696",
            ManufacturingYear = 2010,
            Category = WorkCategory.BodyWork,
            WorkDescription = "Hell Yeah",
            Fault = 10,
            Status = WorkStatus.Working
        };
        context.Works.Add(work2);
        
        await context.SaveChangesAsync();
        
        var service = CreateService(context);

        var list = await service.GetWorks(1);
        
        Assert.Equal(2, list.Count);
    }
}
