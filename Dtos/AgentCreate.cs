using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Realestate.Dtos;

public class AgentCreate
{
    public string? Name { get; set; }
    public string? Phone { get; set; }

    public IList<Property> Properties { get; set; } = [];
}