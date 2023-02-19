using System;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class PanelComplete : MonoBehaviour
{
	[SerializeField]
	private Text txtScore;

	[SerializeField]
	private Text txtHighScore;

	[SerializeField]
	private Text txtCoin;

	public GameObject winPanel;

	private void OnEnable()
	{
		EventDispatcher.Instance.PostEvent(EventID.Disable, null);

		this.txtScore.text = GamePlay.Instance.gamePlayManager.txtScore.text;
		if (GamePlay.Instance.gamePlayManager.score >= PlayerPrefs.GetInt("high"))
		{
			PlayerPrefs.SetInt("high", GamePlay.Instance.gamePlayManager.score);
		}
		this.txtHighScore.text = Localisation.GetString("High_Score:") + " "+ PlayerPrefs.GetInt("high");
		int num = GamePlay.Instance.gamePlayManager.score / 2;
		GameManager.Instance.ChangeCoin(num);
		this.txtCoin.text =Localisation.GetString("Coin:")+ " " + num;
		GameManager.Instance.selectLevelManager.SetNextLevel();

		if (GameManager.Instance.level >= 45)
		{
			winPanel.SetActive(true);
		}

		GamePlayManager.Instance.HideWhenWin();
	}

	public void ClickHome(Button btn)
	{
		this.Hide();
		btn.Click(delegate
		{
			GameManager.Instance.ChangeState(Scene.SELECTLEVEL);
		});
	}

	public void ClickNext(Button btn)
	{
		this.Hide();
		if (GameManager.Instance.level == GameManager.Instance.selectLevelManager.btnLevels.Length)
		{
		//	this.Hide();
			GameManager.Instance.ChangeState(Scene.SELECTLEVEL);
			return;
		}
		btn.Click(delegate
		{
		//	this.Hide();
			GameManager.Instance.level++;
			ResetLevel.Instance.RestartLevel();
			//GameManager.Instance.ChangeState(Scene.GAMEPLAY);
		});
	}

	public void ClickRestart(Button btn)
	{
		this.Hide();
		btn.Click(delegate
		{
			ResetLevel.Instance.RestartLevel();

/*
		//	this.Hide();
			GameManager.Instance.loadManager.loadMap.ResetData();
			GameManager.Instance.loadManager.loadMap.Init();
			PlayerController.Instance.OnInit();*/
		});
	}
}
