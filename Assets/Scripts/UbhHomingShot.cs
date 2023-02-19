using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Homing Shot")]
public class UbhHomingShot : UbhBaseShot
{
	private sealed class _ShotCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal UbhBullet _bullet___2;

		internal float _angle___2;

		internal UbhHomingShot _this;

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
				if (this._this._BulletNum <= 0 || this._this._BulletSpeed <= 0f)
				{
					UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
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
				//IL_D9:
				this._bullet___2 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
				if (this._bullet___2 == null)
				{
					goto IL_211;
				}
				if (this._this._TargetTransform == null && this._this._SetTargetFromTag)
				{
					this._this._TargetTransform = UbhUtil.GetTransformFromTagName(this._this._TargetTagName);
				}
				this._angle___2 = UbhUtil.GetAngleFromTwoPosition(this._this.transform, this._this._TargetTransform, this._this.ShotCtrl._AxisMove);
				this._this.ShotBullet(this._bullet___2, this._this._BulletSpeed, this._angle___2, true, this._this._TargetTransform, this._this._HomingAngleSpeed, false, 0f, 0f);
				this._this.AutoReleaseBulletGameObject(this._bullet___2.gameObject);
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this._BulletNum)
			{
				if (0 < this._i___1 && 0f < this._this._BetweenDelay)
				{
					this._current = this._this.StartCoroutine(UbhUtil.WaitForSeconds(this._this._BetweenDelay));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				goto IL_D9;
			}
			IL_211:
			this._this.FinishedShot();
			this._PC = -1;
			return false;


        IL_D9:
            this._bullet___2 = this._this.GetBullet(this._this.transform.position, this._this.transform.rotation, false);
            if (this._bullet___2 == null)
            {
                goto IL_211;
            }
            if (this._this._TargetTransform == null && this._this._SetTargetFromTag)
            {
                this._this._TargetTransform = UbhUtil.GetTransformFromTagName(this._this._TargetTagName);
            }
            this._angle___2 = UbhUtil.GetAngleFromTwoPosition(this._this.transform, this._this._TargetTransform, this._this.ShotCtrl._AxisMove);
            this._this.ShotBullet(this._bullet___2, this._this._BulletSpeed, this._angle___2, true, this._this._TargetTransform, this._this._HomingAngleSpeed, false, 0f, 0f);
            this._this.AutoReleaseBulletGameObject(this._bullet___2.gameObject);
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

	public float _BetweenDelay = 0.1f;

	public float _HomingAngleSpeed = 20f;

	public bool _SetTargetFromTag = true;

	public string _TargetTagName = "Player";

	public Transform _TargetTransform;

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
		UbhHomingShot._ShotCoroutine_c__Iterator0 _ShotCoroutine_c__Iterator = new UbhHomingShot._ShotCoroutine_c__Iterator0();
		_ShotCoroutine_c__Iterator._this = this;
		return _ShotCoroutine_c__Iterator;
	}
}
