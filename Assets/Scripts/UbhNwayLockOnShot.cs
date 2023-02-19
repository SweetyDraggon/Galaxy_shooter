using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/nWay Shot (Lock On)")]
public class UbhNwayLockOnShot : UbhNwayShot
{
	private sealed class _AimingCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhNwayLockOnShot _this;

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

		public _AimingCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (!this._this._Aiming)
			{
				this._PC = -1;
			}
			else if (this._this._Shooting)
			{
				this._this.AimTarget();
				this._current = 0;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
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

	public bool _SetTargetFromTag = true;

	public string _TargetTagName = "Player";

	public Transform _TargetTransform;

	public bool _Aiming;

	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		if (this._Shooting)
		{
			return;
		}
		this.AimTarget();
		if (this._TargetTransform == null)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because TargetTransform is not set.");
			return;
		}
		base.Shot();
		if (this._Aiming)
		{
			base.StartCoroutine(this.AimingCoroutine());
		}
	}

	private void AimTarget()
	{
		if (this._TargetTransform == null && this._SetTargetFromTag)
		{
			this._TargetTransform = UbhUtil.GetTransformFromTagName(this._TargetTagName);
		}
		if (this._TargetTransform != null)
		{
			this._CenterAngle = UbhUtil.GetAngleFromTwoPosition(base.transform, this._TargetTransform, base.ShotCtrl._AxisMove);
		}
	}

	private IEnumerator AimingCoroutine()
	{
		UbhNwayLockOnShot._AimingCoroutine_c__Iterator0 _AimingCoroutine_c__Iterator = new UbhNwayLockOnShot._AimingCoroutine_c__Iterator0();
		_AimingCoroutine_c__Iterator._this = this;
		return _AimingCoroutine_c__Iterator;
	}
}
