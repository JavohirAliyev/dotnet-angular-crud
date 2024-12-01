using Realestate.Entities;

namespace Realestate.Dtos;

public class AgentRead
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }

    public IList<Property> Properties { get; set; } = null!;
}