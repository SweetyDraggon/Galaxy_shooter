using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UbhBullet : UbhMonoBehaviour
{
	private sealed class _MoveCoroutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhUtil.AXIS axisMove;

		internal float angle;

		internal float _selfFrameCnt___0;

		internal float _selfTimeCount___0;

		internal bool homing;

		internal Transform homingTarget;

		internal float homingAngleSpeed;

		internal bool wave;

		internal float accelTurn;

		internal float waveSpeed;

		internal float waveRangeSize;

		internal float accelSpeed;

		internal float speed;

		internal bool pauseAndResume;

		internal float pauseTime;

		internal float resumeTime;

		internal UbhBullet _this;

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

		public _MoveCoroutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this.axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					this._this.transform.SetEulerAnglesY(-this.angle);
				}
				else
				{
					this._this.transform.SetEulerAnglesZ(this.angle);
				}
				this._selfFrameCnt___0 = 0f;
				this._selfTimeCount___0 = 0f;
				break;
			case 1u:
				this._selfTimeCount___0 += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				if (this.pauseAndResume && this.pauseTime >= 0f && this.resumeTime > this.pauseTime)
				{
					goto IL_3D9;
				}
				break;
			case 2u:
				this._selfTimeCount___0 += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				goto IL_3D9;
			default:
				return false;
			}
			IL_79:
			if (this.homing)
			{
				if (this.homingTarget != null && 0f < this.homingAngleSpeed)
				{
					float angleFromTwoPosition = UbhUtil.GetAngleFromTwoPosition(this._this.transform, this.homingTarget, this.axisMove);
					float current;
					if (this.axisMove == UbhUtil.AXIS.X_AND_Z)
					{
						current = -this._this.transform.eulerAngles.y;
					}
					else
					{
						current = this._this.transform.eulerAngles.z;
					}
					float num2 = Mathf.MoveTowardsAngle(current, angleFromTwoPosition, UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime * this.homingAngleSpeed);
					if (this.axisMove == UbhUtil.AXIS.X_AND_Z)
					{
						this._this.transform.SetEulerAnglesY(-num2);
					}
					else
					{
						this._this.transform.SetEulerAnglesZ(num2);
					}
				}
			}
			else if (this.wave)
			{
				this.angle += this.accelTurn * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				if (0f < this.waveSpeed && 0f < this.waveRangeSize)
				{
					float num3 = this.angle + this.waveRangeSize / 2f * Mathf.Sin(this._selfFrameCnt___0 * this.waveSpeed / 100f);
					if (this.axisMove == UbhUtil.AXIS.X_AND_Z)
					{
						this._this.transform.SetEulerAnglesY(-num3);
					}
					else
					{
						this._this.transform.SetEulerAnglesZ(num3);
					}
				}
				this._selfFrameCnt___0 += 1f;
			}
			else
			{
				float num4 = this.accelTurn * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				if (this.axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					this._this.transform.AddEulerAnglesY(-num4);
				}
				else
				{
					this._this.transform.AddEulerAnglesZ(num4);
				}
			}
			this.speed += this.accelSpeed * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			if (this.axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				this._this.transform.position += this._this.transform.forward.normalized * this.speed * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			}
			else
			{
				this._this.transform.position += this._this.transform.up.normalized * this.speed * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			}
			this._current = 0;
			if (!this._disposing)
			{
				this._PC = 1;
			}
			return true;
			IL_3D9:
			if (this.pauseTime <= this._selfTimeCount___0 && this._selfTimeCount___0 < this.resumeTime)
			{
				this._current = 0;
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			}
			goto IL_79;
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

	private bool __Shooting_k__BackingField;

	public bool _Shooting
	{
		get;
		private set;
	}

	private void OnDisable()
	{
		base.StopAllCoroutines();
		base.transform.ResetPosition(false);
		base.transform.ResetRotation(false);
		this._Shooting = false;
	}

	public void Shot(float speed, float angle, float accelSpeed, float accelTurn, bool homing, Transform homingTarget, float homingAngleSpeed, bool wave, float waveSpeed, float waveRangeSize, bool pauseAndResume, float pauseTime, float resumeTime, UbhUtil.AXIS axisMove)
	{
		if (this._Shooting)
		{
			return;
		}
		this._Shooting = true;
		base.StartCoroutine(this.MoveCoroutine(speed, angle, accelSpeed, accelTurn, homing, homingTarget, homingAngleSpeed, wave, waveSpeed, waveRangeSize, pauseAndResume, pauseTime, resumeTime, axisMove));
	}

	private IEnumerator MoveCoroutine(float speed, float angle, float accelSpeed, float accelTurn, bool homing, Transform homingTarget, float homingAngleSpeed, bool wave, float waveSpeed, float waveRangeSize, bool pauseAndResume, float pauseTime, float resumeTime, UbhUtil.AXIS axisMove)
	{
		UbhBullet._MoveCoroutine_c__Iterator0 _MoveCoroutine_c__Iterator = new UbhBullet._MoveCoroutine_c__Iterator0();
		_MoveCoroutine_c__Iterator.axisMove = axisMove;
		_MoveCoroutine_c__Iterator.angle = angle;
		_MoveCoroutine_c__Iterator.homing = homing;
		_MoveCoroutine_c__Iterator.homingTarget = homingTarget;
		_MoveCoroutine_c__Iterator.homingAngleSpeed = homingAngleSpeed;
		_MoveCoroutine_c__Iterator.wave = wave;
		_MoveCoroutine_c__Iterator.accelTurn = accelTurn;
		_MoveCoroutine_c__Iterator.waveSpeed = waveSpeed;
		_MoveCoroutine_c__Iterator.waveRangeSize = waveRangeSize;
		_MoveCoroutine_c__Iterator.accelSpeed = accelSpeed;
		_MoveCoroutine_c__Iterator.speed = speed;
		_MoveCoroutine_c__Iterator.pauseAndResume = pauseAndResume;
		_MoveCoroutine_c__Iterator.pauseTime = pauseTime;
		_MoveCoroutine_c__Iterator.resumeTime = resumeTime;
		_MoveCoroutine_c__Iterator._this = this;
		return _MoveCoroutine_c__Iterator;
	}
}
