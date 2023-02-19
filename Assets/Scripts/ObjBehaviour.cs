using EZ_Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjBehaviour : MonoBehaviour
{
	private sealed class _Decay_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal ObjBehaviour _this;

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

		public _Decay_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				EZ_PoolManager.Despawn(this._this.transform);
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

	private void OnSpawned()
	{
		base.StartCoroutine(this.Decay());
	}

	private IEnumerator Decay()
	{
		ObjBehaviour._Decay_c__Iterator0 _Decay_c__Iterator = new ObjBehaviour._Decay_c__Iterator0();
		_Decay_c__Iterator._this = this;
		return _Decay_c__Iterator;
	}
}
