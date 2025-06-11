using Com.IsartDigital.Hackaton;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class InGame : Control
{
    private PackedScene choiceScreenScene = GD.Load<PackedScene>("res://Scenes/ChoiceScreen.tscn");

    [Export] private TextureButton book, phone, next;
	[Export] private ColorRect livreImage, phoneImage;
	[Export] private Label cityString;
	[Export] private Label bookTitleScoreLabel, bookScoreLabel, bookTitleObjectsLabel;
	[Export] private VBoxContainer bookRessourcesCont;
	[Export] private PackedScene foodScene, waterScene, clothingScene, HealthKitScene, TentScene;

	private ChoicesManager choicesManager;

	private int maxItemPerRow = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Manager.GetManager<ChoicesManager>().phoneImage = phoneImage;
		book.Pressed += OpenBook;
		phone.Pressed += OpenPhone;
		next.Pressed += NextPhase;
		livreImage.GetChild<Button>(0).Pressed += CloseBook;
		phoneImage.GetChild<Button>(0).Pressed += ClosePhone;

		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(this, "modulate", Colors.White, 0.8f);
		lTween.TweenInterval(0.33f);
		lTween.Finished += ShowChoices;

		cityString.Text = Manager.GetManager<GameManager>().cityName;
	}

	private void NextPhase()
	{
		Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quart).SetEase(Tween.EaseType.Out);
		lTween.TweenProperty(this, "modulate", Colors.Black, 0.5f);
		lTween.Finished += () => GetTree().ReloadCurrentScene();
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
		ShowScore();
		ShowObjects();
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

	private void ShowScore()
	{
		string lScoreText = "";
		lScoreText += StatType.Argent + " : " + GameManager.allItems[StatType.Argent];
		lScoreText += ", " + StatType.Confort + " : " + GameManager.allItems[StatType.Confort];
		lScoreText += ", " + StatType.Social + " : " + GameManager.allItems[StatType.Social];
		bookScoreLabel.Text = lScoreText;
	}

    private void ShowObjects()
    {
        foreach (Node lChild in bookRessourcesCont.GetChildren())
        {
            lChild.QueueFree();
        }
		
        List<StatType> displayItems = GameManager.allItems.Keys
            .Where(item => item != StatType.Confort && item != StatType.Social && item != StatType.Argent)
            .ToList();

        int totalItems = displayItems.Count;
        int numRows = Mathf.CeilToInt((float)totalItems / maxItemPerRow);

        for (int i = 0; i < numRows; i++)
        {
            HBoxContainer lHBoxCont = new HBoxContainer();
			lHBoxCont.Alignment = BoxContainer.AlignmentMode.Center;

            for (int j = 0; j < maxItemPerRow; j++)
            {
                int index = i * maxItemPerRow + j;
                if (index >= totalItems)
                    break;

                StatType currentItem = displayItems[index];
                PackedScene scene = null;

                switch (currentItem)
                {
                    case StatType.Tent:
                        scene = TentScene;
                        break;
                    case StatType.Cloting:
                        scene = clothingScene;
                        break;
                    case StatType.Food:
                        scene = foodScene;
                        break;
                    case StatType.HealthKit:
                        scene = HealthKitScene;
                        break;
                    case StatType.Water:
                        scene = waterScene;
                        break;
                    default:
                        break;
                }

                if (scene != null)
                {
                    lHBoxCont.AddChild(scene.Instantiate());
                    Label lLabel = new Label();
                    lLabel.Text = " x " + GameManager.allItems[currentItem].ToString();
					LabelSettings lLabelSet = new LabelSettings();
					lLabelSet.FontSize = 96;
					lLabelSet.FontColor = Colors.Black;
					lLabel.LabelSettings = lLabelSet;
                    lHBoxCont.AddChild(lLabel);
                }
            }

            bookRessourcesCont.AddChild(lHBoxCont);
        }
    }


    /*private void ShowObjects()
	{
		foreach (Node lChlid in bookRessourcesCont.GetChildren())
		{
			lChlid.QueueFree();
		}

		int lNumObj = GameManager.allItems.Count - 3;
		StatType[] lItems = GameManager.allItems.Keys.ToArray();
		StatType lCurrentItem;
		int lNumRows = Mathf.CeilToInt(lNumObj / maxItemPerRow);
		HBoxContainer lHBoxCont;
		PackedScene lScene;
		Label lLabel;
		
		for (int  i = 0; i < lNumRows; i++)
		{
			lHBoxCont = new HBoxContainer();
			
			for (int j = 0; j < lNumObj % maxItemPerRow; j++)
			{
				
				lCurrentItem = lItems[j + i*maxItemPerRow];
				if (lCurrentItem is StatType.Confort || lCurrentItem is StatType.Social || lCurrentItem is StatType.Argent)
					continue;
				switch(lCurrentItem)
				{
					case StatType.Tent:
						lScene = TentScene;
						break;
					case StatType.Cloting:
						lScene = clothingScene;
						break;
					case StatType.Food:
						lScene = foodScene;
						break;
					case StatType.HealthKit:
						lScene = HealthKitScene;
						break;
					case StatType.Water:
						lScene = waterScene;
						break;
					default:
						lScene = null;
						break;
				}
				lHBoxCont.AddChild(lScene.Instantiate());
				lLabel = new Label();
				lLabel.Text = " x " + GameManager.allItems[lCurrentItem].ToString();
				lHBoxCont.AddChild(lLabel);
			}
            bookRessourcesCont.AddChild(lHBoxCont);
            lNumObj -= maxItemPerRow;
		}
	}*/
}
