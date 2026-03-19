using NUnit.Framework;
using TerribleStateMachineLib;
using TerribleStateMachineLibTest.Stubs;
using TerribleStateMachineLibTest.Utils;

namespace TerribleStateMachineLibTest.Tests;

/// <summary>
/// Test suite for <see cref="StateMachine"/>.
/// </summary>
[TestFixture]
public class StateMachineTest
{
    [Test]
    public void Test_StateMachineExecution()
    {
        StubStateNode initState = TestObjectProviderUtil.MakeValidStubbedTestState(
            "Init", false, true);
        
        var nextState1 = TestObjectProviderUtil.MakeValidStubbedTestState(
            "State1", false, true);
        
        var nextState2 = TestObjectProviderUtil.MakeValidStubbedTestState(
            "State2", true, true);
        
        Transition edge1 = new Transition(initState, nextState1, () => true);
        Transition edge2 = new Transition(nextState1, nextState2, () => true);
        
        var stateMachine = new StateMachine(initState);
        stateMachine.AddState(nextState1);
        stateMachine.AddState(nextState2);
        
        stateMachine.DefineTransition(edge1);
        stateMachine.DefineTransition(edge2);

        Assert.That(stateMachine.ExecutionState, Is.EqualTo(StateMachineExecutionState.NotStarted));
        stateMachine.Update();
        while (stateMachine.ExecutionState != StateMachineExecutionState.Terminating)
        {
            Assert.That(stateMachine.ExecutionState,  
                Is.AnyOf(
                    StateMachineExecutionState.Running, 
                    StateMachineExecutionState.ChangingState,
                    StateMachineExecutionState.UnChanged));
            
            stateMachine.Update();
        }
    }
}