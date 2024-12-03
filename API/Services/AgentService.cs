using Microsoft.EntityFrameworkCore;
using Realestate.Abstractions;
using Realestate.Entities;
//00016096
namespace Realestate.Services;

public class AgentService(RealEstateDbContext dbContext) : IAgentService
{
    public async Task<Agent> CreateAgentAsync(Agent agent)
    {
        dbContext.Agents!.Add(agent);
        await dbContext.SaveChangesAsync();
        return agent;
    }

    public async Task<bool> DeleteAgentAsync(int id)
    {
        var agent = await dbContext.Agents!.FindAsync(id);
        if (agent == null) return false;

        dbContext.Agents!.Remove(agent);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Agent> GetAgentByIdAsync(int id)
        => await dbContext.Agents!.Include(p => p.Properties).FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception(nameof(Agent));

    public async Task<IEnumerable<Agent>> GetAllAgentsAsync()
        => await dbContext.Agents!.Include(a => a.Properties).ToListAsync();

    public async Task<Agent> UpdateAgentAsync(int id, Agent agent)
    {
        var existingAgent = await dbContext.Agents!.FindAsync(id) ?? throw new Exception(nameof(Agent));
        
        existingAgent.Name = agent.Name;
        existingAgent.Phone = agent.Phone;
        existingAgent.Properties = agent.Properties;

        await dbContext.SaveChangesAsync();
        return existingAgent;
    }
}