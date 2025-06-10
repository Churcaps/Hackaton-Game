using Com.IsartDigital.Hackaton;
using Godot;
using System;

public partial class InGame : Control
{
    private PackedScene choiceScreenScene = GD.Load<PackedScene>("res://Scenes/ChoiceScreen.tscn");

    [Export] private TextureButton book, phone;
	[Export] private ColorRect livreImage, phoneImage;
	[Export] private Label cityString;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		book.Pressed += OpenBook;
		phone.Pressed += OpenPhone;
		livreImage.GetChild<Button>(0).Pressed += CloseBook;
		phoneImage.GetChild<Button>(0).Pressed += ClosePhone;

		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(this, "modulate", Colors.White, 0.8f);
		lTween.TweenInterval(0.33f);
		lTween.Finished += ShowChoices;

		cityString.Text = Manager.GetManager<GameManager>().cityName;
	}

	private void CloseBook()
	{
		livreImage.GetChild<Button>(0).Visible = false;
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(livreImage, "modulate", Colors.Transparent, 0.5f);
	}

	private void OpenBook()
	{
		livreImage.GetChild<Button>(0).Visible = true;
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(livreImage, "modulate", Colors.White, 0.5f);
	}

	private void ClosePhone()
	{
		phoneImage.GetChild<Button>(0).Visible = false;
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(phoneImage, "modulate", Colors.Transparent, 0.5f);
	}

	private void OpenPhone()
	{
		phoneImage.GetChild<Button>(0).Visible = true;
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(phoneImage, "modulate", Colors.White, 0.5f);
	}

	private void ShowChoices()
	{
		ChoiceScreen lScreen = choiceScreenScene.Instantiate<ChoiceScreen>();
		lScreen.Modulate = Colors.Transparent;
        AddChild(lScreen);
		Tween lTween = CreateTween();
		lTween.TweenProperty(lScreen, "modulate", Colors.White, 0.75f);
	}
}
