using Godot;

// Author : Auguste Paccapelo

namespace Com.IsartDigital.Hackaton
{
	[GlobalClass]
    public partial class ProblemRessource : Resource
	{
		[Export] public string name;
		[Export] public string text;
		[Export] public ChoicesRessource[] allChoices;
	}
}
