using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spread nWay Shot")]
public class UbhSpreadNwayShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhSpreadNwayShot _this;

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
			bool arg_0D_0 = this._PC != 0;
			this._PC = -1;
			if (!arg_0D_0)
			{
				if (this._this._BulletNum <= 0 || this._this._BulletSpeed <= 0f || this._this._WayNum <= 0)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
				}
				else if (!this._this._Shooting)
				{
					this._this._Shooting = true;
					int num = 0;
					float num2 = this._this._BulletSpeed;
					for (int i = 0; i < this._this._BulletNum; i++)
					{
						if (this._this._WayNum <= num)
						{
							num = 0;
							for (num2 -= this._this._DiffSpeed; num2 <= 0f; num2 += Mathf.Abs(this._this._DiffSpeed))
							{
							}
						}
						UbhBullet bullet = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
						if (bullet == null)
						{
							break;
						}
						float baseAngle = (this._this._WayNum % 2 != 0) ? this._this._CenterAngle : (this._this._CenterAngle - this._this._BetweenAngle / 2f);
						float shiftedAngle = UbhUtil.GetShiftedAngle(num, baseAngle, this._this._BetweenAngle);
						this._this.ShotBullet(bullet, num2, shiftedAngle, false, null, 0f, false, 0f, 0f);
						this._this.AutoReleaseBulletGameObject(bullet.gameObject);
						num++;
					}
					this._this.FinishedShot();
				}
			}
			return false;
		}

		public void Dispose()
		{
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
		UbhSpreadNwayShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhSpreadNwayShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
