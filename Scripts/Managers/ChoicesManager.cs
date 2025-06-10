using Godot;
using System;
using System.Linq;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	public partial class ChoicesManager : Manager
	{
		// ---------- VARIABLES ---------- \\

		// ----- Paths ----- \\

		private const string PROB_PATH = "res://Assets/Ressource/Problemes/TestProb1.tres";
		private ProblemRessource probRes = GD.Load<ProblemRessource>(PROB_PATH);

		// ----- Nodes ----- \\

		// ----- Others ----- \\

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Init & Process ----- \\

		private ChoicesManager() : base() { }

		protected override void Init()
		{
			
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\

		public void SetChoiceScreen(ChoiceScreen pScreen)
		{
			pScreen.probName.Text = probRes.name;
            pScreen.probText.Text = probRes.text;

			int lNumChoice = probRes.allChoices.Length;
			int lNumChanges;
			string lChoiceCost;
			int lIndex;
            for (int i = 0; i < lNumChoice; i++)
            {
				lIndex = i;
				pScreen.choiceNames[i].Text = probRes.allChoices[i].name;
				pScreen.choiceTexts[i].Text = probRes.allChoices[i].text;
				lNumChanges = probRes.allChoices[i].typesChanges.Count;
				lChoiceCost = "";
                for (int j = 0; j < lNumChanges; j++)
                {
					lChoiceCost += probRes.allChoices[i].typesChanges[j].ToString();
					lChoiceCost += " : " + probRes.allChoices[i].amoutChanges[j].ToString();
					if (j != lNumChanges - 1) lChoiceCost += "\n";
                }
                pScreen.choiceCosts[i].Text = lChoiceCost;

				pScreen.choiceNames[lIndex].Pressed += () => ButtonPressed(probRes.allChoices[lIndex]);
            }
        }

		private void ButtonPressed(ChoicesRessource pChoice)
		{
			GD.Print(pChoice.amoutChanges[0], " ", pChoice.typesChanges[0]);
		}

		// ----- Destructor ----- \\

		public override void Destructor()
		{
			base.Destructor();
		}
	}
}
