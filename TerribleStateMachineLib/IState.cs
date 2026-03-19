namespace TerribleStateMachineLib;

/// <summary>
/// Common definition for basic states to be used by state machine.
/// </summary>
public interface IState
{
    /// <summary>
    /// The name of the state
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Logic to trigger when entering the state.
    /// </summary>
    /// <remarks>
    /// Optional
    /// </remarks>
    public Action? OnEntry { get; set; }
    
    /// <summary>
    /// Logic to trigger when <see cref="StateMachine.Update"/> is called.
    /// </summary>
    public Action? OnUpdate { get; set; }
    
    /// <summary>
    /// Logic to trigger when exiting the state.
    /// </summary>
    /// <remarks>
    /// Optional
    /// </remarks>
    public Action? OnExit { get; set; }

    /// <summary>
    /// Whether this state can interrupt the entire state machine.
    /// </summary>
    public bool CanTerminate { get; set; }
}