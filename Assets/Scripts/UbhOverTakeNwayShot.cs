using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Over Take nWay Shot")]
public class UbhOverTakeNwayShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _wayIndex___0;

		internal float _bulletSpeed___0;

		internal float _shiftAngle___0;

		internal int _i___1;

		internal UbhBullet _bullet___2;

		internal float _baseAngle___2;

		internal float _angle___2;

		internal UbhOverTakeNwayShot _this;

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

		public _ShotCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this._BulletNum <= 0 || this._this._BulletSpeed <= 0f || this._this._WayNum <= 0)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
					return false;
				}
				if (this._this._Shooting)
				{
					return false;
				}
				this._this._Shooting = true;
				this._wayIndex___0 = 0;
				this._bulletSpeed___0 = this._this._BulletSpeed;
				this._shiftAngle___0 = 0f;
				this._i___1 = 0;
				goto IL_265;
			case 1u:
				//IL_11E:
				this._bulletSpeed___0 += this._this._DiffSpeed;
				this._shiftAngle___0 += this._this._ShiftAngle;
				break;
			default:
				return false;
			}
			IL_14E:
			this._bullet___2 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
			if (this._bullet___2 == null)
			{
				goto IL_27B;
			}
			this._baseAngle___2 = ((this._this._WayNum % 2 != 0) ? this._this._CenterAngle : (this._this._CenterAngle - this._this._BetweenAngle / 2f));
			this._angle___2 = UbhUtil.GetShiftedAngle(this._wayIndex___0, this._baseAngle___2, this._this._BetweenAngle) + this._shiftAngle___0;
			this._this.ShotBullet(this._bullet___2, this._bulletSpeed___0, this._angle___2, false, null, 0f, false, 0f, 0f);
			this._this.AutoReleaseBulletGameObject(this._bullet___2.gameObject);
			this._wayIndex___0++;
			this._i___1++;
			IL_265:
			if (this._i___1 < this._this._BulletNum)
			{
				if (this._this._WayNum > this._wayIndex___0)
				{
					goto IL_14E;
				}
				this._wayIndex___0 = 0;
				if (0f < this._this._NextLineDelay)
				{
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._this._NextLineDelay));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				goto IL_11E;
			}
			IL_27B:
			this._this.FinishedShot();
			this._PC = -1;
			return false;

        IL_11E:
            this._bulletSpeed___0 += this._this._DiffSpeed;
            this._shiftAngle___0 += this._this._ShiftAngle;
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

	public int _WayNum = 8;

	[Range(0f, 360f)]
	public float _CenterAngle = 180f;

	[Range(0f, 360f)]
	public float _BetweenAngle = 10f;

	public float _DiffSpeed = 0.5f;

	[Range(-360f, 360f)]
	public float _ShiftAngle;

	public float _NextLineDelay = 0.1f;

	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		UbhOverTakeNwayShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhOverTakeNwayShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
