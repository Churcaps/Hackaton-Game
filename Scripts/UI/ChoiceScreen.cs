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
		[Export] public Button[] choiceNames;
		[Export] public Label[] choiceTexts;
		[Export] public Label[] choiceCosts;

        // ----- Others ----- \\

        // ---------- FUNCTIONS ---------- \\

        // ----- Constructor & Ready & Process ----- \\

        protected ChoiceScreen () : base() { }

		public override void _Ready()
		{
			base._Ready();

			Manager.GetManager<ChoicesManager>().SetChoiceScreen(this);
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
