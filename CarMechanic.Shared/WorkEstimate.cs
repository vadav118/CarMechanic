
namespace CarMechanic.Shared;

public static class WorkEstimate
{
    private static readonly Dictionary<WorkCategory, double> BaseHours = new()
    {
        { WorkCategory.BodyWork, 3.0 },
        { WorkCategory.Engine, 8.0 },
        { WorkCategory.Suspension, 6.0 },
        { WorkCategory.Breaks, 4.0 }
    };

    public static double CalculateWorkEstimation(Work work)
    {
        var hours = BaseHours[work.Category];

        var age = DateTime.Today.Year - work.ManufacturingYear;

        var ageWeight = age switch
        {
            <= 5 => 0.5,
            <= 10 => 1.0,
            <= 20 =>1.5,
            _ => 2.0
        };

        var faultWeight = work.Fault switch
        {
            <= 2 => 0.2,
            <= 4 => 0.4,
            <= 7 => 0.6,
            <= 9 => 0.8,
            _ => 1.0
        };
        
        return Math.Round(hours * ageWeight * faultWeight,1,MidpointRounding.AwayFromZero);
    }
}