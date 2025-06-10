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

		private const string PROB_PATH = "res://Assets/Ressource/Problemes/Event1.tres";
		private ProblemRessource probRes = GD.Load<ProblemRessource>(PROB_PATH);
		public ColorRect resultColorRect2;

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
            for (int i = 0; i < lNumChoice; i++)
            {
				int lIndex = i;
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

				pScreen.choiceButtons[lIndex].Pressed += () => ButtonPressed(probRes.allChoices[lIndex]);
            }
        }

		private void ButtonPressed(ChoicesRessource pChoice)
		{
			int lLength = pChoice.typesChanges.Count;
			for (int i = 0; i < lLength; i++)
			{
				GetManager<GameManager>().UpdateStat(pChoice.typesChanges[i], -pChoice.amoutChanges[i]);
			}

			ShowResult(pChoice);
		}

		private void ShowResult(ChoicesRessource pChoice)
		{
			resultColorRect2.Visible = true;

			resultColorRect2.GetChild<Label>(0).Text = pChoice.resultString;

			for (int i = 0; i < pChoice.resultAmount.Count(); i++)
			{
				resultColorRect2.GetChild<Label>(1).Text += "\n" + pChoice.resultAmount[i].ToString() + " " + pChoice.resultType[i].ToString();
			}

			Tween lTWeen = CreateTween();
			GetParent<Node2D>().GetParent<Control>().GetChild<TextureButton>(3).Visible = true;
			lTWeen.TweenProperty(GetParent<Node2D>().GetParent<Control>().GetChild<TextureButton>(3), "modulate", Colors.White, 0.5f);
		}

		// ----- Destructor ----- \\

		public override void Destructor(bool Destroy = true)
		{
			base.Destructor(Destroy);
		}
	}
}
