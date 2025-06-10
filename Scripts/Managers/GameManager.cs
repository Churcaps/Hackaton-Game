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

		[Export] private Control menuContainer;

		// ----- Others ----- \\

		// Gameplay
		public string cityName;
		private int minComfortLevel = 0, maxComfortLevel = 10;
		private int minContactLebel = 0, maxContactLevel = 10;
		private int minMoneyLevel = 0, maxMoneyLevel = 10;

		public static Dictionary<StatType, int> allItems = new Dictionary<StatType, int>();

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Init & Process ----- \\

		private GameManager() : base() { }

		protected override void Init()
		{
            PrintAllStats();
        }

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\

		public static void SetBaseStats(int pMoney, int pComfort, int pSocial)
		{
			allItems.Add(StatType.Money, pMoney);
			allItems.Add(StatType.Comfort, pComfort);
			allItems.Add(StatType.Social, pSocial);
		}

		public void PrintAllStats()
		{
			foreach (StatType lStat in allItems.Keys)
			{
				GD.Print(lStat, " : x", allItems[lStat]);
			}
			GD.Print();
		}

		public void UpdateStat(StatType pStat, int pAmout)
		{
			if (!allItems.ContainsKey(pStat)) return;
			allItems[pStat] += pAmout;
			switch (pStat)
			{
				case StatType.Comfort:
                    allItems[pStat] = Mathf.Clamp(allItems[pStat], minComfortLevel, maxComfortLevel);
					break;
				case StatType.Social:
                    allItems[pStat] = Mathf.Clamp(allItems[pStat], minContactLebel, maxContactLevel);
					break;
				case StatType.Money:
                    allItems[pStat] = Mathf.Clamp(allItems[pStat], minMoneyLevel, maxContactLevel);
					break;
				default:
					if (allItems[pStat] <= 0) allItems.Remove(pStat);
					break;
			}
        }

		// ----- Destructor ----- \\

		public override void Destructor(bool Destroy = true)
		{
			base.Destructor(Destroy);
		}
	}
}
