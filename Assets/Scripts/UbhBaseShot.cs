using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class UbhBaseShot : UbhMonoBehaviour
{
	private sealed class _AutoReleaseBulletGameObjectCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _countUpTime___0;

		internal GameObject goBullet;

		internal UbhBaseShot _this;

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

		public _AutoReleaseBulletGameObjectCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._countUpTime___0 = 0f;
				break;
			case 1u:
				this._countUpTime___0 += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				break;
			default:
				return false;
			}
			if (!(this.goBullet == null) && this.goBullet.activeInHierarchy)
			{
				if (this._this._AutoReleaseTime > this._countUpTime___0)
				{
					this._current = 0;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				UbhSingletonMonoBehavior<UbhObjectPool>.Instance.ReleaseGameObject(this.goBullet, false);
				this._PC = -1;
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

	public GameObject _BulletPrefab;

	public int _BulletNum = 10;

	public float _BulletSpeed = 2f;

	public float _AccelerationSpeed;

	public float _AccelerationTurn;

	public bool _UsePauseAndResume;

	public float _PauseTime;

	public float _ResumeTime;

	public bool _InitialPooling;

	public bool _UseAutoRelease;

	public float _AutoReleaseTime = 10f;

	public GameObject _CallbackReceiver;

	public string _CallbackMethod;

	private UbhShotCtrl _ShotCtrl;

	protected bool _Shooting;

	protected UbhShotCtrl ShotCtrl
	{
		get
		{
			if (this._ShotCtrl == null)
			{
				this._ShotCtrl = base.transform.GetComponentInParent<UbhShotCtrl>();
			}
			return this._ShotCtrl;
		}
	}

	protected virtual void Awake()
	{
		if (this._InitialPooling)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this._BulletNum; i++)
			{
				UbhBullet bullet = this.GetBullet(Vector3.zero, Quaternion.identity, true);
				if (bullet != null)
				{
					list.Add(bullet.gameObject);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.Instance.ReleaseGameObject(list[j], false);
			}
		}
	}

	protected virtual void OnDisable()
	{
		this._Shooting = false;
	}

	public abstract void Shot();

	public void SetShotCtrl(UbhShotCtrl shotCtrl)
	{
		this._ShotCtrl = shotCtrl;
	}

	protected void FinishedShot()
	{
		if (this._CallbackReceiver != null && !string.IsNullOrEmpty(this._CallbackMethod))
		{
			this._CallbackReceiver.SendMessage(this._CallbackMethod, SendMessageOptions.DontRequireReceiver);
		}
		this._Shooting = false;
	}

	protected UbhBullet GetBullet(Vector3 position, Quaternion rotation, bool forceInstantiate = false)
	{
		if (this._BulletPrefab == null)
		{
			UnityEngine.Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
			return null;
		}
		GameObject gameObject = UbhSingletonMonoBehavior<UbhObjectPool>.Instance.GetGameObject(this._BulletPrefab, position, rotation, forceInstantiate);
		if (gameObject == null)
		{
			return null;
		}
		UbhBullet ubhBullet = gameObject.GetComponent<UbhBullet>();
		if (ubhBullet == null)
		{
			ubhBullet = gameObject.AddComponent<UbhBullet>();
		}
		return ubhBullet;
	}

	protected void ShotBullet(UbhBullet bullet, float speed, float angle, bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f, bool wave = false, float waveSpeed = 0f, float waveRangeSize = 0f)
	{
		if (bullet == null)
		{
			return;
		}
		bullet.Shot(speed, angle, this._AccelerationSpeed, this._AccelerationTurn, homing, homingTarget, homingAngleSpeed, wave, waveSpeed, waveRangeSize, this._UsePauseAndResume, this._PauseTime, this._ResumeTime, (!(this.ShotCtrl != null)) ? UbhUtil.AXIS.X_AND_Y : this.ShotCtrl._AxisMove);
	}

	protected void AutoReleaseBulletGameObject(GameObject goBullet)
	{
		if (!this._UseAutoRelease || this._AutoReleaseTime < 0f)
		{
			return;
		}
		UbhCoroutine.StartIE(this.AutoReleaseBulletGameObjectCoroutine(goBullet));
	}

	private IEnumerator AutoReleaseBulletGameObjectCoroutine(GameObject goBullet)
	{
		UbhBaseShot._AutoReleaseBulletGameObjectCoroutine_c__Iterator0 _AutoReleaseBulletGameObjectCoroutine_c__Iterator = new UbhBaseShot._AutoReleaseBulletGameObjectCoroutine_c__Iterator0();
		_AutoReleaseBulletGameObjectCoroutine_c__Iterator.goBullet = goBullet;
		_AutoReleaseBulletGameObjectCoroutine_c__Iterator._this = this;
		return _AutoReleaseBulletGameObjectCoroutine_c__Iterator;
	}
}
