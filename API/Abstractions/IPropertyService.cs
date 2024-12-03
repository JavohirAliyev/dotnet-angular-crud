using Realestate.Entities;
//00016096
namespace Realestate.Abstractions;

public interface IPropertyService
{
    Task<IEnumerable<RealEstateProperty>> GetAllPropertiesAsync();
    Task<RealEstateProperty> GetPropertyByIdAsync(int id);
    Task<RealEstateProperty> CreatePropertyAsync(RealEstateProperty property);
    Task<RealEstateProperty> UpdatePropertyAsync(int id, RealEstateProperty property);
    Task<bool> DeletePropertyAsync(int id);
}