using Godot;
using System;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	public partial class ChoicesManager : Manager
	{
		// ---------- VARIABLES ---------- \\

		// ----- Paths ----- \\

		// ----- Nodes ----- \\

		// ----- Others ----- \\

		// ---------- FUNCTIONS ---------- \\

		// ----- Constructor & Ready & Init & Process ----- \\

		private ChoicesManager() : base() { }

		protected override void Init() { }

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

			base._Process(lDelta);
		}

		// ----- My Functions ----- \\

		// ----- Destructor ----- \\

		public override void Destructor()
		{
			base.Destructor();
		}
	}
}
