using System;
using System.Collections.Generic;

namespace StateMachineApp
{
	public class State
	{
		public Enum Type { get; private set; }

		public Status Status
		{
			get { return status; }
			set
			{
				status = value;

				if (actions.ContainsKey(status))
				{
					if (actions[status] != null)
					{
						actions[status].Invoke(this);
					}
				}
			}
		}

		private Status status;
		private readonly Dictionary<Status, Action<State>> actions;

		public State(Enum type)
		{
			Type = type;
			actions = new Dictionary<Status, Action<State>>();
		}

		public void Register(Status status, Action<State> action)
		{
			if (actions.ContainsKey(status))
			{
				actions.Remove(status);
			}

			actions.Add(status, action);
		}

		public bool IsType(Enum type)
		{
			return Type.Equals(type);
		}

		public void Update()
		{
			actions[status].Invoke(this);
		}
	}
}