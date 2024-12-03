using Realestate.Entities;
//00016096
namespace Realestate.Dtos;

public class AgentCreate
{
    public string? Name { get; set; }
    public string? Phone { get; set; }

    public IList<RealEstateProperty> Properties { get; set; } = null!;
}