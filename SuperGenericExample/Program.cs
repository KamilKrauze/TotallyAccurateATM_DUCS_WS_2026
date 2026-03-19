using System;
using TerribleStateMachineLib;

bool bIsCardInserted = false;
bool bStartWithdrawal = false;
bool bIsAmountSelected = false;
bool bAreNotesTaken = false;
bool bIsCardRemoved = true;


StateNode init = new StateNode("Init");
init.OnEntry = () => { Console.WriteLine("Showing idle ATM screen..."); };

StateNode state1 = new StateNode("Card Inserted");
state1.OnEntry = () => { Console.WriteLine("Detecting card..."); };
state1.OnUpdate = () => { Console.WriteLine("Reading card..."); };
state1.OnExit = () => { Console.WriteLine("Card read..."); };

StateNode state2 = new StateNode("Start Withdrawal");
state2.OnEntry = () => { Console.WriteLine("User selected cash withdrawal..."); };
state2.OnUpdate = () => { Console.WriteLine("Waiting for amount selection..."); };

StateNode state3 = new StateNode("Amount Selected");
state3.OnUpdate = () => { Console.WriteLine("User selected amount..."); };

StateNode state4 = new StateNode("Present Notes");
state4.OnEntry = () => { Console.WriteLine("Device is gathering notes..."); };
state4.OnUpdate = () => { Console.WriteLine("Presenting notes to user..."); };

StateNode state5 = new StateNode("End Withdrawal");
state5.OnUpdate = () => { Console.WriteLine("Finishing withdrawal..."); };

StateNode state6 = new StateNode("Return Card");
state6.OnUpdate = () => { Console.WriteLine("Returning card to user..."); };
state6.OnExit = () => { Console.WriteLine("Returning back to idle ATM screen..."); };

Transition connection1 = new Transition(init, state1, () => bIsCardInserted);
Transition connection2 = new Transition(state1, state2, () => bStartWithdrawal);
Transition connection3 = new Transition(state2, state3, () => true);
Transition connection4 = new Transition(state3, state4, () => bIsAmountSelected);
Transition connection5 = new Transition(state4, state5, () => bAreNotesTaken);
Transition connection6 = new Transition(state5, state6, () => true);
Transition connection7 = new Transition(state6, init, () => bIsCardRemoved);

StateMachine totallyAccurateATM = new StateMachine(init);
totallyAccurateATM.AddState(state1);
totallyAccurateATM.AddState(state2);
totallyAccurateATM.AddState(state3);
totallyAccurateATM.AddState(state4);
totallyAccurateATM.AddState(state5);
totallyAccurateATM.AddState(state6);

totallyAccurateATM.DefineTransition(connection1);
totallyAccurateATM.DefineTransition(connection2);
totallyAccurateATM.DefineTransition(connection3);
totallyAccurateATM.DefineTransition(connection4);
totallyAccurateATM.DefineTransition(connection5);
totallyAccurateATM.DefineTransition(connection6);
totallyAccurateATM.DefineTransition(connection7);

// Just run the state machine indefinitely.
Thread stateMachineWorkerThread = new Thread(() =>
{
    while (totallyAccurateATM.ExecutionState != StateMachineExecutionState.Terminating)
    {
        totallyAccurateATM.Update();
        Thread.Sleep(1000);
    }
});
stateMachineWorkerThread.Start();

while (true)
{
    Console.WriteLine("Please insert card... (just enter to continue).");
    Console.ReadLine();
    bIsCardInserted = true;

    Console.WriteLine("Select a transaction... (YOU DON'T HAVE A CHOICE >:)).");
    Console.ReadLine();
    bStartWithdrawal = true;
    
    Console.WriteLine("Select an amount to withdraw.");
    Console.ReadLine();
    bIsAmountSelected = true;
    
    Console.WriteLine("Please take your notes");
    Console.ReadLine();
    bAreNotesTaken = true;
    
    Console.WriteLine("Please take your bank card");
    bIsCardInserted = false;
    bStartWithdrawal = false;
    bIsAmountSelected = false;
    Console.ReadLine();
    bAreNotesTaken = false;
    bIsCardRemoved = true;

    
    Thread.Sleep(500);
}
