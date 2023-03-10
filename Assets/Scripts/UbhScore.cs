using System;
using UnityEngine;

public class UbhScore : UbhMonoBehaviour
{
	private const string HIGH_SCORE_KEY = "highScoreKey";

	private const string HIGH_SCORE_TITLE = "HighScore : ";

	[SerializeField]
	private bool _DeleteScore;

	[SerializeField]
	private GUIText _ScoreGUIText;

	[SerializeField]
	private GUIText _HighScoreGUIText;

	private int _Score;

	private int _HighScore;

	private void Start()
	{
		this.Initialize();
	}

	private void Update()
	{
		if (this._HighScore < this._Score)
		{
			this._HighScore = this._Score;
		}
		this._ScoreGUIText.text = this._Score.ToString();
		this._HighScoreGUIText.text = "HighScore : " + this._HighScore.ToString();
	}

	public void Initialize()
	{
		if (this._DeleteScore)
		{
			PlayerPrefs.DeleteAll();
		}
		this._Score = 0;
		this._HighScore = PlayerPrefs.GetInt("highScoreKey", 0);
	}

	public void AddPoint(int point)
	{
		this._Score += point;
	}

	public void Save()
	{
		PlayerPrefs.SetInt("highScoreKey", this._HighScore);
		PlayerPrefs.Save();
		this.Initialize();
	}
}
