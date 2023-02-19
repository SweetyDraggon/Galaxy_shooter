using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
	private sealed class _RunProgress_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string[] _SPLIT_VAL___0;

		internal string _strTips___0;

		internal string[] _lines___0;

		internal float _dy___0;

		internal LoadingManager _this;

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

		public _RunProgress_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.value = 0f;
				this._SPLIT_VAL___0 = new string[]
				{
					"\n",
					"\r",
					"\r\n"
				};
				this._strTips___0 = Resources.Load<TextAsset>("Tut").text.Trim();
				this._lines___0 = this._strTips___0.Split(this._SPLIT_VAL___0, StringSplitOptions.RemoveEmptyEntries);
					this._this.txtTips.text = Localisation.GetString("Tips:") + " " + Localisation.GetString("tip_" + UnityEngine.Random.Range(0, this._lines___0.Length));//.Trim().ToUpper();
				//this._this.txtTips.text = Localisation.GetString("Tips:") + Localisation.GetString(this._lines___0[UnityEngine.Random.Range(0, this._lines___0.Length)].Trim().ToUpper());
				/*foreach (var tip in _lines___0)
				{
					UnityEngine.Debug.Log(tip);
				}*/
				GamePlayManager.state = StateGame.LOADING;
				this._dy___0 = this._this.imgProgress.sizeDelta.y;
				break;
			case 1u:
				this._this.imgProgress.sizeDelta = new Vector2(this._this.value * 3, this._dy___0);
				break;
			default:
				return false;
			}
			if (this._this.value < this._this.maxDx)
			{
				this._this.value += 2f;
				if (this._this.value > this._this.maxDx)
				{
					this._this.value = this._this.maxDx;
				}
				this._current = new WaitForSeconds(Time.deltaTime / this._this.speed);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._this.gameObject.SetActive(false);
			PlayerController.isDie = false;
			this._this.loadMap.Init();
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
	}

	public LoadMap loadMap;

	public float maxDx = 520f;

	public float value;

	public RectTransform imgProgress;

	private float speed = 5f;

	public Text txtTips;

	public Transform trnThienThach;

	public void Init()
	{
		base.gameObject.SetActive(true);
		Time.timeScale = 1f;
		base.StartCoroutine(this.RunProgress());
	}

	private IEnumerator RunProgress()
	{
		LoadingManager._RunProgress_c__Iterator0 _RunProgress_c__Iterator = new LoadingManager._RunProgress_c__Iterator0();
		_RunProgress_c__Iterator._this = this;
		return _RunProgress_c__Iterator;
	}
}
