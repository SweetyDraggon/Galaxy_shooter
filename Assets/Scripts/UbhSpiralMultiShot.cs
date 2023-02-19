using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spiral Multi Shot")]
public class UbhSpiralMultiShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _spiralWayShiftAngle___0;

		internal int _spiralWayIndex___0;

		internal int _i___1;

		internal UbhBullet _bullet___2;

		internal float _angle___2;

		internal UbhSpiralMultiShot _this;

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
				if (this._this._BulletNum <= 0 || this._this._BulletSpeed <= 0f || this._this._SpiralWayNum <= 0)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or SpiralWayNum is not set.");
					return false;
				}
				if (this._this._Shooting)
				{
					return false;
				}
				this._this._Shooting = true;
				this._spiralWayShiftAngle___0 = 360f / (float)this._this._SpiralWayNum;
				this._spiralWayIndex___0 = 0;
				this._i___1 = 0;
				break;
			case 1u:
				//IL_11A:
				this._bullet___2 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
				if (this._bullet___2 == null)
				{
					goto IL_223;
				}
				this._angle___2 = this._this._StartAngle + this._spiralWayShiftAngle___0 * (float)this._spiralWayIndex___0 + this._this._ShiftAngle * Mathf.Floor((float)(this._i___1 / this._this._SpiralWayNum));
				this._this.ShotBullet(this._bullet___2, this._this._BulletSpeed, this._angle___2, false, null, 0f, false, 0f, 0f);
				this._this.AutoReleaseBulletGameObject(this._bullet___2.gameObject);
				this._spiralWayIndex___0++;
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this._BulletNum)
			{
				if (this._this._SpiralWayNum > this._spiralWayIndex___0)
				{
					goto IL_11A;
				}
				this._spiralWayIndex___0 = 0;
				if (0f < this._this._BetweenDelay)
				{
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._this._BetweenDelay));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				goto IL_11A;
			}
			IL_223:
			this._this.FinishedShot();
			this._PC = -1;
			return false;

        IL_11A:
            this._bullet___2 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
            if (this._bullet___2 == null)
            {
                goto IL_223;
            }
            this._angle___2 = this._this._StartAngle + this._spiralWayShiftAngle___0 * (float)this._spiralWayIndex___0 + this._this._ShiftAngle * Mathf.Floor((float)(this._i___1 / this._this._SpiralWayNum));
            this._this.ShotBullet(this._bullet___2, this._this._BulletSpeed, this._angle___2, false, null, 0f, false, 0f, 0f);
            this._this.AutoReleaseBulletGameObject(this._bullet___2.gameObject);
            this._spiralWayIndex___0++;
            this._i___1++;
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

	public int _SpiralWayNum = 4;

	[Range(0f, 360f)]
	public float _StartAngle = 180f;

	[Range(-360f, 360f)]
	public float _ShiftAngle = 5f;

	public float _BetweenDelay = 0.2f;

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
		UbhSpiralMultiShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhSpiralMultiShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
