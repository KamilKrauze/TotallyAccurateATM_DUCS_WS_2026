namespace TerribleStateMachineLib;

/// <summary>
/// The execution states that the state machine can report when running.
/// </summary>
public enum StateMachineExecutionState
{
    NotStarted,
    Starting,
    Terminating,
    UnChanged,
    Running,
    ChangingState,
}