using Microsoft.EntityFrameworkCore;
using Realestate.Abstractions;
using Realestate.Entities;
//00016096
namespace Realestate.Services;

public class PropertyService(RealEstateDbContext dbContext) : IPropertyService
{
    public async Task<RealEstateProperty> CreatePropertyAsync(RealEstateProperty property)
    {
        dbContext.Properties!.Add(property);
        await dbContext.SaveChangesAsync();
        return property;
    }

    public async Task<bool> DeletePropertyAsync(int id)
    {
        var property = await dbContext.Properties!.FindAsync(id);
        if (property == null) return false;

        dbContext.Properties.Remove(property);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<RealEstateProperty>> GetAllPropertiesAsync()
        => await dbContext.Properties!.ToListAsync();

    public async Task<RealEstateProperty> GetPropertyByIdAsync(int id)
        => await dbContext.Properties!.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception(nameof(RealEstateProperty));

    public async Task<RealEstateProperty> UpdatePropertyAsync(int id, RealEstateProperty property)
    {
        var existingProperty = await dbContext.Properties!.FindAsync(id) ?? throw new Exception(nameof(RealEstateProperty));
        
        existingProperty.Address = property.Address;
        existingProperty.Price = property.Price;
        //existingProperty.Agent = property.Agent;

        await dbContext.SaveChangesAsync();
        return existingProperty;
    }
}