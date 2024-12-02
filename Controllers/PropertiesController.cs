using Microsoft.AspNetCore.Mvc;
using Realestate.Dtos;
using Realestate.Entities;
using Realestate.Services;

namespace Realestate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController(PropertyService propertyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await propertyService.GetAllPropertiesAsync();

        return Ok(properties.Select(p => new PropertyRead
        {
            Id = p.Id,
            Address = p.Address,
            Agent = p.Agent,
            Price = p.Price
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddProperty(PropertyCreate dto)
    {
        var newProperty = new RealEstateProperty {
            Address = dto.Address,
            Agent = dto.Agent,
            Price = dto.Price
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
            Agent = property.Agent,
            Price = property.Price
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AgentUpdate>> UpdateProperty(int id, PropertyUpdate dto)
    {
        var property = new RealEstateProperty
        {
            Address = dto.Address,
            Agent = dto.Agent,
            Price = dto.Price
        };

        var updatedProperty = await propertyService.UpdatePropertyAsync(id, property);
        if (updatedProperty == null) return NotFound();

        return Ok(new PropertyRead
        {
            Id = property.Id,
            Address = property.Address,
            Agent = property.Agent,
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
