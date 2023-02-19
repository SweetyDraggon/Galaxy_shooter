using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class ItemShop : MonoBehaviour
{
	private sealed class _Awake_c__AnonStorey0
	{
		internal Button btn;

		internal ItemShop _this;

		internal void __m__0()
		{
			this.btn.Click(delegate
			{
				GameManager.Instance.selectLevelManager.panelShop.SetSelect(this._this);
			});
		}

		internal void __m__1()
		{
			GameManager.Instance.selectLevelManager.panelShop.SetSelect(this._this);
		}
	}

	public Text txtCount;

	public Text txtCoin;

	public Image imgEff;

	public Image img;

	public TypeBonus type;

	public int price;

	internal Tweener bullet;

	internal Tweener eff;

	private void Awake()
	{
		Button btn = base.GetComponent<Button>();
		btn.onClick.AddListener(delegate
		{
			btn.Click(delegate
			{
				GameManager.Instance.selectLevelManager.panelShop.SetSelect(this);
			});
		});
		this.txtCoin.text = this.price + string.Empty;
		this.txtCount.text = "x" + PlayerPrefs.GetInt(this.type.ToString());
		this.bullet = this.img.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
		this.eff = this.imgEff.transform.DORotate(new Vector3(0f, 0f, 180f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
		this.SetPause();
	}

	private void OnEnable()
	{
		this.txtCount.text = "x" + PlayerPrefs.GetInt(this.type.ToString());
	}

	private void OnValidate()
	{
		this.txtCount = base.transform.Find("txtCount").GetComponent<Text>();
		this.txtCoin = base.transform.Find("txtCoin").GetComponent<Text>();
		this.img = base.transform.Find("img").GetComponent<Image>();
		this.imgEff = base.transform.Find("imgEff").GetComponent<Image>();
	}

	internal void SetPause()
	{
		this.bullet.Pause<Tweener>();
		this.eff.Pause<Tweener>();
		this.img.transform.localScale = Vector3.one;
		this.imgEff.transform.localRotation = Quaternion.identity;
	}

	internal void SetPlay()
	{
		this.bullet.Play<Tweener>();
		this.eff.Play<Tweener>();
	}

	internal void SaveValue()
	{
		PlayerPrefs.SetInt(this.type.ToString(), PlayerPrefs.GetInt(this.type.ToString()));
	}
}
