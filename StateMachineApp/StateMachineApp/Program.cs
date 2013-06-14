using System;
using System.Diagnostics;

namespace StateMachineApp
{
	public class Program
	{
		public enum UserState
		{
			Idle,
			Working
		}

		static void Main(string[] args)
		{
			var machine = new StateMachine();

			// 1. This is how you wire up your states and delegates for all statuses within that state

			// Idle
			var state = new State(UserState.Idle);
			state.Register(Status.Initializing, (s) => Console.WriteLine("Idle.Initializing"));
			state.Register(Status.Running, (s) => Console.WriteLine("Idle.Running"));
			state.Register(Status.Completed, (s) => Console.WriteLine("Idle.Completed"));
			state.Register(Status.Failed, (s) => Console.WriteLine("Idle.Failed"));
			machine.States.Add(state);

			// Working
			state = new State(UserState.Working);
			state.Register(Status.Initializing, (s) => { Console.WriteLine("Working.Initializing"); s.Status = Status.Completed; });
			state.Register(Status.Completed, (s) => Console.WriteLine("Working.Completed"));
			machine.States.Add(state);

			// 2. After setting up your states you can set the default/current one like this
			machine.SetState(UserState.Working);

			// 3. This is how you would check statuses from outside the state by using the state machine itself
			if (machine.CurrentState.Status == Status.Failed)
			{
				Console.WriteLine("An error occured!");
			}

			// 4. Finally, this is how you would check what type your current state is
			if (machine.CurrentState.IsType(UserState.Working))
			{
				Console.WriteLine("We are working!");
			}

			Console.ReadKey();
		}
	}
}