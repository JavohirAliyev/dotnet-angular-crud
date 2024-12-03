namespace Realestate.Dtos;
//00016096
public class PropertyRead
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public decimal Price { get; set; }
    public int AgentId { get; set; }
}