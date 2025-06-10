using Godot;
using System;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	public partial class ItemsChoiceScreen : Control
	{
		// ---------- VARIABLES ---------- \\

		// ----- Paths ----- \\

		// ----- Nodes ----- \\

		// ----- Others ----- \\

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Process ----- \\

		protected ItemsChoiceScreen () : base() { }

		public override void _Ready()
		{
			base._Ready();
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
