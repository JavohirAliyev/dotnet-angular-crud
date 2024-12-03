namespace Realestate.Entities;
//00016096
public class Agent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }

    public IList<RealEstateProperty> Properties { get; set; } = null!;
}