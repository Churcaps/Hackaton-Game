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
	[Export] private Label textStrasbourg, textRochelle, textFrejus;
	[Export] private ColorRect colorStras, colorRoche, colorFrej;

	private Cities currentCitySelected;
	private Label currentLabelToShow;
	private int numTotCharac;
	private float timeBetweenCarharcters = 0.05f;
	private float elapseTime;

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
		Cities lCity = Cities.NULL;

		switch (pIndex)
		{
			case 0:
				lCity = Cities.STRASBOURG;
				break;
			case 1:
				lCity = Cities.FREJUS;
				break;
			case 2:
				lCity = Cities.LAROCHELLE;
				break;
		}
		
		if (currentCitySelected == Cities.NULL)
		{
			ShowText(lCity);
			currentCitySelected = lCity;
		}
		else
		{
			if (currentCitySelected == lCity)
			{
				StartGame(pIndex);
			}
			else
			{
				HideText(currentCitySelected);
				ShowText(lCity);
				currentCitySelected = lCity;
			}
		}
	}

	private void HideText(Cities pCity)
	{
        Label lLabel = null;
		ColorRect lColor = null;
        switch (pCity)
        {
            case Cities.STRASBOURG:
				lLabel = textStrasbourg;
				lColor = colorStras;
                break;
            case Cities.FREJUS:
                lLabel = textFrejus;
				lColor = colorFrej;
                break;
            case Cities.LAROCHELLE:
                lLabel = textRochelle;
				lColor = colorRoche;
                break;
            case Cities.NULL:
                lLabel = null;
                break;
        }

		lLabel.Hide();
		lColor.Hide();
		currentLabelToShow = null;
    }

	private void ShowText(Cities pCity)
	{
		Label lLabel = null;
		ColorRect lColor = null;
		switch (pCity)
		{
			case Cities.STRASBOURG:
				lLabel = textStrasbourg;
				lColor = colorStras;
				break;
			case Cities.FREJUS:
				lLabel = textFrejus;
				lColor = colorFrej;
				break;
			case Cities.LAROCHELLE:
				lLabel = textRochelle;
				lColor = colorRoche;
				break;
			case Cities.NULL:
				lLabel = null;
				break;
		}

		lLabel.Show();
		lColor.Show();
        currentLabelToShow = lLabel;
        /*numTotCharac = lLabel.GetTotalCharacterCount();
		lLabel.VisibleCharacters = 0;
		elapseTime = 0;*/
    }

	private void StartGame(int pIndex)
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
	public override void _Process(double pDelta)
	{
		float lDelta = (float)pDelta;

		base._Process(pDelta);

		/*if (currentLabelToShow == null) return;
		if (currentLabelToShow.VisibleCharacters >= numTotCharac) return;

		elapseTime += lDelta;
		if (elapseTime >= timeBetweenCarharcters)
		{
			elapseTime = 0;
			currentLabelToShow.VisibleCharacters++;
		}*/
	}
}