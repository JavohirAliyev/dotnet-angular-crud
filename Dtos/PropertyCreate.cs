using Realestate.Entities;

namespace Realestate.Dtos;

public class PropertyCreate
{
    public string? Address { get; set; }
    public decimal Price { get; set; }
    public Agent Agent { get; set; } = null!;
}