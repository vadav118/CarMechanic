using CarMechanic.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Serilog;
using Xunit.Abstractions;


namespace CarMechanic.App.Test;

public class WorkServiceUnitTests
{
     private CarMechanicContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<CarMechanicContext>()
            .UseInMemoryDatabase(databaseName:dbName)
            .Options;
        return new CarMechanicContext(options);
    }

    private WorkService CreateWorkService(CarMechanicContext context)
    {
        var logger = Substitute.For<ILogger<WorkService>>();
        return new WorkService(logger, context);
    }

    [Fact]
    public async Task GivenValidWork()
    {
        var context = CreateContext("AddWorkDb");
        var service = CreateWorkService(context);

        var work = new Work
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
        
        await service.AddWork(work);
        var saved = await context.Works.FindAsync(1);
        Assert.NotNull(saved);
        Assert.Equal(1, saved.Id);
    }
    
    [Fact]
    public async Task GivenExistingWork()
    {
        var context = CreateContext("GetWorkDb");
        

        var work = new Work
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
        context.Works.Add(work);
        await context.SaveChangesAsync();
        var service = CreateWorkService(context);

        var result = await service.GetWorkById(1);
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    
    [Fact]
    public async Task GetNullWork()
    {
        var context = CreateContext("GetNullWorkDb");
        var service = CreateWorkService(context);
        var result = await service.GetWorkById(404);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task DeleteWorkAndRemoved()
    {
        var context = CreateContext("DeleteWorkDb");
        var work = new Work
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
        context.Works.Add(work);
        await context.SaveChangesAsync();
        var service = CreateWorkService(context);
        
        await service.DeleteWork(1);
        var result = await context.Works.FindAsync(1);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateWorkModified()
    {
        var context = CreateContext("UpdateWorkDb");
        var work = new Work
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
        context.Works.Add(work);
        await context.SaveChangesAsync();
        var service = CreateWorkService(context);

        var update = new Work
        {
            Id = 1,
            CustomerId = 3,
            LicensePlate = "XYZ-123",
            ManufacturingYear = 2000,
            Category = WorkCategory.BodyWork,
            WorkDescription = "I have wun",
            Fault = 10,
            Status = WorkStatus.Completed
        };
        
        await service.UpdateWork(update);
        var result = await context.Works.FindAsync(1);
        
        Assert.Equal(3, result.CustomerId);
        Assert.Equal("I have wun", result.WorkDescription);
        Assert.Equal(WorkStatus.Completed, result.Status);
    }
    
    [Fact]
    public async Task ReturnAllWork()
    {
        var context = CreateContext("ReturnAllWorkDb");
        var work1 = new Work
        {
            Id = 1,
            CustomerId = 3,
            LicensePlate = "XYZ-123",
            ManufacturingYear = 2000,
            Category = WorkCategory.BodyWork,
            WorkDescription = "I have wun",
            Fault = 10,
            Status = WorkStatus.Completed
        };
        
        context.Works.Add(work1);
        
        var work2 = new Work
        {
            Id = 2,
            CustomerId = 4,
            LicensePlate = "YYY-222",
            ManufacturingYear = 2001,
            Category = WorkCategory.Engine,
            WorkDescription = "Oil up",
            Fault = 10,
            Status = WorkStatus.ListedWork
        };
        
        context.Works.Add(work2);
        
        await context.SaveChangesAsync();
        
        var service = CreateWorkService(context);

        var list = await service.GetAllWorks();
        
        Assert.Equal(2, list.Count);
    }
    
}