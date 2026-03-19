using TerribleStateMachineLib;

namespace TerribleStateMachineLibTest.Stubs;

internal class StubStateNode(string stateName, bool interruptible) : IState
{
    public string Name { get; set; } = stateName;
    public Action? OnEntry { get; set; }
    
    public Action? OnUpdate { get; set; }
    public Action? OnExit { get; set; }
    public bool CanTerminate { get; set; } = interruptible;
}