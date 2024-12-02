using Microsoft.EntityFrameworkCore;
using Realestate.Entities;

public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
{
    public DbSet<RealEstateProperty>? Properties { get; set; }
    public DbSet<Agent>? Agents { get; set; }
}
