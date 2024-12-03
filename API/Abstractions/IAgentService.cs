using Realestate.Entities;
//00016096
namespace Realestate.Abstractions;

public interface IAgentService
{
    Task<IEnumerable<Agent>> GetAllAgentsAsync();
    Task<Agent> GetAgentByIdAsync(int id);
    Task<Agent> CreateAgentAsync(Agent agent);
    Task<Agent> UpdateAgentAsync(int id, Agent agent);
    Task<bool> DeleteAgentAsync(int id);
}