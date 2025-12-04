using CarMechanic.Shared;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic;

public class CarMechanicContext: DbContext
{
    public CarMechanicContext(DbContextOptions<CarMechanicContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Work> Works { get; set; }
}