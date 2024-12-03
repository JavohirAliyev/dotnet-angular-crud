using Microsoft.EntityFrameworkCore;
using Realestate.Entities;
//00016096
public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
{
    public DbSet<RealEstateProperty>? Properties { get; set; }
    public DbSet<Agent>? Agents { get; set; }
}
