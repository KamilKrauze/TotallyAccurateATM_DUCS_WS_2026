namespace TerribleStateMachineLib;

/// <summary>
/// One-way connection between two <see cref="IState"/>.
/// </summary>
public class Transition
{
    /// <summary>
    /// Parametrized constructor that creates the definition
    /// for transitioning between state.
    /// </summary>
    /// <param name="from">State to exit from</param>
    /// <param name="to">State to enter to</param>
    /// <param name="condition">Optional transition condition that
    /// defines when the state machine is allowed to exit the source state and enter the destination state. (Default FALSE)</param>
    /// <param name="priority">The priority to have on this transition definition (0 - highest). More info on <seealso cref="Priority"/></param>
    public Transition(IState from, IState to, Func<bool>? condition = null, int priority = 0)
    {
        From = from;
        To = to;
        Condition = condition ?? (() => false);
        Priority = priority;
    }
    
    /// <summary>
    /// State to exit from.
    /// </summary>
    public IState From { get; }
    
    /// <summary>
    /// State to enter to.
    /// </summary>
    public IState To { get; }
    
    /// <summary>
    /// Optional transition condition that
    /// defines when the state machine is allowed to exit the source state
    /// and enter the destination state. (Default FALSE)
    /// </summary>
    public Func<bool> Condition { get; }
    
    /// <summary>
    /// The priority to have on this transition definition (0 - highest)
    /// </summary>
    /// <remarks>
    /// If there are multiple <see cref="Transition"/> objects pointing to the same source state
    /// then it's first-come-first served!
    /// </remarks>
    public int Priority { get; }
}