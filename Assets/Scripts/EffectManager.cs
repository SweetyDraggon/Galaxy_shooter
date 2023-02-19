using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class EffectManager : MonoBehaviour
{
	private sealed class _DisableEff_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GameObject eff;

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

		public _DisableEff_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this.eff.transform.HideObject();
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
	}

	internal static EffectManager Instance;

	private List<GameObject> listEff;

	private void Start()
	{
		EffectManager.Instance = this;
		this.listEff = new List<GameObject>();
	}

	internal void InitEff(GameObject prEff, Vector3 pos, Color color)
	{
	}

	private IEnumerator DisableEff(GameObject eff)
	{
		EffectManager._DisableEff_c__Iterator0 _DisableEff_c__Iterator = new EffectManager._DisableEff_c__Iterator0();
		_DisableEff_c__Iterator.eff = eff;
		return _DisableEff_c__Iterator;
	}

	private GameObject InstanceObject(GameObject prEff)
	{
		GameObject gameObject;
		for (int i = 0; i < this.listEff.Count; i++)
		{
			if (!this.listEff[i].activeSelf)
			{
				gameObject = this.listEff[i];
				gameObject.SetActive(true);
				return gameObject;
			}
		}
		gameObject = UnityEngine.Object.Instantiate<GameObject>(prEff, base.transform);
		gameObject.name = prEff.name;
		this.listEff.Add(gameObject);
		return gameObject;
	}
}
