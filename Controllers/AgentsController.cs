using Microsoft.AspNetCore.Mvc;
using Realestate.Abstractions;
using Realestate.Dtos;
using Realestate.Entities;
using Realestate.Services;

namespace Realestate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentsController(AgentService agentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAgents()
    {
        var agents = await agentService.GetAllAgentsAsync();

        return Ok(agents.Select(a => new AgentRead
        {
            Id = a.Id,
            Name = a.Name,
            Phone = a.Phone,
            Properties = a.Properties
        }));
    }

    [HttpPost]
    public async Task<IActionResult> AddAgent(AgentCreate dto)
    {
        var newAgent = new Agent {
            Name = dto.Name,
            Phone = dto.Phone,
            Properties = dto.Properties
        };
        var createdAgent = await agentService.CreateAgentAsync(newAgent);
        
        return CreatedAtAction(nameof(GetAllAgents), new { id = newAgent.Id }, newAgent);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgentRead>> GetAgentById(int id)
    {
        var agent = await agentService.GetAgentByIdAsync(id);
        if (agent == null) return NotFound();

        return Ok(new AgentRead
        {
            Id = agent.Id,
            Name = agent.Name,
            Phone = agent.Phone,
            Properties = agent.Properties
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AgentUpdate>> UpdateAgent(int id, AgentUpdate dto)
    {
        var agent = new Agent
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Properties = dto.Properties
        };

        var updatedAgent = await agentService.UpdateAgentAsync(id, agent);
        if (updatedAgent == null) return NotFound();

        return Ok(new AgentRead
        {
            Id = agent.Id,
            Name = agent.Name,
            Phone = agent.Phone,
            Properties = agent.Properties
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAgent(int id)
    {
        var deleted = await agentService.DeleteAgentAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }

}