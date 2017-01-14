namespace MultiAgentSystem.Cores.Constants
{
    public enum SchedulingStrategyEnum
    {
        /// <summary>
        /// Each agent must done an action before that any agent start a second action. And the agent sequence must change at every tick.
        /// </summary>
        Fair,
        /// <summary>
        /// Each agent must done an action before that any agent start a second action. But the sequence have not change.
        /// </summary>
        Sequential,

        /// <summary>
        /// ???
        /// </summary>
        Random
    }
}