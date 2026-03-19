namespace TerribleStateMachineLib;

/// <summary>
/// The state machine that transitions between <see cref="IState"/> objects
/// defined by <see cref="Transition"/> objects. 
/// </summary>
public class StateMachine
{
    private readonly Dictionary<string, IState> _states = new();
    private readonly List<Transition> _transitions = new();
    private bool ranInitStateEntry = false;
    
    /// <summary>
    /// Current state node.
    /// </summary>
    public IState CurrentState { get; private set; }
    
    /// <summary>
    /// The execution state of the machine instance.
    /// </summary>
    public StateMachineExecutionState ExecutionState { get; private set; } = StateMachineExecutionState.NotStarted;
    
    /// <summary>
    /// Constructs the state machine object with an initial state object instance.
    /// </summary>
    /// <param name="initialState">The initial state that state machine is in.</param>
    public StateMachine(IState initialState)
    {
        if (initialState.CanTerminate)
        {
            throw new InvalidOperationException("Init state cannot interrupt state machine execution.");
        }
        CurrentState = initialState;
        _states[initialState.Name] = initialState;
    }
    
    /// <summary>
    /// Adds a new state for machine to track.
    /// </summary>
    /// <param name="state">The state to add.</param>
    public void AddState(IState state)
    {
        if (_states.Keys.Contains(state.Name))
        {
            throw new InvalidOperationException($"State name \"{state.Name}\" already exists, cannot have duplicate states.");
        }
        _states[state.Name] = state;
    }
    
    /// <summary>
    /// Define a new transition point between two states.
    /// </summary>
    /// <param name="transition">The defined transition point.</param>
    public void DefineTransition(Transition transition)
    {
        _transitions.Add(transition);
    }
    
    /// <summary>
    /// Updates the current execution of the state machine.
    /// </summary>
    public void Update()
    {
        if (!ranInitStateEntry)
        {
            ranInitStateEntry = true;
            CurrentState.OnEntry?.Invoke();
            ExecutionState = StateMachineExecutionState.Starting;
        }
        
        if (CurrentState.CanTerminate)
        {
            ExecutionState = StateMachineExecutionState.Terminating;
            return;
        }
        
        ExecutionState = StateMachineExecutionState.Running;
        
        List<Transition> validTransitions = _transitions
            .Where(t => t.From.Name == CurrentState.Name)
            .Where(t => t.To.Name != CurrentState.Name && _states.ContainsKey(t.To.Name))
            .Where(t => t.Condition.Invoke())
            .OrderByDescending(t => t.Priority)
            .ToList();

        if (!validTransitions.Any())
        {
            ExecutionState = StateMachineExecutionState.UnChanged;
            CurrentState.OnUpdate?.Invoke();
            return;
        }

        Transition transition = validTransitions.First();
        PerformTransition(transition);
    }
    
    
    private void PerformTransition(Transition transition)
    {
        var nextState = _states[transition.To.Name];

        CurrentState.OnExit?.Invoke();

        CurrentState = nextState;
        ExecutionState = StateMachineExecutionState.ChangingState;

        CurrentState.OnEntry?.Invoke();
    }
}