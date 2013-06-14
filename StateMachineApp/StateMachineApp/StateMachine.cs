using System;
using System.Collections.Generic;

namespace StateMachineApp
{
	public class StateMachine
	{
		public List<State> States { get; private set; }
		public State CurrentState { get; private set; }

		public StateMachine()
		{
			States = new List<State>();
		}

		public void SetState(Enum state)
		{
			for (int i = 0; i < States.Count; i++)
			{
				if (States[i].Type.Equals(state))
				{
					CurrentState = States[i];
					States[i].Status = Status.Initializing;
					break;
				}
			}
		}
	}
}