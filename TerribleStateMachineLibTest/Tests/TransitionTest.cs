using NUnit.Framework;
using TerribleStateMachineLib;
using TerribleStateMachineLibTest.Utils;

namespace TerribleStateMachineLibTest.Tests;

/// <summary>
/// Test suite for <see cref="Transition"/>.
/// </summary>
[TestFixture]
public class TransitionTest
{
    [Test]
    public void Test_TransitionConstruction()
    {  
        Transition transition = new Transition(
            TestObjectProviderUtil.MakeValidTestState("State1"), 
            TestObjectProviderUtil.MakeValidTestState("State2"),
            (() => true), 
            priority: 0);
        
        IState fromState = transition.From;
        IState toState = transition.To;
        
        AssertForStateInTransitionDefinition(fromState, "State1");
        AssertForStateInTransitionDefinition(toState, "State2");
    }

    private static void AssertForStateInTransitionDefinition(IState state, string expectedStateName)
    {
        Assert.That(state, Is.Not.Null);
        Assert.That(state.Name, Is.EqualTo(expectedStateName));
        Assert.That(state.CanTerminate, Is.False, 
            "This property should be false by default.");

        Assert.That(state.OnEntry, Is.Null);
        Assert.That(state.OnExit, Is.Null);
    }
}