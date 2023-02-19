using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using zZ17;

public class UpgradeManager : MonoBehaviour
{
	public Button btnUpgradePower;

	public Button btnUpgradeMax;

	public Text txtValueMax;

	public Text txtValuePower;

	public Text txtPriceMax;

	public Text txtPricePower;

	private int priceMax;

	private int pricePower;

	private float valueMax;

	private float valuePower;

	public Transform trnPerPower;

	public Transform trnPerMax;

	public Sprite[] spritePer;

	private void OnEnable()
	{
		this.LoadMax();
		this.LoadPower();
	}

	private void Start()
	{
		this.btnUpgradePower.onClick.AddListener(new UnityAction(this.ClickUpgradePower));
		this.btnUpgradeMax.onClick.AddListener(new UnityAction(this.ClickUpgradeMax));
	}

	private void LoadPower()
	{

		this.valuePower = GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")];
		this.pricePower = GameManager.Instance.config.dataGame.pricePower[PlayerPrefs.GetInt("power")];
		this.txtValuePower.text = "+" + this.valuePower;
		this.txtPricePower.text = string.Empty + this.pricePower;
		this.btnUpgradePower.interactable = this.CheckCoin(this.pricePower);

		if (PlayerPrefs.GetInt("power") >= GameManager.Instance.config.dataGame.valuePower.Length - 1)
		{
			this.btnUpgradePower.interactable = false;
			//	this.txtValuePower.text = string.Empty;
			this.txtPricePower.text = string.Empty;
			PlayerPrefs.SetInt("power", GameManager.Instance.config.dataGame.valuePower.Length - 1);
		//	return;
		}


		int num = PlayerPrefs.GetInt("power");// * 10 / GameManager.Instance.config.dataGame.valuePower.Length;
		int num2 = 0;
		IEnumerator enumerator = this.trnPerPower.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.GetComponent<Image>().sprite = this.spritePer[(num2 >= num) ? 0 : 1];
				num2++;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	private void LoadMax()
	{
		this.valueMax = GameManager.Instance.config.dataGame.valueMax[PlayerPrefs.GetInt("maxpower")];
		this.priceMax = GameManager.Instance.config.dataGame.priceMax[PlayerPrefs.GetInt("maxpower")];
		this.txtPriceMax.text = string.Empty + this.priceMax;
		this.txtValueMax.text = "+" + this.valueMax;
		this.btnUpgradeMax.interactable = this.CheckCoin(this.priceMax);

		if (PlayerPrefs.GetInt("maxpower") >= GameManager.Instance.config.dataGame.valueMax.Length - 1)
		{
			this.btnUpgradeMax.interactable = false;
		//	this.txtValueMax.text = string.Empty;
			this.txtPriceMax.text = string.Empty;
			PlayerPrefs.SetInt("maxpower", GameManager.Instance.config.dataGame.valueMax.Length - 1);
		//	return;
		}

		int num = PlayerPrefs.GetInt("maxpower");// * 10 / GameManager.Instance.config.dataGame.valueMax.Length;
		int num2 = 0;
		IEnumerator enumerator = this.trnPerMax.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.GetComponent<Image>().sprite = this.spritePer[(num2 >= num) ? 0 : 1];
				num2++;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	private bool CheckCoin(int value)
	{
		return GameManager.Instance.coin >= value;
	}

	public void ClickUpgradePower()
	{
		this.btnUpgradePower.Click(delegate
		{
			PlayerPrefs.SetInt("power", PlayerPrefs.GetInt("power") + 1);
			GameManager.Instance.ChangeCoin(-this.pricePower);
			this.LoadPower();
		});
	}

	public void ClickUpgradeMax()
	{
		this.btnUpgradeMax.Click(delegate
		{
			PlayerPrefs.SetInt("maxpower", PlayerPrefs.GetInt("maxpower") + 1);
			GameManager.Instance.ChangeCoin(-this.priceMax);
			this.LoadMax();
		});
	}
}
