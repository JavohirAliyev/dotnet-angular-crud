using Microsoft.AspNetCore.Mvc;
using Realestate.Abstractions;
using Realestate.Dtos;
using Realestate.Entities;
//00016096
namespace Realestate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController(IPropertyService propertyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await propertyService.GetAllPropertiesAsync();

        return Ok(properties.Select(p => new PropertyRead
        {
            Id = p.Id,
            Address = p.Address,
            AgentId = p.AgentId,
            Price = p.Price
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddProperty(PropertyCreate dto)
    {
        var newProperty = new RealEstateProperty {
            Address = dto.Address,
            Price = dto.Price,
            AgentId = dto.AgentId
        };
        var createdProperty = await propertyService.CreatePropertyAsync(newProperty);
        
        return CreatedAtAction(nameof(GetAllProperties), new { id = newProperty.Id }, newProperty);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgentRead>> GetPropertyById(int id)
    {
        var property = await propertyService.GetPropertyByIdAsync(id);
        if (property == null) return NotFound();

        return Ok(new PropertyRead
        {
            Id = property.Id,
            Address = property.Address,
            AgentId = property.AgentId,
            Price = property.Price
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AgentUpdate>> UpdateProperty(int id, PropertyUpdate dto)
    {
        var property = new RealEstateProperty
        {
            Id = id,
            Address = dto.Address,
            Price = dto.Price,
            AgentId = dto.AgentId
        };

        var updatedProperty = await propertyService.UpdatePropertyAsync(id, property);
        if (updatedProperty == null) return NotFound();

        return Ok(new PropertyRead
        {
            Id = property.Id,
            Address = property.Address,
            AgentId = property.AgentId,
            Price = property.Price
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProperty(int id)
    {
        var deleted = await propertyService.DeletePropertyAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }

}
