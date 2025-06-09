using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

// Author : PACCAPELO Auguste

namespace Com.IsartDigital.Hackaton
{
	public abstract class Manager : Node2D
	{
		// ---------- VARIABLES ---------- \\

		// ----- Managers ----- \\
		[Export] protected int initPriority = 0;
		public static int numManager { get; private set; } = 0;
		private static Dictionary<Type, Manager> allManagers = new Dictionary<Type, Manager>();
		public static Action allManagersFinishedInits;
		public static bool isInitsFinished { get; private set; } = false;
		public Type thisType { get; private set; } = null;

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Start & Init ----- \\

		protected Manager() : base()
		{
			// Count how many Managers instances have been created
			numManager++;
		}

		public override void _Ready()
		{
			thisType = GetType();
			
			// Prevent duplicate manager of the same type
			if (allManagers.ContainsKey(thisType))
			{
				GD.Print("This manager : " + thisType + " already exist, destroying last added.");
				numManager--;
				Destructor();
				return;
			}

			allManagers.Add(thisType, this);
			if (IsAllManagersReady()) InitAllManagers();
		}

		protected abstract void Init();

		// ----- My Functions ----- \\

		/// <summary>
		/// Retrieves the instance of the specified manager type, if it exist.
		/// </summary>
		/// <typeparam name="T">The manager type to retrieve</typeparam>
		/// <returns>The manager instance, or null if not found</returns>
		public static T GetManager<T>() where T : Manager
		{
			Type lManagerType = typeof(T);
			if (allManagers.ContainsKey(lManagerType))
				return (T)allManagers[lManagerType];

			GD.Print("Manager of type : " + lManagerType + " not found.");
			return null;
		}

		private bool IsAllManagersReady() => numManager == allManagers.Count;
		private void InitAllManagers()
		{
			if (!isInitsFinished)
			{
				List<Manager> allManagersList = allManagers.Values.ToList();
				allManagersList.Sort((a, b) => b.initPriority.CompareTo(a.initPriority));
				foreach (Manager lManager in allManagersList) lManager.Init();
				isInitsFinished = true;
				allManagersFinishedInits?.Invoke();
				return;
			}
			Init();
		}

		// ----- Destructor ----- \\

		public virtual void Destructor()
		{
			if (allManagers[thisType] == this)
			{
				numManager--;
				allManagers.Remove(thisType);
			}
			QueueFree();
		}
	}
}
