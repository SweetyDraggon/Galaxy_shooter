using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using Mr1;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class Enemy : MonoBehaviour
{
	private sealed class _Load1_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Hashtable param;

		internal Vector3[] _path___0;

		internal InfoEnemy _info___0;

		internal float _time___0;

		internal float _dis___0;

		internal Enemy _this;

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

		public _Load1_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._path___0 = (this.param["path"] as Vector3[]);
				this._info___0 = (InfoEnemy)this.param["info"];
				this._this.isDie = false;
				this._time___0 = (float)this.param["time"];
				this._this.Health = (this._this.MaxHealth = (float)this._info___0.Health);
				this._this.posInit = this._path___0[this._path___0.Length - 1];
				if (this._info___0.Type == TypeEnemy.Enemy1)
				{
					this._this.SetTypeEnemy0();
				}
				this._dis___0 = 0f;
				for (int i = 1; i < this._path___0.Length; i++)
				{
					this._dis___0 += Vector3.Distance(this._path___0[i - 1], this._path___0[i]);
				}
				this._current = new WaitUntil(() => !GamePlay.Instance.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.isMove = true;
				this._this.transform.DOLocalPath(this._path___0, this._dis___0 / (2f * this._time___0 * this._info___0.Speed), PathType.Linear, PathMode.Full3D, 10, null).SetSpeedBased<TweenerCore<Vector3, Path, PathOptions>>().OnComplete(delegate
				{
					this._this.isDone = true;
					this._this.isMove = false;
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
			this._this.isDone = true;
			this._this.isMove = false;
		}
	}

	private sealed class _Load2_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal InfoEnemy info;

		internal string namePath;

		internal float speed;

		internal bool rotation;

		internal Enemy _this;

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

		public _Load2_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.isDie = false;
				this._this.Health = (this._this.MaxHealth = (float)this.info.Health);
				this._this.posInit = LoadMap.GridToVector((float)this.info.dx, (float)this.info.dy, LoadMap.distance);
				this._this.transform.position = WaypointManager.instance.GetPathData(this.namePath).startPoint;
				if (this.info.Type == TypeEnemy.Enemy1)
				{
					this._this.SetTypeEnemy0();
				}
				this._current = new WaitUntil(() => !GamePlay.Instance.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitForSeconds(this.info.Delay);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.transform.localScale = Vector3.one;
				this._this.transform.FollowPath(this.namePath, this.speed, (!LoadMap.infoMap.Loop) ? FollowType.Once : FollowType.Loop, FollowDirection.Forward).SetLookForward(this.rotation);
				LoadMap.isDone = true;
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
	}

	private sealed class _MoveAttack_c__AnonStorey3
	{
		internal Vector3 posTarget;

		internal Enemy _this;

		internal void __m__0()
		{
			this._this.transform.DOLocalMove(this.posTarget, (float)UnityEngine.Random.Range(2, 4), false).OnComplete(delegate
			{
				this._this.transform.DOLocalMove(this._this.posInit, 1f, false).SetDelay(UnityEngine.Random.Range(0f, 1f)).OnComplete(delegate
				{
					this._this.isMove = false;
				});
			});
		}

		internal void __m__1()
		{
			this._this.transform.DOLocalMove(this._this.posInit, 1f, false).SetDelay(UnityEngine.Random.Range(0f, 1f)).OnComplete(delegate
			{
				this._this.isMove = false;
			});
		}

		internal void __m__2()
		{
			this._this.isMove = false;
		}
	}

	private sealed class _Disable_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Enemy _this;

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

		public _Disable_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				DOTween.Kill(this._this.transform, false);
				this._current = new WaitForSeconds(0.3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
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

	public float Health = 10f;

	public float MaxHealth;

	public ParticleSystem Eff;

	public TypeEnemy typeEnemy;

	internal bool isDone;

	internal bool isMove;

	public TypeBullet idBonus;

	public bool isDie;

	internal Vector3 posInit;

	public TypeItem typeItem;

	public SkeletonAnimation skeAni;

	private void Awake()
	{
		Action<object> callback = delegate(object x)
		{
			if (this)
				base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	private void OnEnable()
	{
		this.isDie = (this.isDone = (this.isMove = false));
		if (this.Eff)
		{
			this.Eff.transform.localPosition = Vector3.zero;
		}
	}

	private void OnDisable()
	{
		if (base.gameObject)
		{
			base.StopAllCoroutines();
			base.transform.DOKill(false);
			Destroy(base.gameObject, 2f);
		}
	}

	internal void Init(Hashtable param)
	{
		base.StartCoroutine(this.Load1(param));
	}

	internal void SetPath(string namePath, InfoEnemy info, float speed, bool rotation)
	{
		base.StartCoroutine(this.Load2(namePath, info, speed, rotation));
	}

	private void SetTypeEnemy0()
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		if (num <= 0.5f)
		{
			if (num > 0.25f)
			{
			}
		}
	}

	private IEnumerator Load1(Hashtable param)
	{
		Enemy._Load1_c__Iterator0 _Load1_c__Iterator = new Enemy._Load1_c__Iterator0();
		_Load1_c__Iterator.param = param;
		_Load1_c__Iterator._this = this;
		return _Load1_c__Iterator;
	}

	private IEnumerator Load2(string namePath, InfoEnemy info, float speed, bool rotation)
	{
		Enemy._Load2_c__Iterator1 _Load2_c__Iterator = new Enemy._Load2_c__Iterator1();
		_Load2_c__Iterator.info = info;
		_Load2_c__Iterator.namePath = namePath;
		_Load2_c__Iterator.speed = speed;
		_Load2_c__Iterator.rotation = rotation;
		_Load2_c__Iterator._this = this;
		return _Load2_c__Iterator;
	}

	private void Attack()
	{
		if (this.Health <= 0f)
		{
			return;
		}
		if (this.typeEnemy == TypeEnemy.Enemy12)
		{
			EnemyManager.Instance.InstanceBullet12(base.transform.position);
			return;
		}
		Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
		{
			this.typeItem
		});
		if (item)
		{
			item.Init(base.transform.position, TypeBullet.Bullet1);
		}
	}

	private void Enemy9Attack()
	{
		if (this.Health <= 0f)
		{
			return;
		}
		Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
		{
			this.typeItem
		});
		if (item)
		{
			item.Init(base.transform.position, TypeBullet.Bullet1);
			EnemyManager.Instance.GetPool(this.typeItem, EnemyManager.Instance.goBulletEnemy9).Init(base.transform.position, TypeBullet.Bullet2);
		}
	}

	private void MoveAttack()
	{
		Enemy._MoveAttack_c__AnonStorey3 _MoveAttack_c__AnonStorey = new Enemy._MoveAttack_c__AnonStorey3();
		_MoveAttack_c__AnonStorey._this = this;
		if (this.Health <= 0f || this.isMove || !this.isDone)
		{
			return;
		}
		_MoveAttack_c__AnonStorey.posTarget = PlayerController.Instance.transform.position - base.transform.parent.position;
		Enemy._MoveAttack_c__AnonStorey3 expr_64_cp_0 = _MoveAttack_c__AnonStorey;
		expr_64_cp_0.posTarget.x = expr_64_cp_0.posTarget.x + UnityEngine.Random.Range(-0.2f, 0.2f);
		Enemy._MoveAttack_c__AnonStorey3 expr_85_cp_0 = _MoveAttack_c__AnonStorey;
		expr_85_cp_0.posTarget.y = expr_85_cp_0.posTarget.y + UnityEngine.Random.Range(0.3f, 0.5f);
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
		this.isMove = true;
		base.transform.DOLocalMoveY(0.5f, 1f, false).OnComplete(delegate
		{
			_MoveAttack_c__AnonStorey._this.transform.DOLocalMove(_MoveAttack_c__AnonStorey.posTarget, (float)UnityEngine.Random.Range(2, 4), false).OnComplete(delegate
			{
				_MoveAttack_c__AnonStorey._this.transform.DOLocalMove(_MoveAttack_c__AnonStorey._this.posInit, 1f, false).SetDelay(UnityEngine.Random.Range(0f, 1f)).OnComplete(delegate
				{
					_MoveAttack_c__AnonStorey._this.isMove = false;
				});
			});
		});
	}

	private void SetBonus(TypeBullet value)
	{
		this.idBonus = value;
	}

	private void OnHit(float prDame)
	{
		if (this.isDie || GamePlay.Instance.loadingManager.gameObject.activeSelf)
		{
			return;
		}
		if (!EnemyManager.isEnableOnHit(base.transform.position))
		{
			return;
		}
		this.Health -= prDame;
		if (this.Health <= 0f)
		{
			SoundManager.Instance.PlaySoundEnemy(0);
			EnemyManager.Instance.InstanceItem(new TypeItem[]
			{
				TypeItem.BonusBullet
			}).Init(base.transform.position, this.idBonus);
			this.isDie = true;
			this.Health = 0f;
			this.Eff.Play();
			base.StartCoroutine(this.Disable());
			if (this.typeEnemy == TypeEnemy.Enemy1 || this.typeEnemy == TypeEnemy.Enemy2 || this.typeEnemy == TypeEnemy.Enemy3)
			{
				Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
				{
					TypeItem.Gold,
					TypeItem.Silver,
					TypeItem.DuiGa
				});
				if (item)
				{
					item.Init(base.transform.position, TypeBullet.Bullet1);
				}
			}
			else
			{
				Item item2 = EnemyManager.Instance.InstanceItem(new TypeItem[]
				{
					TypeItem.DuiGa,
					TypeItem.ThitGa1,
					TypeItem.ThitGa2
				});
				if (item2)
				{
					item2.Init(base.transform.position, TypeBullet.Bullet1);
				}
			}
		}
		else
		{
			SoundManager.Instance.PlaySoundEnemy(0);
		}
	}

	public IEnumerator Disable()
	{
		Enemy._Disable_c__Iterator2 _Disable_c__Iterator = new Enemy._Disable_c__Iterator2();
		_Disable_c__Iterator._this = this;
		return _Disable_c__Iterator;
	}
}
