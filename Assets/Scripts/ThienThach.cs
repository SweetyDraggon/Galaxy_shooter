using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class ThienThach : MonoBehaviour
{
	private sealed class _DisableEnemy_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal bool isVaCham;

		internal ThienThach _this;

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

		public _DisableEnemy_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.CancelInvoke("Move");
				this._this.goBody.SetActive(false);
				this._this.effExplode.gameObject.SetActive(this.isVaCham);
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.goBody.transform.localPosition = Vector3.zero;
				this._this.effExplode.gameObject.SetActive(false);
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

	public ParticleSystem effExplode;

	public GameObject goBody;

	internal Vector3 target;

	internal float speed;

	internal TypeBullet idBonus;

	internal float HP;

	private int id;

	private Vector3 arrow;

	private void Awake()
	{
		Action<object> callback = delegate(object x)
		{
			base.StopCoroutine(this.DisableEnemy(true));
			base.CancelInvoke();
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	private void Move()
	{
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
		base.transform.Translate(this.arrow * 0.1f * this.speed);
		if (base.transform.position.y <= -7f)
		{
			base.StartCoroutine(this.DisableEnemy(false));
		}
	}

	private void SetBonus(TypeBullet value)
	{
		this.idBonus = value;
	}

	private void Init(InfoEnemy info)
	{
		this.arrow = ((LoadMap.infoMap.idThienThach != 1) ? ((LoadMap.infoMap.idThienThach != 2) ? new Vector3(3f, -6.4f) : new Vector3(-3f, -6.4f)) : new Vector3(0f, -1f));
		this.arrow = this.arrow.normalized;
		this.target = LoadMap.infoMap.posStart;
		this.goBody.SetActive(true);
		this.id = (int)info.Type;
		this.speed = LoadMap.infoMap.Speed;
		this.HP = (float)info.Health;
		base.InvokeRepeating("Move", 0f, 0.02f);
	}

	private void OnHit(float prDame)
	{
		if (this.HP == 0f)
		{
			return;
		}
		if (prDame == 7f)
		{
			prDame = 100f;
		}
		this.HP -= prDame;
		if (this.HP <= 0f)
		{
			this.HP = 0f;
			GameManager.Instance.gamePlayManager.SetScore(this.id + 3);
			base.StartCoroutine(this.DisableEnemy(true));
			EnemyManager.Instance.InstanceItem(new TypeItem[]
			{
				TypeItem.BonusBullet
			}).Init(base.transform.position, this.idBonus);
		}
	}

	private IEnumerator DisableEnemy(bool isVaCham = true)
	{
		ThienThach._DisableEnemy_c__Iterator0 _DisableEnemy_c__Iterator = new ThienThach._DisableEnemy_c__Iterator0();
		_DisableEnemy_c__Iterator.isVaCham = isVaCham;
		_DisableEnemy_c__Iterator._this = this;
		return _DisableEnemy_c__Iterator;
	}
}
