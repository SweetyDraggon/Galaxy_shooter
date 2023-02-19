using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class EnemyManager : MonoBehaviour
{
	private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal EnemyManager _this;

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

		public _Start_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Invoke("InitAttack", UnityEngine.Random.Range(0f, 2f));
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

	public Sprite[] spriteItems;

	public Sprite[] spriteBullets;

	public Sprite[] spriteEffBullet;

	internal static EnemyManager Instance;

	public Item goBonus;

	public Item goTrung;

	public Item goDuiGa;

	public Item goShit;

	public Item goGold;

	public Item goSilver;

	public Item goThitGa1;

	public Item goThitGa2;

	public Item goBulletEnemy9;

	public Item goHamberger;

	public BulletEnemy12 bullet12;

	public Transform trnBullet12;
	public int childecol;

	private void Awake()
	{
		EnemyManager.Instance = null;
		EnemyManager.Instance = this;
	}
    private void Update()
    {
		childecol = this.transform.childCount;           
    }
    private IEnumerator Start()
	{
		EnemyManager._Start_c__Iterator0 _Start_c__Iterator = new EnemyManager._Start_c__Iterator0();
		_Start_c__Iterator._this = this;
		return _Start_c__Iterator;
	}

	internal Item InstanceBulletBoss1(Vector3 posInit)
	{
		Item item = base.transform.InstanceItem(this.goTrung);
		TypeItem typeItem = TypeItem.Trung;
		item.GetComponent<SpriteRenderer>().sprite = this.spriteItems[0];
		Item arg_4A_0 = item;
		Vector3 vector = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), -1f);
		arg_4A_0.arrow = vector.normalized;
		item.transform.position = posInit;
		item.speed = 2f;
		//UnityEngine.Random.Range(0.5f, 2f) * 2;
		item.typeItem = typeItem;
		return item;
	}

	private bool checkRandomItem()
	{
		return LoadMap.idMap <= 3 && GamePlay.Instance.loadingManager.loadMap.isWaveBoss();
	}

	internal Item GetPool(TypeItem type, Item goItem)
	{
		//MonoBehaviour.print(goItem);
		Item item = base.transform.InstanceItem(goItem);
		item.typeItem = type;
		return item;
	}

	internal Item InstanceItem(params TypeItem[] prTypes)
	{
		if (!this.checkRandomItem() && prTypes[0] != TypeItem.BonusBullet && UnityEngine.Random.Range(0, 2) == 0)
		{
			return null;
		}
		TypeItem typeItem = prTypes[UnityEngine.Random.Range(0, prTypes.Length)];
		Item goItem = null;
		switch (typeItem)
		{
		case TypeItem.Trung:
			goItem = this.goTrung;
			break;
		case TypeItem.DuiGa:
			goItem = this.goDuiGa;
			break;
		case TypeItem.Shit:
			goItem = this.goShit;
			break;
		case TypeItem.BonusBullet:
			goItem = this.goBonus;
			break;
		case TypeItem.Gold:
			goItem = this.goGold;
			break;
		case TypeItem.Silver:
			goItem = this.goSilver;
			break;
		case TypeItem.ThitGa1:
			goItem = this.goThitGa1;
			break;
		case TypeItem.ThitGa2:
			goItem = this.goThitGa2;
			break;
		case TypeItem.BulletEnemy9:
			goItem = this.goBulletEnemy9;
			break;
		case TypeItem.Hamberger:
			goItem = this.goHamberger;
			break;
		}
		Item pool = this.GetPool(typeItem, goItem);
		pool.gameObject.SetActive(true);
		pool.typeItem = typeItem;
		return pool;
	}

	internal void InstanceBullet12(Vector3 pos)
	{
		for (int i = 0; i < this.trnBullet12.childCount; i++)
		{
			BulletEnemy12 component = this.trnBullet12.GetChild(i).GetComponent<BulletEnemy12>();
			if (component.isMove)
			{
				component.gameObject.SetActive(true);
				component.Init(pos);
				return;
			}
		}
		UnityEngine.Object.Instantiate<BulletEnemy12>(this.bullet12, this.trnBullet12).Init(pos);
	}

	private void InitAttack()
	{
		Enemy[] componentsInChildren = GamePlay.Instance.loadingManager.loadMap.GetComponentsInChildren<Enemy>();
		int num = componentsInChildren.Length;
		if (num > 0)
		{
			int num2 = UnityEngine.Random.Range(0, num);
			Transform transform = componentsInChildren[num2].transform;
			if (transform.gameObject.activeSelf)
			{
				transform.SendMessage("Attack", SendMessageOptions.DontRequireReceiver);
			}
		}
		base.Invoke("InitAttack", UnityEngine.Random.Range(0f, 3f));
		base.Invoke("InitMoveAttack", UnityEngine.Random.Range(1f, 3f));
	}

	private void InitMoveAttack()
	{
		int childCount = GamePlay.Instance.loadingManager.loadMap.transform.childCount;
		if (childCount > 0)
		{
			int index = UnityEngine.Random.Range(0, childCount);
			Transform child = GamePlay.Instance.loadingManager.loadMap.transform.GetChild(index);
			if (child.gameObject.activeSelf && LoadMap.idMap > 3)
			{
				child.SendMessage("MoveAttack", SendMessageOptions.DontRequireReceiver);
			}
		}
		base.Invoke("InitMoveAttack", UnityEngine.Random.Range(1f, 4f));
	}

	internal Item InitBullet(GameObject goBullet)
	{
		Item component = UnityEngine.Object.Instantiate<GameObject>(goBullet).GetComponent<Item>();
		component.transform.SetParent(base.transform);
		return component;
	}

	internal static bool isEnableOnHit(Vector3 pos)
	{
		return pos[0] > -3.6f && pos[0] < 3.6f && pos[1] > -6.4f && pos[1] < 6.4f;
	}

	internal Enemy InitEnemy()
	{
		Enemy enemy = GamePlay.Instance.loadingManager.loadMap.arrEnemy[0] as Enemy;
		Enemy enemy2 = UnityEngine.Object.Instantiate<Enemy>(enemy);
		enemy2.name = enemy.name;
		enemy2.Health = 10f;
		enemy2.transform.SetParent(GamePlay.Instance.loadingManager.loadMap.transform);
		return enemy2;
	}

	public void DestroysChilds()
	{
		for (int i = 0; i < childecol; i++)
		{
			GameObject enem = this.transform.GetChild(i).gameObject;
			Destroy(enem);
		}
		UnityEngine.Debug.Log("work1");
	}
}
