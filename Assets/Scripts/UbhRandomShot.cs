using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Shot")]
public class UbhRandomShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<int> _numList___0;

		internal int _index___1;

		internal UbhBullet _bullet___1;

		internal float _bulletSpeed___1;

		internal float _minAngle___1;

		internal float _maxAngle___1;

		internal float _angle___1;

		internal float _waitTime___2;

		internal UbhRandomShot _this;

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
				this._numList___0 = new List<int>();
				for (int i = 0; i < this._this._BulletNum; i++)
				{
					this._numList___0.Add(i);
				}
				break;
			case 1u:
				break;
			default:
				return false;
			}
			while (0 < this._numList___0.Count)
			{
				this._index___1 = UnityEngine.Random.Range(0, this._numList___0.Count);
				this._bullet___1 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
				if (this._bullet___1 == null)
				{
					break;
				}
				this._bulletSpeed___1 = UnityEngine.Random.Range(this._this._RandomSpeedMin, this._this._RandomSpeedMax);
				this._minAngle___1 = this._this._RandomCenterAngle - this._this._RandomRangeSize / 2f;
				this._maxAngle___1 = this._this._RandomCenterAngle + this._this._RandomRangeSize / 2f;
				this._angle___1 = 0f;
				if (this._this._EvenlyDistribute)
				{
					float num2 = Mathf.Floor((float)this._this._BulletNum / 4f);
					float num3 = Mathf.Floor((float)this._numList___0[this._index___1] / num2);
					float num4 = Mathf.Abs(this._maxAngle___1 - this._minAngle___1) / 4f;
					this._angle___1 = UnityEngine.Random.Range(this._minAngle___1 + num4 * num3, this._minAngle___1 + num4 * (num3 + 1f));
				}
				else
				{
					this._angle___1 = UnityEngine.Random.Range(this._minAngle___1, this._maxAngle___1);
				}
				this._this.ShotBullet(this._bullet___1, this._bulletSpeed___1, this._angle___1, false, null, 0f, false, 0f, 0f);
				this._this.AutoReleaseBulletGameObject(this._bullet___1.gameObject);
				this._numList___0.RemoveAt(this._index___1);
				if (0 < this._numList___0.Count && 0f <= this._this._RandomDelayMin && 0f < this._this._RandomDelayMax)
				{
					this._waitTime___2 = UnityEngine.Random.Range(this._this._RandomDelayMin, this._this._RandomDelayMax);
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._waitTime___2));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
			}
			this._this.FinishedShot();
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

	[Range(0f, 360f)]
	public float _RandomCenterAngle = 180f;

	[Range(0f, 360f)]
	public float _RandomRangeSize = 360f;

	public float _RandomSpeedMin = 1f;

	public float _RandomSpeedMax = 3f;

	public float _RandomDelayMin = 0.01f;

	public float _RandomDelayMax = 0.1f;

	public bool _EvenlyDistribute = true;

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
		UbhRandomShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhRandomShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
