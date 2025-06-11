//using System.Collections.Generic;
using Com.IsartDigital.Hackaton;
using Com.IsartDigital.ProjectName;
using Godot;
using Godot.Collections;

public partial class CharacterSelection : Control
{
	[Export] public Array<TextureButton> charactersButton = new Array<TextureButton>();
	[Export] public Array<Vector2> charactersButtonPos = new Array<Vector2>();
	[Export] public Array<Texture2D> charactersBackgrounds = new Array<Texture2D>();
	[Export] public TextureButton closeButton, playButton;
	[Export] public Label characterInfo;
	[Export] public Button skipButton;
	[Export] private PackedScene itemsChoiceScreenScene;

	private int currentCharacterCount = 0;

	private float timeDelta = 0.8f;
	public bool canShowText;
	private bool returncharactercheck;
	private Timer characterSelectTimer;
	private Vector2 playSTartPos;
	public Vector2 spawnPlayPos = new Vector2(1073, 1182);

	private Vector2 defaultPos;

	private Dictionary<CharactersEnum, Dictionary<StatType, int>> charactersStats = new Dictionary<CharactersEnum, Dictionary<StatType, int>>()
	{
		{CharactersEnum.OldLady, new Dictionary<StatType, int>() { {StatType.Argent, 6 }, {StatType.Social, 5 }, {StatType.Confort, 2 } } },
		{CharactersEnum.Rich, new Dictionary<StatType, int>() { {StatType.Argent, 8 }, {StatType.Social, 1 }, {StatType.Confort, 6 } } },
		{CharactersEnum.Student, new Dictionary<StatType, int>() { {StatType.Argent, 1 }, {StatType.Social, 8 }, {StatType.Confort, 6 } } }
	};

	public CharactersEnum characterSelected;

	public bool bahahah;

	public override void _Ready()
	{
		for (int i = 0; i < charactersButton.Count; i++)
		{
			charactersButtonPos.Add(charactersButton[i].Position);
		}

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
		floatinganim();
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
			AddChild(itemsChoiceScreenScene.Instantiate());
			Tween lTween2 = CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
			lTween2.TweenProperty(this, "modulate", Colors.White, 0.8f);
		};

		Dictionary<StatType, int> lStats = charactersStats[characterSelected];
		GameManager.SetBaseStats(lStats[StatType.Argent], lStats[StatType.Confort], lStats[StatType.Social]);
		switch (characterSelected)
		{
			case CharactersEnum.OldLady:
				ChoicesManager.backgroundCharacterSelected = charactersBackgrounds[1];
				break;

			case CharactersEnum.Rich:
				ChoicesManager.backgroundCharacterSelected = charactersBackgrounds[0];
				break;

			case CharactersEnum.Student:
				ChoicesManager.backgroundCharacterSelected = charactersBackgrounds[2];
				break;
		}
	}

	private void ReturnCharacterSelection()
	{
		if (!returncharactercheck)
		{
			bahahah = false;
			skipButton.Visible = false;
			returncharactercheck = true;

			Tween lTween2 = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out).SetParallel();

			GetChild<TextureRect>(1).Visible = true;
			lTween2.TweenProperty(GetChild<TextureRect>(1), "modulate", new Color(0.551f, 0.551f, 0.551f, 1f), 0.8f);

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
				floatinganim();
			};

			characterSelectTimer.Start();
		}
	}

	Tween lTween;

	private void floatinganim()
	{
		if (bahahah) return;

		lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetParallel();

		for (int i = 0; i < charactersButton.Count; i++)
		{
			lTween.TweenProperty(charactersButton[i], "position", charactersButtonPos[i] + new Vector2(0, -40), 0.8f).SetDelay(0.2 * i);
			lTween.TweenProperty(charactersButton[i], "position", charactersButtonPos[i], 0.8f).SetDelay((0.2 * i) + 0.8F);
		}

		lTween.Finished += floatinganim;
	}

	public void stoptween()
	{
		for (int i = 0; i < charactersButton.Count; i++)
		{
			lTween.Kill();
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

	public void LoadStats()
	{
		Dictionary<StatType, int> lStats = charactersStats[characterSelected];
		string lText = "";
		foreach (StatType lStat in lStats.Keys)
		{
			lText += lStat.ToString() + " : " + lStats[lStat].ToString() + "\n";
		}
		characterInfo.Text = lText;
	}
}