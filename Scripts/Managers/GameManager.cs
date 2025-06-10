using Godot;
using System;
using System.Collections.Generic;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	public partial class GameManager : Manager
	{
		// ---------- VARIABLES ---------- \\

		// ----- Paths ----- \\

		private PackedScene choiceScreenScene = GD.Load<PackedScene>("res://Scenes/ChoiceScreen.tscn");

		// ----- Nodes ----- \\

		[Export] private Control menuContainer;

		// ----- Others ----- \\

		// Gameplay
		private int minComfortLevel = -5, maxComfortLevel = 5;
		private int minContactLebel = -5, maxContactLevel = 5;
		private int minMoneyLevel = 0, maxMoneyLevel = 10;

		public Dictionary<StatType, int> allItems = new Dictionary<StatType, int>();

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Init & Process ----- \\

		private GameManager() : base() { }

		protected override void Init()
		{
			menuContainer.AddChild(choiceScreenScene.Instantiate());
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\

		public void UpdateStat(StatType pStat, int pAmout)
		{
			allItems[pStat] += pAmout;
			switch (pStat)
			{
				case StatType.Comfort:
                    allItems[pStat] = Mathf.Clamp(allItems[pStat] + pAmout, minComfortLevel, maxComfortLevel);
					break;
				case StatType.Social:
                    allItems[pStat] = Mathf.Clamp(allItems[pStat] + pAmout, minContactLebel, maxContactLevel);
					break;
				case StatType.Money:
                    allItems[pStat] = Mathf.Clamp(allItems[pStat] + pAmout, minMoneyLevel, maxContactLevel);
					break;
				default:
					if (allItems[pStat] <= 0) allItems.Remove(pStat);
					break;
			}
		}

		// ----- Destructor ----- \\

		public override void Destructor()
		{
			base.Destructor();
		}
	}
}
