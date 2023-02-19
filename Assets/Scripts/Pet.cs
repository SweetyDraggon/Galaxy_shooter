using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pet : MonoBehaviour
{
	private sealed class _InitBullet_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal BulletPet _bullet___1;

		internal List<BulletPet>.Enumerator _locvar0;

		internal Pet _this;

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

		public _InitBullet_c__Iterator0()
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
				this._bullet___1 = null;
				this._locvar0 = this._this.lsBulletPet.GetEnumerator();
				try
				{
					while (this._locvar0.MoveNext())
					{
						BulletPet current = this._locvar0.Current;
						if (!current.gameObject.activeSelf)
						{
							this._bullet___1 = current;
							break;
						}
					}
				}
				finally
				{
					((IDisposable)this._locvar0).Dispose();
				}
				if (!this._bullet___1)
				{
					this._bullet___1 = UnityEngine.Object.Instantiate<BulletPet>(this._this.bulletPet, this._this.trnBulletPet);
					this._this.lsBulletPet.Add(this._bullet___1);
				}
				this._bullet___1.Init(this._this.transform.position);
				break;
			default:
				return false;
			}
			if (this._this.gameObject.activeSelf)
			{
				this._current = new WaitForSeconds(0.12f);
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

	public List<BulletPet> lsBulletPet = new List<BulletPet>();

	public float damage;

	public Transform trnBulletPet;

	public BulletPet bulletPet;

	private IEnumerator InitBullet()
	{
		Pet._InitBullet_c__Iterator0 _InitBullet_c__Iterator = new Pet._InitBullet_c__Iterator0();
		_InitBullet_c__Iterator._this = this;
		return _InitBullet_c__Iterator;
	}

	private void OnEnable()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.InitBullet());
	}

	private void OnDisable()
	{
		base.StopAllCoroutines();
	}
}
