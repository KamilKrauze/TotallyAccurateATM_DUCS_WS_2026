namespace TerribleStateMachineLib;

/// <summary>
/// Concrete implementation of a state node.
/// </summary>
public class StateNode : IState
{
    public StateNode(string state_name, bool interruptible = false)
    {
        Name = state_name ?? throw new ArgumentNullException(nameof(state_name));
        CanTerminate = interruptible;
    }
    
    public string Name { get; set; }
    public Action? OnEntry { get; set; } = null;

    public Action? OnUpdate { get; set; } = null;
    public Action? OnExit { get; set; } = null;
    public bool CanTerminate { get; set; }
}