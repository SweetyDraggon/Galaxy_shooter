using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class Item : MonoBehaviour
{
	private sealed class _DisableDuiGa_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Item _this;

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

		public _DisableDuiGa_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => this._this.rigid2D.velocity == Vector2.zero);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Disable();
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
			return this._this.rigid2D.velocity == Vector2.zero;
		}
	}

	private sealed class _DisableEgg_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Item _this;

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

		public _DisableEgg_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.gameObject.SetActive(false);
				this._this.transform.SetAsFirstSibling();
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

	public TypeItem typeItem;

	internal TypeBullet typeBullet;

	internal float speed;

	internal Vector3 arrow;

	private Rigidbody2D rigid2D;

	private void Start()
	{
		EventDispatcher.Instance.RegisterListener(EventID.BulletEnemy, new Action<object>(this.Callback));
		EventDispatcher.Instance.RegisterListener(EventID.Disable, delegate(object _)
		{
			//if (base.gameObject)
			//	base.gameObject.SetActive(false);
		});
	}

	internal bool checkTypes(params TypeItem[] types)
	{
		for (int i = 0; i < types.Length; i++)
		{
			if (types[i] == this.typeItem)
			{
				return true;
			}
		}
		return false;
	}

	private void Callback(object obj)
	{
		TypeItem typeItem = this.typeItem;
		switch (typeItem)
		{
		case TypeItem.Trung:
		case TypeItem.Shit:
			goto IL_2D;
		case TypeItem.DuiGa:
			//IL_19:
			if (typeItem != TypeItem.BulletSpace && typeItem != TypeItem.BulletEnemy9)
			{
				return;
			}
			goto IL_2D;
		}
		goto IL_19;
		IL_2D:
		if (Mathf.Abs(base.transform.position.y) <= 6.4f && Mathf.Abs(base.transform.position.x) <= 3.6f)
		{
			base.gameObject.SetActive(false);
		}
       IL_19:
        if (typeItem != TypeItem.BulletSpace && typeItem != TypeItem.BulletEnemy9)
        {
            return;
        }
        goto IL_2D;
    }

	internal void Init(Vector3 pos, TypeBullet type = TypeBullet.Bullet1)
	{
		this.arrow = Vector3.down;
		this.speed = 0f;
		this.typeBullet = type;
		base.transform.position = pos;
		this.rigid2D = base.GetComponent<Rigidbody2D>();
		this.rigid2D.isKinematic = false;
		this.rigid2D.gravityScale = 0f;
		if (this.typeItem == TypeItem.BonusBullet)
		{
			if (type == TypeBullet.None)
			{
				base.gameObject.SetActive(false);
				return;
			}
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = EnemyManager.Instance.spriteEffBullet[(int)this.typeBullet];
			base.GetComponent<SpriteRenderer>().sprite = EnemyManager.Instance.spriteBullets[(int)this.typeBullet];
		}
		switch (this.typeItem)
		{
		case TypeItem.Trung:
			base.GetComponent<SpriteRenderer>().sprite = EnemyManager.Instance.spriteItems[0];
			this.speed = 7f;
			break;
		case TypeItem.DuiGa:
		case TypeItem.Gold:
		case TypeItem.Silver:
		case TypeItem.ThitGa1:
		case TypeItem.ThitGa2:
			this.rigid2D.gravityScale = UnityEngine.Random.Range(0.5f, 1f);
			this.rigid2D.velocity = new Vector2((float)UnityEngine.Random.Range(-1, 2), (float)UnityEngine.Random.Range(0, 2)) * 3f;
			this.rigid2D.AddTorque((float)UnityEngine.Random.Range(-20, 20));
			base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
			break;
		case TypeItem.Shit:
			this.speed = 5f;
			break;
		case TypeItem.BonusBullet:
			this.speed = 3f;
			break;
		case TypeItem.BulletEnemy9:
		{
			Vector3 vector = new Vector3((float)((this.typeBullet != TypeBullet.Bullet1) ? (-1) : 1), -2f);
			this.arrow = vector.normalized;
			base.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(base.transform.position.y - this.arrow.y, base.transform.position.x - this.arrow.x) * 57.29578f - 90f);
			this.speed = 4f;
			break;
		}
		}
	}

	private void Update()
	{
		if (this.typeItem != TypeItem.BulletSpace)
		{
			base.transform.Translate(this.arrow * Time.deltaTime * this.speed);
		}
	}

	private void Disable()
	{
		base.gameObject.SetActive(false);
		base.transform.SetAsFirstSibling();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (PlayerController.isDie)
			{
				return;
			}
			switch (this.typeItem)
			{
			case TypeItem.DuiGa:
				GamePlay.Instance.gamePlayManager.SetScore(3);
				GamePlay.Instance.gamePlayManager.SetMana(1);
				goto IL_127;
			case TypeItem.BonusBullet:
				SoundManager.Instance.PlaySoundPlayer(1);
				PlayerController.Instance.effBullet.Play();
				this.PostEvent(EventID.Bullet, this.typeBullet);
				goto IL_127;
			case TypeItem.Gold:
				GamePlay.Instance.gamePlayManager.SetScore(2);
				goto IL_127;
			case TypeItem.Silver:
				GamePlay.Instance.gamePlayManager.SetScore(1);
				goto IL_127;
			case TypeItem.ThitGa1:
			case TypeItem.ThitGa2:
				GamePlay.Instance.gamePlayManager.SetScore(5);
				GamePlay.Instance.gamePlayManager.SetMana(5);
				goto IL_127;
			case TypeItem.Hamberger:
				GamePlay.Instance.gamePlayManager.SetMana(7);
				goto IL_127;
			}
			other.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
			IL_127:
			this.Disable();
		}
		else if (other.tag == "Safe")
		{
			if (this.typeItem == TypeItem.BonusBullet)
			{
				SoundManager.Instance.PlaySoundPlayer(1);
				PlayerController.Instance.effBullet.Play();
				this.PostEvent(EventID.Bullet, this.typeBullet);
			}
			this.Disable();
		}
		else if (other.tag == "NenDuoi")
		{
			if (this.typeItem == TypeItem.DuiGa || this.typeItem == TypeItem.Gold || this.typeItem == TypeItem.Silver || this.typeItem == TypeItem.ThitGa1 || this.typeItem == TypeItem.ThitGa2)
			{
				base.StartCoroutine(this.DisableDuiGa());
			}
			else
			{
				this.Disable();
			}
		}
	}

	private IEnumerator DisableDuiGa()
	{
		Item._DisableDuiGa_c__Iterator0 _DisableDuiGa_c__Iterator = new Item._DisableDuiGa_c__Iterator0();
		_DisableDuiGa_c__Iterator._this = this;
		return _DisableDuiGa_c__Iterator;
	}

	private IEnumerator DisableEgg()
	{
		Item._DisableEgg_c__Iterator1 _DisableEgg_c__Iterator = new Item._DisableEgg_c__Iterator1();
		_DisableEgg_c__Iterator._this = this;
		return _DisableEgg_c__Iterator;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.transform.tag == "Player")
		{
			if (PlayerController.isDie)
			{
				return;
			}
			switch (this.typeItem)
			{
			case TypeItem.Trung:
			case TypeItem.BulletSpace:
				coll.transform.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
				break;
			case TypeItem.DuiGa:
				GameManager.Instance.gamePlayManager.SetScore(3);
				GameManager.Instance.gamePlayManager.SetMana(1);
				break;
			case TypeItem.Shit:
				coll.transform.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
				break;
			case TypeItem.BonusBullet:
				SoundManager.Instance.PlaySoundPlayer(1);
				this.PostEvent(EventID.Bullet, this.typeBullet);
				break;
			case TypeItem.Gold:
				GameManager.Instance.gamePlayManager.SetScore(15);
				break;
			case TypeItem.Silver:
				GameManager.Instance.gamePlayManager.SetScore(10);
				break;
			case TypeItem.ThitGa1:
			case TypeItem.ThitGa2:
				GameManager.Instance.gamePlayManager.SetScore(5);
				GameManager.Instance.gamePlayManager.SetMana(5);
				break;
			}
			this.Disable();
		}
		else if (coll.transform.tag == "NenDuoi")
		{
			if (this.typeItem == TypeItem.DuiGa || this.typeItem == TypeItem.Gold || this.typeItem == TypeItem.Silver || this.typeItem == TypeItem.ThitGa1 || this.typeItem == TypeItem.ThitGa2)
			{
				if (base.gameObject.activeSelf)
				{
					base.StartCoroutine(this.DisableDuiGa());
				}
			}
			else
			{
				this.Disable();
			}
		}
	}
}
