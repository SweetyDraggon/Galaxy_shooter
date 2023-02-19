using System;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class PanelGameOver : MonoBehaviour
{
	[SerializeField]
	private Text txtScore;

	[SerializeField]
	private Text txtHighScore;
	[SerializeField]
	private Text txtHighScoreinfin;
	public PanelMoreLive moreLive;
	public GamePlayManager game;

	public int bestscoreinfin;
	public Button btnMoreLive;

	private void OnEnable()
	{
		Debug.Log("1");
		//this.btnMoreLive.gameObject.SetActive(!GameManager.Instance.isEndless);
		this.txtScore.text = game.txtScore.text;
		int score =game.score;
		
		if (GamePlayManager.isPauseInfin == false)
		{
			Debug.Log("4");
			this.txtHighScore.text = Localisation.GetString("High_Score:") + " " + PlayerPrefs.GetInt("high");
		}
		if (GamePlayManager.isPauseInfin == true)
		{
			Debug.Log("5");
			if (PlayerPrefs.HasKey("hightScoreInfin"))
			{
				Debug.Log("2");
				bestscoreinfin = PlayerPrefs.GetInt("hightScoreInfin");
				Debug.Log(bestscoreinfin);
				if (bestscoreinfin < score)
				{
					PlayerPrefs.SetInt("hightScoreInfin", score);
					Debug.Log("3");
				}
			}
			else
			{
				PlayerPrefs.SetInt("hightScoreInfin", score);
			}
			this.txtHighScore.text = Localisation.GetString("High_Score:") + " " + PlayerPrefs.GetInt("hightScoreInfin");
		}
    
	}

	public void ClickHome(Button btn)
	{
		btn.Click(delegate
		{
			this.Hide();
			if (GameManager.Instance.isEndless)
			{
				int param = GamePlay.Instance.gamePlayManager.score / 2;
				GameManager.Instance.ChangeCoin(param);
				GameManager.Instance.ChangeState(Scene.HOME);
			}
			else
			{
				GameManager.Instance.ChangeState(Scene.SELECTLEVEL);
			}
			/*}
			GameManager.Instance.ChangeState(Scene.HOME);*/
		});
	}

	public void ClickContinues(Button btn)
	{
		btn.Click(delegate
		{
			this.moreLive.ShowPanel();
		});
	}

	public void ClickRestart(Button btn)
	{
		btn.Click(delegate
		{
			this.Hide();
			GameManager.Instance.loadManager.loadMap.ResetData();
			GameManager.Instance.loadManager.loadMap.Init();
			PlayerController.Instance.OnInit();
		});
	}
}
