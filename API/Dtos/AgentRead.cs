using Realestate.Entities;

namespace Realestate.Dtos;

public class AgentRead
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }

    public IList<RealEstateProperty> Properties { get; set; } = null!;
}