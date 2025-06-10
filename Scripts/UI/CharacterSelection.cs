//using System.Collections.Generic;
using Com.IsartDigital.Hackaton;
using Com.IsartDigital.ProjectName;
using Godot;
using Godot.Collections;

public partial class CharacterSelection : Control
{
	[Export] public Array<TextureButton> charactersButton = new Array<TextureButton>();
	[Export] public TextureButton closeButton, playButton;
	[Export] public Label characterInfo;
	[Export] public Button skipButton;
	[Export] private PackedScene inGameScene;

	private int currentCharacterCount = 0;

	private float timeDelta = 0.8f;
	public bool canShowText;
	private bool returncharactercheck;
	private Timer characterSelectTimer;
	private Vector2 playSTartPos;
	public Vector2 spawnPlayPos = new Vector2(1073, 1182);

	private Dictionary<CharactersEnum, Dictionary<StatType, int>> charactersStats = new Dictionary<CharactersEnum, Dictionary<StatType, int>>()
	{
		{CharactersEnum.OldLady, new Dictionary<StatType, int>() { {StatType.Money, 6 }, {StatType.Social, 5 }, {StatType.Comfort, 2 } } },
        {CharactersEnum.Rich, new Dictionary<StatType, int>() { {StatType.Money, 8 }, {StatType.Social, 1 }, {StatType.Comfort, 6 } } },
        {CharactersEnum.Student, new Dictionary<StatType, int>() { {StatType.Money, 1 }, {StatType.Social, 8 }, {StatType.Comfort, 6 } } }
    };

	public CharactersEnum characterSelected;

	public override void _Ready()
	{
		closeButton.Pressed += ReturnCharacterSelection;
		playButton.Pressed += StartGame;
		skipButton.Pressed += SkipText;

		playSTartPos = playButton.Position;

		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out).SetParallel(true);

		for (int i = 0; i < charactersButton.Count; i++)
		{
			charactersButton[i].Scale = Vector2.Zero;

			lTween.TweenProperty(charactersButton[i], "scale", Vector2.One * 0.8f, 0.7f).SetDelay(0.2 * i);
		}

		characterSelectTimer = new Timer();
		characterSelectTimer.WaitTime = 1f;
		characterSelectTimer.OneShot = true;
		AddChild(characterSelectTimer);

		characterSelectTimer.Timeout += () => returncharactercheck = false;
	}

	private void SkipText()
	{
		canShowText = false;
		characterInfo.VisibleCharacters = characterInfo.GetTotalCharacterCount();
	}

	private void StartGame()
	{
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(this, "modulate", Colors.Black, 0.8f);
		lTween.Finished += () =>
		{
			GetTree().ChangeSceneToPacked(inGameScene);
		};
		Dictionary<StatType, int> lStats = charactersStats[characterSelected];
		GameManager.SetBaseStats(lStats[StatType.Money], lStats[StatType.Comfort], lStats[StatType.Social]);
	}

	private void ReturnCharacterSelection()
	{
		if (!returncharactercheck)
		{
			skipButton.Visible = false;
			returncharactercheck = true;

			Tween lTween2 = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out).SetParallel();

			canShowText = false;
			characterInfo.Visible = false;
			currentCharacterCount = 0;

			for (int i = 0; i < charactersButton.Count; i++)
			{
				CharacterButton myButton = (CharacterButton)charactersButton[i];

				lTween2.TweenProperty(charactersButton[i], "scale", Vector2.One * 0.8f, 0.7f).SetDelay(0.2 * i);
				lTween2.TweenProperty(charactersButton[i], "position", myButton.characterPos, 0.5f);
			}

			Tween lTween3 = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out).SetParallel();

			lTween3.TweenProperty(closeButton, "modulate", Colors.Transparent, 0.5f);
			lTween3.TweenProperty(playButton, "modulate", Colors.Transparent, 0.5f);
			lTween3.TweenProperty(playButton, "position", playSTartPos, 0.5f);
			lTween3.Finished += () =>
			{
				closeButton.Visible = false;
				playButton.Visible = false;
			};

			characterSelectTimer.Start();
		}
	}

    public override void _Process(double delta)
    {
		timeDelta += (float)delta;

		if (!canShowText) return;

		if (timeDelta >= 0.005f)
		{
			currentCharacterCount += 1;
			ShowCharacterInfo();
			timeDelta = 0f;
		}
    }

	private void ShowCharacterInfo()
	{
		characterInfo.VisibleCharacters = currentCharacterCount;
	}
}