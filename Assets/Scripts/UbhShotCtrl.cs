using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Controller/Shot Controller")]
public class UbhShotCtrl : UbhMonoBehaviour
{
	[Serializable]
	public class ShotInfo
	{
		public UbhBaseShot _ShotObj;

		public float _AfterDelay;
	}

	private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhShotCtrl _this;

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
			UnityEngine.Debug.Log("Iterator Move Next");
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (!this._this._StartOnAwake)
				{
					goto IL_86;
				}
				if (0f < this._this._StartOnAwakeDelay)
				{
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._this._StartOnAwakeDelay));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				break;
			case 1u:
				break;
			default:
				return false;
			}
			this._this.StartShotRoutine();
			IL_86:
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

	private sealed class _ShotCoroutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal bool _enableShot___0;

		internal bool _enableDelay___0;

		internal List<UbhShotCtrl.ShotInfo> _tmpShotInfoList___0;

		internal int _nowIndex___0;

		internal UbhShotCtrl _this;

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

		public _ShotCoroutine_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			UnityEngine.Debug.Log("Coroutine Move Next");

			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("Case 0u");
				if (this._this._ShotList == null || this._this._ShotList.Count <= 0)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because ShotList is not set.");
					return false;
				}
				this._enableShot___0 = false;
				for (int i = 0; i < this._this._ShotList.Count; i++)
				{
					if (this._this._ShotList[i]._ShotObj != null)
					{
						this._enableShot___0 = true;
						UnityEngine.Debug.Log("break_1");
						break;
					}
				}
				this._enableDelay___0 = false;
				for (int j = 0; j < this._this._ShotList.Count; j++)
				{
					if (0f < this._this._ShotList[j]._AfterDelay)
					{
						this._enableDelay___0 = true;
						UnityEngine.Debug.Log("break_2");
						break;
					}
				}
				if (!this._enableShot___0 || !this._enableDelay___0)
				{
					if (!this._enableShot___0)
					{
						UnityEngine.Debug.LogWarning("Cannot shot because all ShotObj of ShotList is not set.");
					}
					if (!this._enableDelay___0)
					{
						UnityEngine.Debug.LogWarning("Cannot shot because all AfterDelay of ShotList is zero.");
					}
					UnityEngine.Debug.Log("return false 1");
					return false;
				}
				if (this._this._Shooting)
				{
					UnityEngine.Debug.Log("return false 2");
					return false;
				}
				this._this._Shooting = true;
				this._tmpShotInfoList___0 = new List<UbhShotCtrl.ShotInfo>(this._this._ShotList);
				this._nowIndex___0 = 0;
				break;
			case 1u:
				UnityEngine.Debug.Log("Case 1u");
				//IL_266:
				if (this._this._AtRandom)
				{
					this._tmpShotInfoList___0.RemoveAt(this._nowIndex___0);
					if (this._tmpShotInfoList___0.Count <= 0)
					{
						if (!this._this._Loop)
						{
							UnityEngine.Debug.Log("Goto IL_324");
							goto IL_324;
						}
						this._tmpShotInfoList___0 = new List<UbhShotCtrl.ShotInfo>(this._this._ShotList);
					}
				}
				else
				{
					if (!this._this._Loop && this._tmpShotInfoList___0.Count - 1 <= this._nowIndex___0)
					{
						UnityEngine.Debug.Log("Goto IL_324");
						goto IL_324;
					}
					this._nowIndex___0 = (int)Mathf.Repeat((float)this._nowIndex___0 + 1f, (float)this._tmpShotInfoList___0.Count);
				}
				UnityEngine.Debug.Log("break_3");
				break;
				//IL_324:
				this._this._Shooting = false;
				this._PC = -1;
				UnityEngine.Debug.Log("return false 3");
				return false;
			default:
				UnityEngine.Debug.Log("Case Default");
				return false;
			}
			if (this._this._AtRandom)
			{
				this._nowIndex___0 = UnityEngine.Random.Range(0, this._tmpShotInfoList___0.Count);
			}
			if (this._tmpShotInfoList___0[this._nowIndex___0]._ShotObj != null)
			{
				this._tmpShotInfoList___0[this._nowIndex___0]._ShotObj.SetShotCtrl(this._this);
				this._tmpShotInfoList___0[this._nowIndex___0]._ShotObj.Shot();
			}
			if (0f < this._tmpShotInfoList___0[this._nowIndex___0]._AfterDelay)
			{
				this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._tmpShotInfoList___0[this._nowIndex___0]._AfterDelay));
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			UnityEngine.Debug.Log("Goto IL_266");
			goto IL_266;

        IL_266:
            if (this._this._AtRandom)
            {
                this._tmpShotInfoList___0.RemoveAt(this._nowIndex___0);
                if (this._tmpShotInfoList___0.Count <= 0)
                {
                    if (!this._this._Loop)
                    {
						UnityEngine.Debug.Log("Goto IL_324");
						goto IL_324;
                    }
                    this._tmpShotInfoList___0 = new List<UbhShotCtrl.ShotInfo>(this._this._ShotList);
                }
            }
            else
            {
                if (!this._this._Loop && this._tmpShotInfoList___0.Count - 1 <= this._nowIndex___0)
                {
					UnityEngine.Debug.Log("Goto IL_324");
					goto IL_324;
                }
                this._nowIndex___0 = (int)Mathf.Repeat((float)this._nowIndex___0 + 1f, (float)this._tmpShotInfoList___0.Count);
            }
            
        IL_324:
            this._this._Shooting = false;
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

	public UbhUtil.AXIS _AxisMove;

	public bool _StartOnAwake = true;

	public float _StartOnAwakeDelay = 1f;

	public bool _Loop = true;

	public bool _AtRandom;

	public List<UbhShotCtrl.ShotInfo> _ShotList = new List<UbhShotCtrl.ShotInfo>();

	private bool _Shooting;

	private IEnumerator Start()
	{
		UbhShotCtrl._Start_c__Iterator0 _Start_c__Iterator = new UbhShotCtrl._Start_c__Iterator0();
		_Start_c__Iterator._this = this;
		return _Start_c__Iterator;
	}

	private void OnDisable()
	{
		this._Shooting = false;
	}

	public void StartShotRoutine()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhShotCtrl._ShotCoroutine_c__Iterator1 _ShotCoroutine_c__Iterator = new UbhShotCtrl._ShotCoroutine_c__Iterator1();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}

	public void StopShotRoutine()
	{
		base.StopAllCoroutines();
		this._Shooting = false;
	}
}
