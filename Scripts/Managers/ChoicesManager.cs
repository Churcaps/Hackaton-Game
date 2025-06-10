using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

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
		public ColorRect phoneImage;

		private static int contact;

		// ----- Nodes ----- \\

		[Export] InGame inGameScreen;

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
			if (contact >= 1)
			{
				phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(0).Visible = true;
			}

			if (contact == 2)
			{
				phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(1).Visible = true;
			}

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
				GameManager.UpdateStat(pChoice.typesChanges[i], -pChoice.amoutChanges[i]);
			}

			ShowResult(pChoice);
		}

		public static string GetDisplayName<T>(T lEnvironmentEnum)
        {
            return lEnvironmentEnum.GetType().GetMember(lEnvironmentEnum.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;
        }

		private void ShowResult(ChoicesRessource pChoice)
		{
			resultColorRect2.Visible = true;

			resultColorRect2.GetChild<Label>(0).Text = pChoice.resultString;

			for (int i = 0; i < pChoice.resultAmount.Count(); i++)
			{
				if (GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]) == "Contact (Philippe)")
				{
					phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(0).Visible = true;
					contact += 1;
				}

				if (GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]) == "Contact (Geneviève)")
				{
					phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(1).Visible = true;
					contact += 1;
				}

				resultColorRect2.GetChild<Label>(1).Text += "\n" + pChoice.resultAmount[i].ToString() + " " + GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]);
			}

            Tween lTWeen = CreateTween();
            GetParent<Node2D>().GetParent<Node2D>().GetChild<CanvasLayer>(1).GetChild<Control>(0).GetChild<Control>(0).GetChild<TextureButton>(3).Visible = true;
            lTWeen.TweenProperty(GetParent<Node2D>().GetParent<Node2D>().GetChild<CanvasLayer>(1).GetChild<Control>(0).GetChild<Control>(0).GetChild<TextureButton>(3), "modulate", Colors.White, 0.5f);
        }

		// ----- Destructor ----- \\

		public override void Destructor(bool Destroy = true)
		{
			base.Destructor(Destroy);
		}
	}
}
