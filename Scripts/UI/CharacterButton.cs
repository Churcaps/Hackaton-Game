using Godot;
using System;

// Author : Arnaud Partakelidis

namespace Com.IsartDigital.ProjectName
{
	public partial class CharacterButton : TextureButton
	{
		public Vector2 characterPos;

		private bool currentlyAnimateButtonPressed;
		private Timer characterSelectTimer;
		
		public override void _Ready()
		{
			characterPos = Position;

			Pressed += PlayButtonPress;

			characterSelectTimer = new Timer();
			characterSelectTimer.WaitTime = 1f;
			characterSelectTimer.OneShot = true;
			AddChild(characterSelectTimer);

			characterSelectTimer.Timeout += () => currentlyAnimateButtonPressed = false;
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

		}

		protected override void Dispose(bool pDisposing)
		{

		}

		private void PlayButtonPress()
		{
			if (!currentlyAnimateButtonPressed)
			{
				GetParent<CharacterSelection>().skipButton.Visible = true;
				currentlyAnimateButtonPressed = true;

				for (int i = 0; i < GetParent<CharacterSelection>().charactersButton.Count; i++)
				{
					if (GetParent<CharacterSelection>().charactersButton[i] == this)
					{
						Tween lTween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out).SetParallel();

						lTween.TweenProperty(this, "scale", Vector2.One * 1.1f, 0.5f);
						lTween.TweenProperty(this, "position", Vector2.Zero, 0.5f);

						GetParent<CharacterSelection>().canShowText = true;
						GetParent<CharacterSelection>().characterInfo.Visible = true;

						switch (i)
						{
							case 0:
								GetParent<CharacterSelection>().characterSelected = Hackaton.CharactersEnum.Rich;
                                break;
							case 1:
                                GetParent<CharacterSelection>().characterSelected = Hackaton.CharactersEnum.OldLady;
                                break;
							case 2:
                                GetParent<CharacterSelection>().characterSelected = Hackaton.CharactersEnum.Student;
                                break;
						}

						continue;
					}

					Tween lTween2 = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out).SetParallel();
					lTween2.TweenProperty(GetParent<CharacterSelection>().charactersButton[i], "scale", Vector2.Zero, 0.2f);

					GetParent<CharacterSelection>().closeButton.Visible = true;
					GetParent<CharacterSelection>().playButton.Visible = true;
					lTween2.TweenProperty(GetParent<CharacterSelection>().closeButton, "modulate", Colors.White, 0.5f);
					lTween2.TweenProperty(GetParent<CharacterSelection>().playButton, "modulate", Colors.White, 0.5f);
					lTween2.TweenProperty(GetParent<CharacterSelection>().playButton, "position", GetParent<CharacterSelection>().spawnPlayPos, 0.5f);
				}

				characterSelectTimer.Start();
			}
		}
			
	}
}
