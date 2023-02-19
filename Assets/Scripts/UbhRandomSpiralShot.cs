using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Spiral Shot")]
public class UbhRandomSpiralShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal float _waitTime___2;

		internal UbhBullet _bullet___3;

		internal float _bulletSpeed___3;

		internal float _centerAngle___3;

		internal float _minAngle___3;

		internal float _maxAngle___3;

		internal float _angle___3;

		internal UbhRandomSpiralShot _this;

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
				if (this._this._BulletNum <= 0 || this._this._RandomSpeedMin <= 0f || this._this._RandomSpeedMax <= 0f)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or RandomSpeedMin or RandomSpeedMax is not set.");
					return false;
				}
				if (this._this._Shooting)
				{
					return false;
				}
				this._this._Shooting = true;
				this._i___1 = 0;
				break;
			case 1u:
				//IL_11F:
				this._bullet___3 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
				if (this._bullet___3 == null)
				{
					goto IL_269;
				}
				this._bulletSpeed___3 = UnityEngine.Random.Range(this._this._RandomSpeedMin, this._this._RandomSpeedMax);
				this._centerAngle___3 = this._this._StartAngle + this._this._ShiftAngle * (float)this._i___1;
				this._minAngle___3 = this._centerAngle___3 - this._this._RandomRangeSize / 2f;
				this._maxAngle___3 = this._centerAngle___3 + this._this._RandomRangeSize / 2f;
				this._angle___3 = UnityEngine.Random.Range(this._minAngle___3, this._maxAngle___3);
				this._this.ShotBullet(this._bullet___3, this._bulletSpeed___3, this._angle___3, false, null, 0f, false, 0f, 0f);
				this._this.AutoReleaseBulletGameObject(this._bullet___3.gameObject);
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this._BulletNum)
			{
				if (0 < this._i___1 && 0f <= this._this._RandomDelayMin && 0f < this._this._RandomDelayMax)
				{
					this._waitTime___2 = UnityEngine.Random.Range(this._this._RandomDelayMin, this._this._RandomDelayMax);
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._waitTime___2));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				goto IL_11F;
			}
			IL_269:
			this._this.FinishedShot();
			this._PC = -1;
			return false;

        IL_11F:
            this._bullet___3 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
            if (this._bullet___3 == null)
            {
                goto IL_269;
            }
            this._bulletSpeed___3 = UnityEngine.Random.Range(this._this._RandomSpeedMin, this._this._RandomSpeedMax);
            this._centerAngle___3 = this._this._StartAngle + this._this._ShiftAngle * (float)this._i___1;
            this._minAngle___3 = this._centerAngle___3 - this._this._RandomRangeSize / 2f;
            this._maxAngle___3 = this._centerAngle___3 + this._this._RandomRangeSize / 2f;
            this._angle___3 = UnityEngine.Random.Range(this._minAngle___3, this._maxAngle___3);
            this._this.ShotBullet(this._bullet___3, this._bulletSpeed___3, this._angle___3, false, null, 0f, false, 0f, 0f);
            this._this.AutoReleaseBulletGameObject(this._bullet___3.gameObject);
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

	[Range(0f, 360f)]
	public float _StartAngle = 180f;

	[Range(-360f, 360f)]
	public float _ShiftAngle = 5f;

	[Range(0f, 360f)]
	public float _RandomRangeSize = 30f;

	public float _RandomSpeedMin = 1f;

	public float _RandomSpeedMax = 3f;

	public float _RandomDelayMin = 0.01f;

	public float _RandomDelayMax = 0.1f;

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
		UbhRandomSpiralShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhRandomSpiralShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
