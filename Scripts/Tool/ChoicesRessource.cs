using Godot;
using System;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	[GlobalClass]
	public partial class ChoicesRessource : Resource
	{
		[Export] public Godot.Collections.Array<StatType> typesChanges;
		[Export] public int[] amoutChanges;
		[Export] public int choiceWeight;
	}
}
