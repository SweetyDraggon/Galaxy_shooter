using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpace : Enemy
{
	private sealed class _Attack2_c__AnonStorey4
	{
		internal Item cls;

		internal void __m__0()
		{
			this.cls.gameObject.SetActive(false);
		}
	}

	private sealed class _SetHealth_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal EnemySpace _this;

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

		public _SetHealth_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => this._this.Health > 0f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.FullHealth = this._this.Health;
				this._current = new WaitUntil(() => this._this.rigid2d.transform.position.y < -10f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.gameObject.SetActive(false);
				this._PC = -1;
				break;
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

		internal bool __m__0()
		{
			return this._this.Health > 0f;
		}

		internal bool __m__1()
		{
			return this._this.rigid2d.transform.position.y < -10f;
		}
	}

	private sealed class _EnemyMove_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal EnemySpace _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

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

		public _EnemyMove_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => !GamePlay.Instance.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.transform.DOLocalMove(this._this.posInit, 2f, false).OnComplete(delegate
				{
					this._this.InvokeRepeating("Attack2", 0f, UnityEngine.Random.Range(2f, 4f));
				});
				this._PC = -1;
				break;
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

		private static bool __m__0()
		{
			return !GamePlay.Instance.loadingManager.gameObject.activeSelf;
		}

		internal void __m__1()
		{
			this._this.InvokeRepeating("Attack2", 0f, UnityEngine.Random.Range(2f, 4f));
		}
	}

	private sealed class _DisableSpace_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal EnemySpace _this;

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

		public _DisableSpace_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => this._this.rigid2d.transform.position.y < -8f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.isDie = true;
				this._this.gameObject.SetActive(false);
				this._PC = -1;
				break;
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

		internal bool __m__0()
		{
			return this._this.rigid2d.transform.position.y < -8f;
		}
	}

	private sealed class _DisableSpace2_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal EnemySpace _this;

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

		public _DisableSpace2_c__Iterator3()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.Eff.transform.position = this._this.rigid2d.transform.position;
				this._this.Eff.gameObject.SetActive(true);
				this._this.rigid2d.gameObject.SetActive(false);
				this._this.Eff.Play();
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Eff.gameObject.SetActive(false);
				this._this.gameObject.SetActive(false);
				this._PC = -1;
				break;
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

	public float Health2 = 10f;

	public float FullHealth = 10f;

	public bool isInit;

	public Rigidbody2D rigid2d;

	public GameObject goBullet;

	internal void InitEnemy(InfoEnemy prInfo)
	{
		base.StopAllCoroutines();
		this.Health = (this.FullHealth = (float)prInfo.Health);
		this.Health2 = this.Health * 2f;
		this.isDie = false;
		base.transform.localScale = Vector3.one;
		base.gameObject.SetActive(true);
		this.posInit = LoadMap.GridToVector((float)prInfo.dx, (float)prInfo.dy, LoadMap.distance);
		base.transform.localPosition = new Vector3((float)(prInfo.Arrow * 5), this.posInit.y);
		this.rigid2d.transform.localPosition = Vector3.zero;
		this.rigid2d.freezeRotation = true;
		this.rigid2d.gameObject.SetActive(true);
		this.rigid2d.velocity = Vector2.zero;
		this.rigid2d.gravityScale = 0f;
		this.isInit = true;
		base.StartCoroutine(this.EnemyMove());
		this.Attack2();
	}

	protected virtual void OnEnable()
	{
		this.isDie = (this.isInit = false);
		base.StartCoroutine(this.SetHealth());
	}

	protected virtual void Attack()
	{
	}

	internal void Attack2()
	{
		if (this.isDie || this.Health <= 0f || !base.gameObject.activeSelf)
		{
			this.isDie = true;
			base.CancelInvoke("Attack2");
			return;
		}
		if (!this.isInit || base.transform.localScale == Vector3.zero)
		{
			return;
		}
		Item cls = EnemyManager.Instance.InitBullet(this.goBullet);
		cls.transform.position = base.transform.position;
		Vector2 vector = PlayerController.Instance.transform.position;
		cls.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(base.transform.position.y - vector.y, base.transform.position.x - vector.x) * 57.29578f - 90f);
		cls.transform.DOMove(PlayerController.Instance.transform.position.normalized * 20f, 8f, false).OnComplete(delegate
		{
			cls.gameObject.SetActive(false);
		});
	}

	private IEnumerator SetHealth()
	{
		EnemySpace._SetHealth_c__Iterator0 _SetHealth_c__Iterator = new EnemySpace._SetHealth_c__Iterator0();
		_SetHealth_c__Iterator._this = this;
		return _SetHealth_c__Iterator;
	}

	protected virtual void OnHit(float prDame)
	{
		if (!EnemyManager.isEnableOnHit(base.transform.position))
		{
			return;
		}
		if (!this.isInit)
		{
			return;
		}
		if (!this.isDie)
		{
			this.Health -= prDame;
			if (this.Health <= 0f)
			{
				SoundManager.Instance.PlaySoundEnemy(3);
				this.isDie = true;
				this.Health = 0f;
				this.DropDown();
				base.StartCoroutine(this.DisableSpace());
			}
		}
		else
		{
			this.Health2 -= prDame;
			if (this.Health2 <= 0f)
			{
				this.Health2 = 0f;
				this.isInit = false;
				base.StartCoroutine(this.DisableSpace2());
			}
		}
	}

	private void DropDown()
	{
		Enemy enemy = EnemyManager.Instance.InitEnemy();
		enemy.transform.position = base.transform.position;
		enemy.posInit = this.posInit;
		enemy.SendMessage("SetBonus", this.idBonus, SendMessageOptions.DontRequireReceiver);
		GameManager.Instance.gamePlayManager.SetScore(5);
		DOTween.Kill(base.transform, false);
		this.rigid2d.freezeRotation = false;
		this.rigid2d.velocity = new Vector2(UnityEngine.Random.Range(-1f, 1f), 1f);
		this.rigid2d.mass = 1.5f;
		this.rigid2d.gravityScale = 1f;
	}

	private IEnumerator EnemyMove()
	{
		EnemySpace._EnemyMove_c__Iterator1 _EnemyMove_c__Iterator = new EnemySpace._EnemyMove_c__Iterator1();
		_EnemyMove_c__Iterator._this = this;
		return _EnemyMove_c__Iterator;
	}

	private IEnumerator DisableSpace()
	{
		EnemySpace._DisableSpace_c__Iterator2 _DisableSpace_c__Iterator = new EnemySpace._DisableSpace_c__Iterator2();
		_DisableSpace_c__Iterator._this = this;
		return _DisableSpace_c__Iterator;
	}

	private IEnumerator DisableSpace2()
	{
		EnemySpace._DisableSpace2_c__Iterator3 _DisableSpace2_c__Iterator = new EnemySpace._DisableSpace2_c__Iterator3();
		_DisableSpace2_c__Iterator._this = this;
		return _DisableSpace2_c__Iterator;
	}
}
