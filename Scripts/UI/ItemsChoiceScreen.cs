using Godot;
using System;
using System.Collections.Generic;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	public partial class ItemsChoiceScreen : Control
	{
        // ---------- VARIABLES ---------- \\

        // ----- Paths ----- \\

        [Export] private PackedScene inGameScene;

        // ----- Nodes ----- \\

        [Export] private Button nextButton;
		[Export] private Button[] itemsButtons;

		// ----- Others ----- \\

		private List<Button> buttonsPressed = new List<Button>();

		private Dictionary<string, StatType> allItems = new Dictionary<string, StatType>()
		{
			{"Clothing", StatType.Cloting }, {"Tent", StatType.Tent}, {"Food", StatType.Food },
			{"HealthKit", StatType.HealthKit }, {"Water", StatType.Water}
		};

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Process ----- \\

		protected ItemsChoiceScreen () : base() { }

		public override void _Ready()
		{
			base._Ready();

			nextButton.Pressed += ButtonNextPressed;
			foreach (Button lButton in itemsButtons)
			{
				lButton.Toggled += (bool pState) => ItemsButtonPressed(pState, lButton);
            }
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\

		private void ButtonNextPressed()
		{
            nextButton.Pressed -= ButtonNextPressed;
			foreach (Button lButton in itemsButtons)
			{
				if (lButton.ButtonPressed) GameManager.UpdateStat(allItems[lButton.Name], 1);
			}
            Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
            lTween.TweenProperty(this, "modulate", Colors.Black, 0.8f);
			lTween.Finished += () =>
			{
				GetTree().ChangeSceneToPacked(inGameScene);
			};
        }

		private void ItemsButtonPressed(bool pState, Button lButton)
		{
			if (pState) buttonsPressed.Add(lButton);
			else buttonsPressed.Remove(lButton);
            if (buttonsPressed.Count >= 3) ToggleDisableButtons(true);
            else ToggleDisableButtons(false);
        }

		private void ToggleDisableButtons(bool pState)
		{
			foreach (Button lButton in itemsButtons)
			{
				if (buttonsPressed.Contains(lButton)) continue;
				lButton.Disabled = pState;
				if (pState) lButton.Modulate = new Color(1, 1, 1, 0.5f);
                else lButton.Modulate = new Color(1, 1, 1, 1);
			}
		}

		// ----- Destructor ----- \\

		protected override void Dispose(bool pDisposing)
		{
			base.Dispose(pDisposing);
		}
	}
}
