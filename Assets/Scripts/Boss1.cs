using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using zZ17;

public class Boss1 : Boss
{
	private sealed class _SetDie_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Boss1 _this;

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

		public _SetDie_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.Eff.gameObject.SetActive(true);
				this._this.Eff.Play();
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				LoadMap.isDone = true;
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

	public float Health = 300f;

	public float MaxHealth;

	public ParticleSystem Eff;

	public Transform spriteHealth;

	internal bool isBonus;

	internal bool isDone;

	internal bool isMove;

	internal bool isDie;

	internal Vector3 posInit;

	private UbhShotCtrl shotCtrl;

	public Collider2D coll;

	public int id = -1;

	public TypeBoss typeBoss;

	[SpineAnimation("", "", true, false)]
	public string ANIMATION_ATTACK1;

	[SpineAnimation("", "", true, false)]
	public string ANIMATION_ATTACK2;

	[SpineAnimation("", "", true, false)]
	public string ANIMATION_STAND;

	[SpineEvent("", "", true, false)]
	public string EVENT_START1;

	[SpineEvent("", "", true, false)]
	public string EVENT_START2;

	public Transform trnGun1;

	public Transform trnGun2;

	public Transform trnEye1;

	public Transform trnEye2;

	public Transform trnEye3;

	public GameObject bullet1;

	private static UnityAction<string, string> __f__am_cache0;

	public override void Start()
	{
		base.Start();
		Action<object> callback = delegate(object x)
		{
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	private void Awake()
	{
		this.CallbackEvent = (UnityAction<string, string>)Delegate.Combine(this.CallbackEvent, new UnityAction<string, string>(delegate(string _, string __)
		{
			MonoBehaviour.print(_ + "/" + __);
		}));
		Action<object> callback = delegate(object x)
		{
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	private void Init()
	{
		this.coll.enabled = true;
		this.MaxHealth = (this.Health = 1500f);
		this.isDie = false;
	}

	private void MoveDone()
	{
		this.ShotControl(0);
		this.spriteHealth.localScale = new Vector3(1f, 1f, 1f);
		base.Invoke("Move", (float)UnityEngine.Random.Range(0, 5));
	}

	private void Move()
	{
		if (this.shotCtrl)
		{
			this.shotCtrl.StopShotRoutine();
		}
		base.transform.DOMove(new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(-1f, 3.5f)), UnityEngine.Random.Range(3f, 3.5f), false).SetDelay(1f).OnComplete(delegate
		{
			this.Attack();
		});
	}

	private void OnHit(float prDame)
	{
		if (this.isDie)
		{
			return;
		}
		if (this.Health - prDame < 0f)
		{
			this.Health = 0f;
		}
		else
		{
			this.Health -= prDame;
		}
		float num = this.Health / this.MaxHealth;
		this.spriteHealth.localScale = new Vector3(num, 1f, 1f);
		if (num <= 0.6f)
		{
			if (num > 0.3f)
			{
				if (this.id == -1)
				{
					this.InstanceBonus();
				}
			}
			else if (num > 0.1f)
			{
				if (this.id == 0)
				{
					this.InstanceBonus();
				}
			}
			else if (this.id == 1)
			{
				this.InstanceBonus();
			}
		}
		if (this.Health <= 0f)
		{
			if (this.isDie)
			{
				return;
			}
			this.Health = 0f;
			this.isDie = true;
			this.coll.enabled = true;
			DOTween.Kill(this, false);
			GamePlay.Instance.DisableItemEnemy();
			GameManager.Instance.gamePlayManager.ChangeState(StateGame.NEXTLEVEL);
			int num2 = UnityEngine.Random.Range(30, 45);
			for (int i = 0; i < num2; i++)
			{
				Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
				{
					TypeItem.Gold,
					TypeItem.Silver,
					TypeItem.DuiGa,
					TypeItem.ThitGa1,
					TypeItem.ThitGa2
				});
				if (item)
				{
					item.Init(base.transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)), TypeBullet.Bullet1);
				}
			}
			GameManager.Instance.StartCoroutine(this.SetDie());
			coll.gameObject.SetActive(false);
		}
	}

	private void InstanceBonus()
	{
		int type = UnityEngine.Random.Range(0, 5);
		Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
		{
			TypeItem.BonusBullet
		});
		if (item)
		{
			item.Init(base.transform.position, (TypeBullet)type);
		}
		this.id++;
	}

	private void ShotControl(int index)
	{
		base.transform.DOLocalMoveY(base.transform.localPosition[1] + 0.3f, 0.2f, false).SetLoops(9, LoopType.Yoyo).OnStepComplete(delegate
		{
			this.Shot();
		});
	}

	private void Shot()
	{
		if (this.Health <= 0f || !base.gameObject.activeSelf)
		{
			return;
		}
		if (GameManager.Instance.loadManager.gameObject.activeSelf)
		{
			return;
		}
		Vector3 vector = base.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
		Item item = EnemyManager.Instance.InstanceBulletBoss1(vector);
	}

	private void Attack()
	{
		if (this.Health <= 0f || !base.gameObject.activeSelf)
		{
			return;
		}
		if (this.shotCtrl)
		{
			this.shotCtrl.StopShotRoutine();
		}
		this.ShotControl(0);
		base.Invoke("Move", UnityEngine.Random.Range(3f, 4f));
	}

	private IEnumerator SetDie()
	{
		Boss1._SetDie_c__Iterator0 _SetDie_c__Iterator = new Boss1._SetDie_c__Iterator0();
		_SetDie_c__Iterator._this = this;
		return _SetDie_c__Iterator;
	}
}
