using DocumentFormat.OpenXml.Drawing;
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

		private const string EVENT_1_PATH = "res://Assets/Ressource/Problemes/Event1.tres";
		private const string EVENT_2_PATH = "res://Assets/Ressource/Problemes/Event2.tres";
		private const string EVENT_3_PATH = "res://Assets/Ressource/Problemes/Event3.tres";
		private static ProblemRessource probRes = GD.Load<ProblemRessource>(EVENT_1_PATH);
		public ColorRect resultColorRect2;
		public ColorRect phoneImage;

		bool hasContact;

		private static int currenttEvent = 0;

		private static Godot.Collections.Array<StatType> contacts = new Godot.Collections.Array<StatType>();

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
			for (int i = 0; i < pScreen.choiceCosts.Count(); i++)
			{
				pScreen.choiceCosts[i].GetParent<VBoxContainer>().Visible = false;
			}

			if (contacts.Contains(StatType.Contact1))
			{
				phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(0).Visible = true;
			}

			if (contacts.Contains(StatType.Contact2))
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
				if (i == 3)
				{
					for (int j = 0; j < probRes.allChoices[i].typesChanges.Count; j++)
					{
						if (contacts.Contains(probRes.allChoices[i].typesChanges[j]))
						{
							hasContact = true;
							break;
						}
					}
				}
				else
				{
					hasContact = true;
				}

				if (!hasContact) return;

				hasContact = false;
				pScreen.choiceCosts[i].GetParent<VBoxContainer>().Visible = true;
				int lIndex = i;
				pScreen.choiceNames[i].Text = probRes.allChoices[i].name;
				pScreen.choiceTexts[i].Text = probRes.allChoices[i].text;
				lNumChanges = probRes.allChoices[i].typesChanges.Count;
				lChoiceCost = "";
                for (int j = 0; j < lNumChanges; j++)
                {
					if (probRes.allChoices[i].typesChanges[j] == StatType.Contact1 || probRes.allChoices[i].typesChanges[j] == StatType.Contact2)
					{
						lChoiceCost += "Retire " + GetDisplayName<Com.IsartDigital.Hackaton.StatType>(probRes.allChoices[i].typesChanges[j]);
						if (j != lNumChanges - 1) lChoiceCost += "\n";
						continue;
					}

					lChoiceCost += GetDisplayName<Com.IsartDigital.Hackaton.StatType>(probRes.allChoices[i].typesChanges[j]);
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

			currenttEvent += 1;

			switch (currenttEvent)
			{
				case 1:
					probRes = GD.Load<ProblemRessource>(EVENT_2_PATH);
					break;

				case 2:
					probRes = GD.Load<ProblemRessource>(EVENT_3_PATH);
					break;

				case 3:
					GD.Print("FINISHED");
					break;
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

			for (int i = 0; i < pChoice.typesChanges.Count; i++)
			{
				if (pChoice.typesChanges[i] == StatType.Contact1)
				{
					contacts.Remove(pChoice.typesChanges[i]);
					phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(0).Visible = false;
				}

				if (pChoice.typesChanges[i] == StatType.Contact2)
				{
					contacts.Remove(pChoice.typesChanges[i]);
					phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(1).Visible = false;
				}
			}

			for (int i = 0; i < pChoice.resultAmount.Count(); i++)
			{
				if (GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]) == "Contact (Philippe)")
				{
					phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(0).Visible = true;
					contacts.Add(StatType.Contact1);
				}

				if (GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]) == "Contact (Geneviève)")
				{
					phoneImage.GetChild<VBoxContainer>(3).GetChild<TextureButton>(1).Visible = true;
					contacts.Add(StatType.Contact2);
				}

				if (pChoice.resultAmount[i] >= 0)
				{
					resultColorRect2.GetChild<Label>(1).Text += "\n" + "+" + pChoice.resultAmount[i].ToString() + " " + GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]);
				}
				else
				{
					resultColorRect2.GetChild<Label>(1).Text += "\n" + pChoice.resultAmount[i].ToString() + " " + GetDisplayName<Com.IsartDigital.Hackaton.StatType>(pChoice.resultType[i]);
				}
			}

			if (currenttEvent < 3)
			{
				Tween lTWeen = CreateTween();
				GetParent<Node2D>().GetParent<Node2D>().GetChild<CanvasLayer>(1).GetChild<Control>(0).GetChild<Control>(0).GetChild<TextureButton>(3).Visible = true;
				lTWeen.TweenProperty(GetParent<Node2D>().GetParent<Node2D>().GetChild<CanvasLayer>(1).GetChild<Control>(0).GetChild<Control>(0).GetChild<TextureButton>(3), "modulate", Colors.White, 0.5f);
			}

			GetManager<GameManager>().PrintAllStats();
        }

		// ----- Destructor ----- \\

		public override void Destructor(bool Destroy = true)
		{
			base.Destructor(Destroy);
		}
	}
}
