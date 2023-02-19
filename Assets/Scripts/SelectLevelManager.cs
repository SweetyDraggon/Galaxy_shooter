using DG.Tweening;
using SelectLevel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class SelectLevelManager : MonoBehaviour
{
	private sealed class _Awake_c__AnonStorey0
	{
		internal Button btn;

		internal SelectLevelManager _this;

		internal void __m__0()
		{
			this._this.OnClickButton(this.btn);
		}
	}

	private sealed class _OnClickButton_c__AnonStorey1
	{
		internal Button btn;

		internal SelectLevelManager _this;

		internal void __m__0()
		{
			GameManager.Instance.level = int.Parse(this.btn.GetComponentInChildren<Text>().text);
			PlayerPrefs.SetInt("level", GameManager.Instance.level);
			this._this.panelShop.Show(1f);
			this.btn.transform.DOKill(false);
			this.btn.transform.localScale = Vector3.one;
		}
	}

	[SerializeField]
	private Text txtCoin;

	public PanelShop panelShop;

	public Button[] btnLevels;

	public Transform trnContent;

	public GameObject goEff;

	public Lines lines;

	private static TweenCallback __f__am_cache0;



	private void OnEnable()
	{
		this.PostEvent(EventID.Coin, 0);
		this.panelShop.Hide();
		this.lines.CheckLines();
		Button button = this.btnLevels[Mathf.Max(0, PlayerPrefs.GetInt("max") - 1)];
		this.goEff.transform.SetParent(button.transform);
		this.goEff.transform.localPosition = Vector3.zero;
		this.goEff.gameObject.SetActive(true);
		button.transform.DOScale(1.1f, 0.8f).SetLoops(-1, LoopType.Yoyo);
	}

	private void Awake()
	{
		if (!PlayerPrefs.HasKey("level"))
		{
			PlayerPrefs.SetInt("level", 0);
		}
		int @int = PlayerPrefs.GetInt("level");
		if (!PlayerPrefs.HasKey("max"))
		{
			PlayerPrefs.SetInt("max", 1);
		}
		int int2 = PlayerPrefs.GetInt("max");
		this.RegisterListener(EventID.Coin, delegate(object param)
		{
			this.ShowCoin(this.txtCoin);
		});
		for (int i = 0; i < this.btnLevels.Length; i++)
		{
			Button btn = this.btnLevels[i];
			if (i + 1 == int2)
			{
				this.goEff.transform.SetParent(btn.transform);
				this.goEff.transform.localPosition = Vector3.zero;
				this.goEff.gameObject.SetActive(true);
				btn.transform.DOScale(1.1f, 0.8f).SetLoops(-1, LoopType.Yoyo);
			}
			btn.interactable = (i <= int2 - 1);
			btn.onClick.AddListener(delegate
			{
				this.OnClickButton(btn);
			});
		}
	}

	public void SetNextLevel()
	{
		if (GameManager.Instance.level + 1 >= PlayerPrefs.GetInt("max"))
		{
			PlayerPrefs.SetInt("max", GameManager.Instance.level + 1);
		}
	}

	private void OnClickButton(Button btn)
	{
		btn.Click(delegate
		{
			GameManager.Instance.level = int.Parse(btn.GetComponentInChildren<Text>().text);
			PlayerPrefs.SetInt("level", GameManager.Instance.level);
			this.panelShop.Show(1f);
			btn.transform.DOKill(false);
			btn.transform.localScale = Vector3.one;
		});
	}

	private void OnValidate()
	{
		List<Button> list = new List<Button>();
		IEnumerator enumerator = this.trnContent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.GetComponent<Button>())
				{
					list.Add(transform.GetComponent<Button>());
				}
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
		this.btnLevels = list.ToArray();
	}
   /* public void Update()
    {
    *//*    if (Input.GetKeyDown(KeyCode.Escape) )
        {
            GameManager.Instance.ChangeState(Scene.HOME);
        }*//*



    }*/
    public void ClickBack(Button btn)
	{
		btn.Click(delegate
		{
			GameManager.Instance.ChangeState(Scene.HOME);
		});
	}
}
