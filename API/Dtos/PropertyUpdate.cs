using Realestate.Entities;
//00016096
namespace Realestate.Dtos;

public class PropertyUpdate
{
    public string? Address { get; set; }
    public decimal Price { get; set; }
    public Agent Agent { get; set; } = null!;
}