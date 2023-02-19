using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UbhEmitter : UbhMonoBehaviour
{
	private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GameObject _wave___1;

		internal UbhEmitter _this;

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

		public _Start_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this._Waves.Length == 0)
				{
					return false;
				}
				this._this._Manager = UnityEngine.Object.FindObjectOfType<UbhManager>();
				break;
			case 1u:
				//IL_71:
				if (this._this._Manager.IsPlaying())
				{
					this._wave___1 = UnityEngine.Object.Instantiate<GameObject>(this._this._Waves[this._this._CurrentWave], this._this.transform.position, Quaternion.identity);
					this._wave___1.transform.parent = this._this.transform;
					goto IL_FD;
				}
				this._current = 0;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 2u:
				goto IL_FD;
			default:
				return false;
			}
			IL_4C:
			goto IL_71;
			IL_FD:
			if (0 >= this._wave___1.transform.childCount)
			{
				UnityEngine.Object.Destroy(this._wave___1);
				this._this._CurrentWave = (int)Mathf.Repeat((float)this._this._CurrentWave + 1f, (float)this._this._Waves.Length);
				goto IL_4C;
			}
			this._current = 0;
			if (!this._disposing)
			{
				this._PC = 2;
			}
			return true;

        IL_71:
            if (this._this._Manager.IsPlaying())
            {
                this._wave___1 = UnityEngine.Object.Instantiate<GameObject>(this._this._Waves[this._this._CurrentWave], this._this.transform.position, Quaternion.identity);
                this._wave___1.transform.parent = this._this.transform;
                goto IL_FD;
            }
            this._current = 0;
            if (!this._disposing)
            {
                this._PC = 1;
            }
            return true;
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

	[SerializeField]
	private GameObject[] _Waves;

	private int _CurrentWave;

	private UbhManager _Manager;

	private IEnumerator Start()
	{
		UbhEmitter._Start_c__Iterator0 _Start_c__Iterator = new UbhEmitter._Start_c__Iterator0();
		_Start_c__Iterator._this = this;
		return _Start_c__Iterator;
	}
}
