using System.Diagnostics;
using TerribleStateMachineLib;
using TerribleStateMachineLibTest.Stubs;

namespace TerribleStateMachineLibTest.Utils;

internal static class TestObjectProviderUtil
{
    internal static IState MakeValidTestState(string name, bool interruptible = false, bool setupLogging = false)
    {
        IState stateNode = new StateNode(name, interruptible);
        if (setupLogging)
        {
            stateNode = LogStateEntry(stateNode);
            stateNode = LogStateExit(stateNode);
        }
        return stateNode;
    }
        
    internal static StubStateNode MakeValidStubbedTestState(string name, bool interruptible,  bool setupLogging = false)
    {
        StubStateNode stateNode = new StubStateNode(name, interruptible);
        if (setupLogging)
        {
            stateNode = LogStateEntry(stateNode);
            stateNode = LogStateExit(stateNode);
        }
        return stateNode;
    }

    internal static StateT LogStateEntry<StateT>(StateT state) where StateT : IState
    {
        state.OnEntry = () => { Debug.WriteLine($"[{state.Name}] - Entered"); };
        return state;
    }
    
    internal static StateT LogStateUpdate<StateT>(StateT state) where StateT : IState
    {
        state.OnUpdate = () => { Debug.WriteLine($"[{state.Name}] - Update"); };
        return state;
    }
    
    internal static StateT LogStateExit<StateT>(StateT state) where StateT : IState
    {
        state.OnExit = () => { Debug.WriteLine($"[{state.Name}] - Exited"); };
        return state;
    }
}