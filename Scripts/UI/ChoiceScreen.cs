using Godot;
using System;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	public partial class ChoiceScreen : Control
	{
        // ---------- VARIABLES ---------- \\

        // ----- Paths ----- \\

        // ----- Nodes ----- \\

        [Export] public Label probName;
		[Export] public Label probText;
		[Export] public TextureButton[] choiceButtons;
		[Export] public Label[] choiceTexts;
		[Export] public Label[] choiceCosts;
		[Export] public Label[] choiceNames;
		[Export] public TextureRect resultColorRect;

        [Export] private VBoxContainer[] choicesContainers;

        // ----- Others ----- \\

        // ---------- FUNCTIONS ---------- \\

        // ----- Constructor & Ready & Process ----- \\

        protected ChoiceScreen () : base() { }

		public override void _Ready()
		{
			base._Ready();

			Manager.GetManager<ChoicesManager>().SetChoiceScreen(this);
			Manager.GetManager<ChoicesManager>().resultColorRect2 = resultColorRect;

			Tween lTween = CreateTween().SetTrans(Tween.TransitionType.Bounce).SetEase(Tween.EaseType.Out).SetParallel();
			Vector2 lPivotPos;
			int i = 0;
			foreach (VBoxContainer lBox in choicesContainers)
			{
				i++;
                lPivotPos = new Vector2(lBox.Size.X * 0.5f, lBox.Size.Y * 0.25f);
				lBox.PivotOffset = lPivotPos;
				lBox.Scale = Vector2.Zero;
				lTween.TweenProperty(lBox, "scale", Vector2.Zero, 0f);
				lTween.TweenProperty(lBox, "scale", Vector2.One, 1f).SetDelay(0.2f * i).FromCurrent();
			}
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\
		
		// ----- Destructor ----- \\

		protected override void Dispose(bool pDisposing)
		{
			base.Dispose(pDisposing);
		}
	}
}
