using MultiAgentSystem.Cores.Helpers.Grids;

namespace MultiAgentSystem.Cores.Models
{
    internal interface IAgent
    {
        Grid Grid { get; set; }

        void Decide();
    }
}