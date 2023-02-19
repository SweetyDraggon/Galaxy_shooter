using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class GamePlay : MonoBehaviour
{
	private sealed class _InstanceEff_c__AnonStorey2
	{
		internal GameObject go;

		internal void __m__0()
		{
			this.go.transform.HideObject();
		}
	}

	private sealed class _TimeShowSafe_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal Image img;
		internal Text txt;

		internal GamePlay _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _TimeShowSafe_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => !this._this.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._i___1 = 0;
				break;
			case 2u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 10)
			{
				this.img.enabled = (this._i___1 % 2 == 0);
				this.txt.enabled = (this._i___1 % 2 == 0);
				this._current = new WaitForSeconds(0.3f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			}
			this.img.enabled = false;
			this.txt.enabled = false;
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		internal bool __m__0()
		{
			return !this._this.loadingManager.gameObject.activeSelf;
		}
	}

	private sealed class _ShowInfo_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GamePlay _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _ShowInfo_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => !this._this.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitForSeconds(3f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.txtInfo.text = string.Empty;
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		internal bool __m__0()
		{
			return !this._this.loadingManager.gameObject.activeSelf;
		}
	}

	public GamePlayManager gamePlayManager;

	public Transform trnsSafe;

	public Transform trnsUnSafe;

	public Image imgSafe;

	public Image imgUnSafe;

	public Text textSafe;
	public Text textUnsafe;

	public LoadingManager loadingManager;

	public Transform trnThienThach;

	public GameObject goEffects;

	public Text txtInfo;

	internal static GamePlay Instance;

	private void Awake()
	{
		GamePlay.Instance = this;
		base.gameObject.SetActive(false);
		GameManager.Instance.gamePlayManager.ChangeState(StateGame.LOADING);
	}

	internal void InstanceEff(GameObject eff, Vector3 pos, Color color)
	{
		GameObject go = this.goEffects.transform.InstanceObject(eff);
		go.transform.position = pos;
		go.GetComponent<ParticleSystem>().startColor = new Color(color.r, color.g, color.b);
		GameManager.Instance.CallCoroutine(1f, delegate
		{
			go.transform.HideObject();
		});
	}

	internal void SetSafe(Vector3[] prPos)
	{
		int childCount = this.trnsSafe.childCount;
		for (int i = 0; i < prPos.Length; i++)
		{
			Image image;
			Text txt;
			if (i < childCount)
			{
				image = this.trnsSafe.GetChild(i).GetComponent<Image>();
				txt = this.textSafe.GetComponentInChildren<Text>();
			}
			else
			{
				image = UnityEngine.Object.Instantiate<Image>(this.imgSafe);
				image.transform.SetParent(this.trnsSafe);
				image.transform.localScale = Vector3.one;
				txt = image.GetComponentInChildren<Text>();
				//txt = UnityEngine.Object.Instantiate<Text>(this.textSafe);
				//txt.transform.SetParent(image.transform);
			}
			image.transform.position = prPos[i];
			image.enabled = true;
		//	txt.transform.position = prPos[i];
			txt.enabled = true;
			
			base.StartCoroutine(this.TimeShowSafe(image, txt));
		}
	}

	internal void SetUnSafe(Vector3[] prPos)
	{
		int childCount = this.trnsUnSafe.childCount;
		for (int i = 0; i < prPos.Length; i++)
		{
			Image image;
			Text txt;
			if (i < childCount)
			{
				image = this.trnsUnSafe.GetChild(i).GetComponent<Image>();
				txt = this.textUnsafe.GetComponentInChildren<Text>();
			}
			else
			{
				image = UnityEngine.Object.Instantiate<Image>(this.imgUnSafe);
				image.transform.SetParent(this.trnsUnSafe);
				image.transform.localScale = Vector3.one;
				txt = image.GetComponentInChildren<Text>();

				Destroy(image.gameObject, 10f);
				//UnityEngine.Object.Instantiate<Text>(this.textUnsafe);
				//txt.transform.SetParent(image.transform);
			}
			image.transform.position = prPos[i];
			image.enabled = true;
		//	txt.transform.position = prPos[i];
			txt.enabled = true;
			base.StartCoroutine(this.TimeShowSafe(image, txt));
		}
	}

	private IEnumerator TimeShowSafe(Image img, Text txt)
	{
		GamePlay._TimeShowSafe_c__Iterator0 _TimeShowSafe_c__Iterator = new GamePlay._TimeShowSafe_c__Iterator0();
		_TimeShowSafe_c__Iterator.img = img;
		_TimeShowSafe_c__Iterator.txt = txt;
		_TimeShowSafe_c__Iterator._this = this;
		return _TimeShowSafe_c__Iterator;
	}

	internal void SetInfoWave(string value, int wave)
	{
		this.txtInfo.enabled = !GameManager.Instance.isEndless;
		this.txtInfo.text = string.Concat(new object[]
		{
			Localisation.GetString("Wave")+ " ",
			wave,
			"/",
			LoadMap.countWave,
			":"
		}).ToUpper();
		base.StartCoroutine(this.ShowInfo());
	}

	private IEnumerator ShowInfo()
	{
		GamePlay._ShowInfo_c__Iterator1 _ShowInfo_c__Iterator = new GamePlay._ShowInfo_c__Iterator1();
		_ShowInfo_c__Iterator._this = this;
		return _ShowInfo_c__Iterator;
	}

	internal void DisableItemEnemy()
	{
		Item[] componentsInChildren = EnemyManager.Instance.transform.GetComponentsInChildren<Item>();
		if (this.loadingManager.loadMap.isWaveBoss() && UbhSingletonMonoBehavior<UbhObjectPool>.Instance != null)
		{
			for (int i = 0; i < UbhSingletonMonoBehavior<UbhObjectPool>.Instance.transform.childCount; i++)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.Instance.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		if (componentsInChildren.Length > 0)
		{
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				if (componentsInChildren[j].typeItem != TypeItem.BonusBullet && componentsInChildren[j].typeItem != TypeItem.DuiGa && componentsInChildren[j].typeItem != TypeItem.Gold && componentsInChildren[j].typeItem != TypeItem.Silver && componentsInChildren[j].typeItem != TypeItem.ThitGa1 && componentsInChildren[j].typeItem != TypeItem.ThitGa2)
				{
					componentsInChildren[j].gameObject.SetActive(false);
					componentsInChildren[j].transform.SetAsFirstSibling();
				}
			}
		}
	}
}
