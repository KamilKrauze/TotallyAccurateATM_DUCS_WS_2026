using TerribleStateMachineLib;
using NUnit.Framework;
using TerribleStateMachineLibTest.Stubs;

namespace TerribleStateMachineLibTest.Tests
{
    /// <summary>
    /// Test suite for <see cref="TerribleStateMachineLib.IState"/>
    /// </summary>
    [TestFixture]
    internal class StateTest
    {
        private string _stateEventString = string.Empty;
        private const string EXPECTED_ENTRY = "Entered";
        private const string EXPECTED_EXIT = "Exited";
        
        /// <summary>
        /// Runs at the start of every Test/TestCase
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _stateEventString = string.Empty;
        }
        
        /// <summary>
        /// Runs at the end of every Test/TestCase
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _stateEventString = string.Empty;
        }
        
        /// <summary>
        /// Tests the instantiation of a <see cref="StateNode"/> object which is stored as an <see cref="IState"/>.
        /// </summary>
        [Test]
        public void Test_StateNodeConstruction()
        {
            IState stateNode1 = MakeValidTestState("State1");
            Assert.That(stateNode1.Name, Is.EqualTo("State1"));
            Assert.That(stateNode1.CanTerminate, Is.False, 
                "This property should be false by default.");

            Assert.That(_stateEventString, Is.EqualTo(string.Empty));
            Assert.That(stateNode1.OnEntry, Is.Not.Null);
            Assert.That(stateNode1.OnExit, Is.Not.Null);
            
            stateNode1.OnEntry.Invoke();
            Assert.That(_stateEventString, Is.EqualTo(EXPECTED_ENTRY));
            stateNode1.OnExit.Invoke();
            Assert.That(_stateEventString, Is.EqualTo(EXPECTED_EXIT));
        }

        /// <summary>
        /// Tests construction of a <see cref="IState"/> based type
        /// </summary>
        /// <param name="interruptible">Test value to assign to property.</param>
        /// <param name="expectedItr">Expected value.</param>
        [TestCase(false, false)]
        [TestCase(true, true)]
        public void Test_StubbedStateNodeConstruction(
            bool interruptible, bool expectedItr)
        {
            IState stateNode1 = MakeValidStubbedTestState("State1", interruptible);
            Assert.That(stateNode1.Name, Is.EqualTo("State1"));
            Assert.That(stateNode1.CanTerminate, Is.EqualTo(expectedItr));

            Assert.That(_stateEventString, Is.EqualTo(string.Empty));
            Assert.That(stateNode1.OnEntry, Is.Not.Null);
            Assert.That(stateNode1.OnExit, Is.Not.Null);
            
            stateNode1.OnEntry.Invoke();
            Assert.That(_stateEventString, Is.EqualTo(EXPECTED_ENTRY));
            stateNode1.OnExit.Invoke();
            Assert.That(_stateEventString, Is.EqualTo(EXPECTED_EXIT));
        }

        private IState MakeValidTestState(string name, bool interruptible = false)
        {
            IState stateNode = new StateNode(name, interruptible);
            stateNode.OnEntry = OnEntryTestStub;
            stateNode.OnExit = OnExitTestStub;
            return stateNode;
        }
        
        private IState MakeValidStubbedTestState(string name, bool interruptible)
        {
            IState stateNode = new StubStateNode(name, interruptible);
            stateNode.OnEntry = OnEntryTestStub;
            stateNode.OnExit = OnExitTestStub;
            
            return stateNode;
        }

        private void OnEntryTestStub()
        {
            _stateEventString = "Entered";
        }
        
        private void OnExitTestStub()
        {
            _stateEventString = "Exited";
        }
    }
}