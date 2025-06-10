using Com.IsartDigital.Hackaton;
using Godot;
using Godot.Collections;
using System;

public partial class CarteSystem : Control
{
	[Export] private Array<Button> mapButtons = new Array<Button>();
	[Export] private Array<string> stringNames = new Array<string>();
	[Export] private PackedScene inGameScene;
	[Export] private Label textInfo, textStats;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(this, "modulate", Colors.White, 0.8f);


		for (int i = 0; i < mapButtons.Count; i++)
		{
			int index = i;

			mapButtons[i].Pressed += () => SelectMap(index);
		}
	}

	private void SelectMap(int pIndex)
	{
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(this, "modulate", Colors.Black, 0.8f);
		lTween.Finished += () =>
		{
			Manager.GetManager<GameManager>().cityName = stringNames[pIndex];
			GetTree().ChangeSceneToPacked(inGameScene);
		};

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}