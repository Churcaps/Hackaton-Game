using Godot;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	[GlobalClass]
	public partial class ChoicesRessource : Resource
	{
		[Export] public string name;
		[Export] public string text;
		[Export] public Godot.Collections.Array<StatType> typesChanges;
		[Export] public int[] amoutChanges;
		[Export] public string resultString;
		[Export] public Godot.Collections.Array<StatType> resultType;
		[Export] public int[] resultAmount;
	}
}
