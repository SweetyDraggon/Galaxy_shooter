using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class Boss5 : MonoBehaviour
{
	private sealed class _SetDie_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Boss5 _this;

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
				this._this.goShotList[0].GetComponent<UbhShotCtrl>().StopAllCoroutines();
				this._this.goShotList[1].GetComponent<UbhShotCtrl>().StopAllCoroutines();
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

	public List<GameObject> goShotList;

	public float Health = 300f;

	public float MaxHealth;

	public Transform trnSpam;

	public ParticleSystem Eff;

	public Transform spriteHealth;

	internal bool isBonus;

	internal bool isDone;

	internal bool isMove;

	internal bool isDie;

	internal Vector3 posInit;

	public Collider2D coll;

	private UbhShotCtrl shotCtrl;

	public int id = -1;

	private void Awake()
	{
		Action<object> callback = delegate(object x)
		{
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	private void Init()
	{
		this.coll.enabled = true;
		this.isDie = false;
	}

	private void MoveDone()
	{
		this.id = -1;
		this.ShotControl(0);
		this.spriteHealth.localScale = new Vector3(1f, 1f, 1f);
		base.Invoke("Move", UnityEngine.Random.Range(2f, 5f));
	}

	private void Move()
	{
		this.ShotControl(0);
		base.transform.DOMove(new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(0f, 3.5f)), UnityEngine.Random.Range(3f, 6f), false).SetDelay(1f).OnComplete(delegate
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
		if (prDame == 15f)
		{
			MonoBehaviour.print("Bomb explode");
		}
		if (this.Health - prDame < 0f)
		{
			this.Health = 0f;
		}
		else
		{
			this.Health -= prDame;
		}
		this.spriteHealth.localScale = new Vector3(this.Health / this.MaxHealth, 1f, 1f);
		if (this.Health <= 0f)
		{
			GamePlay.Instance.DisableItemEnemy();
			GamePlay.Instance.gamePlayManager.ChangeState(StateGame.NEXTLEVEL);
			int num = UnityEngine.Random.Range(30, 45);
			for (int i = 0; i < num; i++)
			{
				Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
				{
					TypeItem.Gold,
					TypeItem.Silver,
					TypeItem.ThitGa1,
					TypeItem.ThitGa2
				});
				if (item)
				{
					item.Init(base.transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)), TypeBullet.Bullet1);
				}
			}
			this.Health = 0f;
			this.isDie = true;
			this.coll.enabled = false;
			GameManager.Instance.StartCoroutine(this.SetDie());

			coll.gameObject.SetActive(false);
		}
	}

	private IEnumerator SetDie()
	{
		Boss5._SetDie_c__Iterator0 _SetDie_c__Iterator = new Boss5._SetDie_c__Iterator0();
		_SetDie_c__Iterator._this = this;
		return _SetDie_c__Iterator;
	}

	private void ShotControl(int index)
	{
		if (this.Health == 0f)
		{
			return;
		}
		if (this.shotCtrl != null)
		{
			this.shotCtrl.StopShotRoutine();
		}
		this.shotCtrl = this.goShotList[index].GetComponent<UbhShotCtrl>();
		if (this.shotCtrl != null)
		{
			this.shotCtrl.StartShotRoutine();
		}
	}

	private void Attack()
	{
		this.ShotControl(1);
		base.Invoke("Move", UnityEngine.Random.Range(5f, 6f));
	}

	private string stateNormal()
	{
		float num = this.Health / this.MaxHealth;
		if (num > 0.6f)
		{
			return "animation";
		}
		if (num > 0.3f)
		{
			if (this.id == -1)
			{
				this.EffHit(0);
			}
			return "60%";
		}
		if (num > 0.1f)
		{
			if (this.id == 0)
			{
				this.EffHit(1);
			}
			return "30%";
		}
		if (this.id == 1)
		{
			this.EffHit(2);
		}
		return "10%";
	}

	private void EffHit(int prId)
	{
		int type = UnityEngine.Random.Range(0, 7);
		Item item = EnemyManager.Instance.InstanceItem(new TypeItem[]
		{
			TypeItem.BonusBullet
		});
		if (item)
		{
			item.Init(base.transform.position, (TypeBullet)type);
		}
		this.id = prId;
	}
}
