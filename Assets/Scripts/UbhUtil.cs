using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class UbhUtil
{
	public enum AXIS
	{
		X_AND_Y,
		X_AND_Z
	}

	private sealed class _WaitForSeconds_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _elapsedTime___0;

		internal float waitTime;

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

		public _WaitForSeconds_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._elapsedTime___0 = 0f;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._elapsedTime___0 < this.waitTime)
			{
				this._elapsedTime___0 += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				this._current = 0;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
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

	public static bool IsMobilePlatform()
	{
		return true;
	}

	public static IEnumerator WaitForSeconds(float waitTime)
	{
		UbhUtil._WaitForSeconds_c__Iterator0 _WaitForSeconds_c__Iterator = new UbhUtil._WaitForSeconds_c__Iterator0();
		_WaitForSeconds_c__Iterator.waitTime = waitTime;
		return _WaitForSeconds_c__Iterator;
	}

	public static Transform GetTransformFromTagName(string tagName)
	{
		if (string.IsNullOrEmpty(tagName))
		{
			return null;
		}
		GameObject gameObject = GameObject.FindWithTag(tagName);
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.transform;
	}

	public static float GetShiftedAngle(int wayIndex, float baseAngle, float betweenAngle)
	{
		return (wayIndex % 2 != 0) ? (baseAngle + betweenAngle * Mathf.Ceil((float)wayIndex / 2f)) : (baseAngle - betweenAngle * (float)wayIndex / 2f);
	}

	public static float Get360Angle(float angle)
	{
		while (angle < 0f)
		{
			angle += 360f;
		}
		while (360f < angle)
		{
			angle -= 360f;
		}
		return angle;
	}

	public static float GetAngleFromTwoPosition(Transform fromTrans, Transform toTrans, UbhUtil.AXIS axisMove)
	{
		if (axisMove == UbhUtil.AXIS.X_AND_Y)
		{
			return UbhUtil.GetZangleFromTwoPosition(fromTrans, toTrans);
		}
		if (axisMove != UbhUtil.AXIS.X_AND_Z)
		{
			return 0f;
		}
		return UbhUtil.GetYangleFromTwoPosition(fromTrans, toTrans);
	}

	private static float GetZangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float y = toTrans.position.x - fromTrans.position.x;
		float x = toTrans.position.y - fromTrans.position.y;
		float angle = Mathf.Atan2(y, x) * 57.29578f;
		return -UbhUtil.Get360Angle(angle);
	}

	private static float GetYangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float y = toTrans.position.x - fromTrans.position.x;
		float x = toTrans.position.z - fromTrans.position.z;
		float angle = Mathf.Atan2(y, x) * 57.29578f;
		return -UbhUtil.Get360Angle(angle);
	}
}
