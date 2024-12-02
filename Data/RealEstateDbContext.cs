using Microsoft.EntityFrameworkCore;
using Realestate.Entities;

public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
{
    public DbSet<RealEstateProperty>? Properties { get; set; }
    public DbSet<Agent>? Agents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RealEstateProperty>()
            .HasOne(p => p.Agent)
            .WithMany(a => a.Properties)
            .HasForeignKey(p => p.AgentId);
    }
}
