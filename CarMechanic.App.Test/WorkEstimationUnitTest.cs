using CarMechanic.Shared;
using Xunit;

namespace CarMechanic.App.Test;

public class WorkEstimationUnitTest
{   
    [Fact]
    public void IsUnitTestValid()
    {
        var work = new Work
        {
            Id = 1,
            CustomerId = 3,
            LicensePlate = "XYZ-123",
            ManufacturingYear = 1980,
            Category = WorkCategory.BodyWork,
            WorkDescription = "I have wun",
            Fault = 3,
            Status = WorkStatus.Completed
        };
        
        Assert.Equal(2.4,WorkEstimate.CalculateWorkEstimation(work));
    }
}