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

		// ----- Nodes ----- \\

		// ----- Others ----- \\

		// Gameplay
		public int comfortLevel, contactLevel, moneyLevel;

		private int minComfortLevel = -5, maxComfortLevel = 5;
		private int minContactLebel = -5, maxContactLevel = 5;
		private int minMoneyLevel = 0, maxMoneyLevel = 10;

		public List<ItemType> allItems = new List<ItemType>();

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Init & Process ----- \\

		private GameManager() : base() { }

		protected override void Init() { }

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\

		public void UpdateStat(StatType pStat, int pAmout)
		{
			switch (pStat)
			{
				case StatType.Comfort:
					comfortLevel = Mathf.Clamp(comfortLevel + pAmout, minComfortLevel, maxComfortLevel);
					break;
				case StatType.Contact:
					contactLevel = Mathf.Clamp(contactLevel + pAmout, minContactLebel, maxContactLevel);
					break;
				case StatType.Money:
					moneyLevel = Mathf.Clamp(moneyLevel + pAmout, minMoneyLevel, maxContactLevel);
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
