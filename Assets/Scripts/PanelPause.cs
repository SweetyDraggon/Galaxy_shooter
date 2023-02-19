using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class PanelPause : MonoBehaviour
{/*
	public Image checkSound;

	public Image checkMusic;*/

	private void OnEnable()
	{
	/*	this.checkSound.sprite = GameManager.Instance.checks[GameManager.Instance.sound.GetHashCode()];
		this.checkMusic.sprite = GameManager.Instance.checks[GameManager.Instance.music.GetHashCode()];*/
	}

	public void ClickResume(Button btn)
	{
		btn.Click(delegate
		{
			GamePlayManager.state = StateGame.PLAY;
            GamePlayManager.isPause = false;
			this.Hide();
		});
	}

	public void ClickAgain(Button btn)
	{
		btn.Click(delegate
		{
			ResetLevel.Instance.RestartLevel();
			//StartCoroutine(Restart());
			

			/*this.Hide();
			GamePlayManager.isPause = false;
			GameManager.Instance.loadManager.loadMap.ResetData();
			GameManager.Instance.loadManager.loadMap.Init();
			PlayerController.Instance.OnInit();*/
		});
	}
	public void ClickAgainInfinity(Button btn)
	{
		 ResetLevel.Instance.RestartLevelInfinity();
	}


		/*private IEnumerator Restart()
		{
			Debug.Log("Before restart");
			ResetLevel.RestartLevel();
			Debug.Log("After restart");

			yield return new WaitForSeconds(1f);

			Debug.Log("Something happening 1");
			this.Hide();
			GamePlayManager.isPause = false;
			GameManager.Instance.loadManager.loadMap.ResetData();
			GameManager.Instance.loadManager.loadMap.Init();
			PlayerController.Instance.OnInit();
			Debug.Log("Something happened");
		}*/

		public void ClickMenu(Button btn)
	{
		btn.Click(delegate
		{
			this.Hide();
            GamePlayManager.isPause = false;
            if (GameManager.Instance.isEndless)
			{
				int param = GamePlay.Instance.gamePlayManager.score / 2;
				GameManager.Instance.ChangeCoin(param);
				GameManager.Instance.ChangeState(Scene.HOME);
			}
			else
			{
				GameManager.Instance.ChangeState(Scene.SELECTLEVEL);//Scene.HOME);
			}
			
		});
	}
}
