using Microsoft.EntityFrameworkCore;
using Realestate.Entities;

public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
{
    public DbSet<Property>? Properties { get; set; }
    public DbSet<Agent>? Agents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>()
            .HasOne(p => p.Agent)
            .WithMany(a => a.Properties)
            .HasForeignKey(p => p.AgentId);
    }
}
