using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using zZ17;

public class LoadMap : MonoBehaviour
{
	private sealed class _Move_c__AnonStorey6
	{
		internal bool left;

		internal LoadMap _this;

		internal void __m__0()
		{
			this._this.Move(!this.left);
		}
	}

	private sealed class _CheckTurn_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal LoadMap _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

		private static Func<bool> __f__am_cache1;

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

		public _CheckTurn_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => LoadMap.isDone);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitUntil(() => this._this.transform.GetComponentInChildren<Enemy>() == null && this._this.trnThienThach.GetComponentInChildren<ThienThach>() == null);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				if (LoadMap.idBoss == -1 || LoadMap.currentWave != LoadMap.countWave)
				{
					this._current = new WaitUntil(() => GamePlayManager.state == StateGame.PLAY);
					if (!this._disposing)
					{
						this._PC = 3;
					}
					return true;
				}
				if (!PlayerController.isDie)
				{
					GamePlay.Instance.gamePlayManager.ChangeState(StateGame.NEXTLEVEL);
					LoadMap.currentWave++;
				}
				else
				{
					GamePlay.Instance.gamePlayManager.ChangeState(StateGame.GAMEOVER);
				}
				break;
			case 3u:
				LoadMap.currentWave++;
				if (LoadMap.idBoss == -1 || LoadMap.currentWave <= LoadMap.countWave)
				{
					this._this.LoadWave();
					this._PC = -1;
				}
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
			return LoadMap.isDone;
		}

		internal bool __m__1()
		{
			return this._this.transform.GetComponentInChildren<Enemy>() == null && this._this.trnThienThach.GetComponentInChildren<ThienThach>() == null;
		}

		private static bool __m__2()
		{
			return GamePlayManager.state == StateGame.PLAY;
		}
	}

	private sealed class _LoadMap1_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _typeMove___0;

		internal bool _sortColumn___0;

		internal float _time___0;

		internal List<GroupEnemy> _rs___0;

		internal Vector3 _posStart___0;

		internal int[] _bonus___0;

		internal int _count___0;

		internal LoadMap _this;

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

		public _LoadMap1_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				LoadMap.infoMap = this._this.data.lsInfo[LoadMap.currentWave - 1];
				this._typeMove___0 = LoadMap.infoMap.TypeSort;
				this._sortColumn___0 = (this._typeMove___0 == 1);
				this._time___0 = LoadMap.infoMap.Time;
				LoadMap.distance = LoadMap.infoMap.Distance;
				LoadMap.isDone = false;
				if (LoadMap.infoMap.posUnSafe.Count > 0)
				{
					GamePlay.Instance.SetUnSafe(LoadMap.infoMap.posUnSafe.ToArray());
				}
				if (LoadMap.infoMap.posSafe.Count > 0)
				{
					GamePlay.Instance.SetSafe(LoadMap.infoMap.posSafe.ToArray());
				}
				GamePlay.Instance.SetInfoWave(LoadMap.infoMap.TextWave, LoadMap.currentWave);
				this._rs___0 = LoadMap.infoMap.Grid;
				this._posStart___0 = LoadMap.infoMap.posStart;
				this._bonus___0 = this._this.InitBonus(LoadMap.infoMap.Count);
				LoadMap.width = LoadMap.infoMap.Width;
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (this._this.data.haveBoss && LoadMap.currentWave == LoadMap.countWave)
				{
					LoadMap.isDone = false;
					this._this.LoadBoss();
					this._this.CheckWave();
				}
				else
				{
					this._count___0 = 0;
					if (LoadMap.infoMap.idThienThach != -1)
					{
						for (int i = 0; i < this._rs___0.Count; i++)
						{
							for (int j = 0; j < this._rs___0[i].listEnemy.Count; j++)
							{
								this._count___0++;
								this._this.StartCoroutine(this._this.LoadThienThach(this._rs___0[i].listEnemy[j], this._bonus___0, this._count___0));
							}
						}
					}
					else if (LoadMap.infoMap.namePath[0] == string.Empty)
					{
						for (int k = 0; k < this._rs___0.Count; k++)
						{
							for (int l = 0; l < this._rs___0[k].listEnemy.Count; l++)
							{
								this._count___0++;
								this._this.StartCoroutine(this._this.LoadEnemy(this._rs___0[k].listEnemy[l], this._count___0, this._bonus___0));
							}
						}
					}
					else
					{
						for (int m = 0; m < this._rs___0.Count; m++)
						{
							for (int n = 0; n < this._rs___0[m].listEnemy.Count; n++)
							{
								this._count___0++;
								InfoEnemy infoEnemy = this._rs___0[m].listEnemy[n];
								MonoBehaviour monoBehaviour = null;
								if (this._this.transform.childCount > 0)
								{
									for (int num2 = 0; num2 < this._this.transform.childCount; num2++)
									{
										if (!this._this.transform.GetChild(num2).gameObject.activeSelf && this._this.arrEnemy[(int)infoEnemy.Type].name == this._this.transform.GetChild(num2).name)
										{
											monoBehaviour = this._this.transform.GetChild(num2).GetComponent<Enemy>();
											monoBehaviour.transform.SetAsLastSibling();
											monoBehaviour.transform.GetChild(0).gameObject.SetActive(true);
											monoBehaviour.gameObject.SetActive(true);
											break;
										}
										if (this._this.transform.GetChild(num2).gameObject.activeSelf)
										{
											break;
										}
									}
								}
								if (monoBehaviour == null)
								{
									monoBehaviour = UnityEngine.Object.Instantiate<MonoBehaviour>(this._this.arrEnemy[(int)infoEnemy.Type]);
									monoBehaviour.name = this._this.arrEnemy[(int)infoEnemy.Type].name;
								}
								monoBehaviour.SendMessage("SetBonus", TypeBullet.None, SendMessageOptions.DontRequireReceiver);
								monoBehaviour.transform.SetParent(this._this.transform);
								monoBehaviour.transform.localScale = Vector3.zero;
								for (int num3 = 0; num3 < this._bonus___0.Length; num3++)
								{
									if (this._count___0 == this._bonus___0[num3])
									{
										monoBehaviour.SendMessage("SetBonus", (TypeBullet)LoadMap.infoMap.Bonus[UnityEngine.Random.Range(2, LoadMap.infoMap.Bonus.Count)], SendMessageOptions.DontRequireReceiver);
										break;
									}
								}
								monoBehaviour.transform.localScale = Vector3.zero;
								string path = (!(LoadMap.infoMap.namePath[1] != string.Empty)) ? LoadMap.infoMap.namePath[0] : LoadMap.infoMap.namePath[(infoEnemy.Arrow != -1) ? 1 : 0];
								this._this.StartCoroutine(this._this.LoadEnemy(monoBehaviour as Enemy, path, infoEnemy, LoadMap.infoMap.Speed, LoadMap.infoMap.rotation));
							}
						}
					}
					this._this.CheckWave();
					this._PC = -1;
				}
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

	private sealed class _LoadEnemy_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string path;

		internal Enemy Enemy;

		internal InfoEnemy prInfo;

		internal float speed;

		internal bool rotation;

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

		public _LoadEnemy_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this.path = this.path.Trim();
				this.Enemy.transform.position = new Vector3(5f, 5f);
				this._current = new WaitForSeconds(0f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this.Enemy.SetPath(this.path, this.prInfo, this.speed, this.rotation);
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

	private sealed class _LoadEnemySpace_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal MonoBehaviour _obj___0;

		internal InfoEnemy prInfo;

		internal int[] bonus;

		internal int count;

		internal LoadMap _this;

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

		public _LoadEnemySpace_c__Iterator3()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._obj___0 = null;
				this._obj___0 = UnityEngine.Object.Instantiate<MonoBehaviour>(this._this.arrEnemy[(int)this.prInfo.Type]);
				this._obj___0.name = this._this.arrEnemy[(int)this.prInfo.Type].name;
				this._obj___0.transform.localScale = Vector3.zero;
				this._obj___0.transform.SetParent(this._this.transform);
				this._obj___0.transform.position = new Vector3(5f, 5f);
				this._obj___0.SendMessage("SetBonus", TypeBullet.None, SendMessageOptions.DontRequireReceiver);
				this._current = new WaitForSeconds(this.prInfo.Delay);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				(this._obj___0 as EnemySpace).InitEnemy(this.prInfo);
				for (int i = 0; i < this.bonus.Length; i++)
				{
					if (this.count == this.bonus[i])
					{
						this._obj___0.SendMessage("SetBonus", (TypeBullet)LoadMap.infoMap.Bonus[UnityEngine.Random.Range(2, LoadMap.infoMap.Bonus.Count)], SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
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
	}

	private sealed class _LoadEnemy_c__Iterator4 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _typeMove___0;

		internal bool _sortColumn___0;

		internal float _time___0;

		internal Vector3 _posStart___0;

		internal InfoEnemy prInfo;

		internal Vector3[] _path___1;

		internal MonoBehaviour _obj___0;

		internal int[] bonus;

		internal int count;

		internal Hashtable _table___0;

		internal LoadMap _this;

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

		public _LoadEnemy_c__Iterator4()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._typeMove___0 = LoadMap.infoMap.TypeSort;
				this._sortColumn___0 = (this._typeMove___0 == 1);
				this._time___0 = LoadMap.infoMap.Time;
				this._posStart___0 = LoadMap.infoMap.posStart;
				this._current = new WaitForSeconds(this.prInfo.Delay);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				switch (this._typeMove___0)
				{
				case 0:
					this._path___1 = this._this.MovePath0(this._posStart___0, this.prInfo);
					break;
				case 1:
					this._path___1 = this._this.MovePath1(this._posStart___0, this.prInfo);
					break;
				case 2:
					this._path___1 = this._this.MovePath2(this.prInfo.dx, this.prInfo.dy, this._posStart___0);
					break;
				default:
					this._path___1 = this._this.MoveDontSort(this.prInfo.dx, this.prInfo.dy);
					break;
				}
				this._obj___0 = null;
				if (this._this.transform.childCount > 0)
				{
					for (int i = 0; i < this._this.transform.childCount; i++)
					{
						if (!this._this.transform.GetChild(i).gameObject.activeSelf && this._this.transform.GetChild(i).name == this._this.arrEnemy[(int)this.prInfo.Type].name)
						{
							this._obj___0 = this._this.transform.GetChild(i).GetComponent<MonoBehaviour>();
							this._obj___0.transform.SetAsLastSibling();
							this._obj___0.transform.GetChild(0).gameObject.SetActive(true);
							this._obj___0.gameObject.SetActive(true);
							break;
						}
						if (this._this.transform.GetChild(i).gameObject.activeSelf)
						{
							break;
						}
					}
				}
				if (this._obj___0 == null)
				{
					this._obj___0 = UnityEngine.Object.Instantiate<MonoBehaviour>(this._this.arrEnemy[(int)this.prInfo.Type]);
					this._obj___0.name = this._this.arrEnemy[(int)this.prInfo.Type].name;
				}
				this._obj___0.SendMessage("SetBonus", TypeBullet.None, SendMessageOptions.DontRequireReceiver);
				this._obj___0.transform.SetParent(this._this.transform);
				for (int j = 0; j < this.bonus.Length; j++)
				{
					if (this.count == this.bonus[j])
					{
						this._obj___0.SendMessage("SetBonus", (TypeBullet)LoadMap.infoMap.Bonus[UnityEngine.Random.Range(2, LoadMap.infoMap.Bonus.Count)], SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
				this._obj___0.transform.localScale = Vector3.one;
				this._obj___0.transform.localPosition = this._path___1[0];
				this._table___0 = new Hashtable();
				this._table___0.Add("path", this._path___1);
				this._table___0.Add("info", this.prInfo);
				this._table___0.Add("time", this._time___0);
				this._obj___0.SendMessage("Init", this._table___0, SendMessageOptions.DontRequireReceiver);
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
	}

	private sealed class _LoadThienThach_c__Iterator5 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal InfoEnemy prInfo;

		internal ThienThach _thienthach___0;

		internal MonoBehaviour _obj___0;

		internal int[] bonus;

		internal int count;

		internal LoadMap _this;

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

		public _LoadThienThach_c__Iterator5()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._thienthach___0 = (this._this.arrThienThach[(int)this.prInfo.Type] as ThienThach);
				this._current = new WaitForSeconds(this.prInfo.Delay);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._obj___0 = null;
				if (this._this.trnThienThach.childCount > 0)
				{
					for (int i = 0; i < this._this.trnThienThach.childCount; i++)
					{
						if (!this._this.trnThienThach.GetChild(i).gameObject.activeSelf && this._this.trnThienThach.GetChild(i).name == this._thienthach___0.name)
						{
							this._obj___0 = this._this.trnThienThach.GetChild(i).GetComponent<MonoBehaviour>();
							this._obj___0.transform.SetAsLastSibling();
							this._obj___0.transform.GetChild(0).gameObject.SetActive(true);
							this._obj___0.gameObject.SetActive(true);
							break;
						}
						if (this._this.trnThienThach.GetChild(i).gameObject.activeSelf)
						{
							break;
						}
					}
				}
				if (this._obj___0 == null)
				{
					this._obj___0 = UnityEngine.Object.Instantiate<ThienThach>(this._thienthach___0);
					this._obj___0.name = this._thienthach___0.name;
				}
				this._obj___0.SendMessage("SetBonus", TypeBullet.None, SendMessageOptions.DontRequireReceiver);
				this._obj___0.transform.SetParent(this._this.trnThienThach);
				for (int j = 0; j < this.bonus.Length; j++)
				{
					if (this.count == this.bonus[j])
					{
						this._obj___0.SendMessage("SetBonus", (TypeBullet)LoadMap.infoMap.Bonus[UnityEngine.Random.Range(2, LoadMap.infoMap.Bonus.Count)], SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
				LoadMap.isDone = true;
				this._obj___0.transform.localScale = Vector3.one;
				this._obj___0.transform.localPosition = LoadMap.GridToVector2((float)this.prInfo.dx, (float)this.prInfo.dy, -1f) + new Vector3((float)((LoadMap.infoMap.idThienThach != 1) ? ((LoadMap.infoMap.idThienThach != 2) ? (-12) : 12) : 0), 0f);
				this._obj___0.SendMessage("Init", this.prInfo, SendMessageOptions.DontRequireReceiver);
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

	private float[] disEnemy = new float[]
	{
		0.25f,
		0.25f,
		0.25f,
		0.35f,
		0.45f,
		0.5f
	};

	public MonoBehaviour[] arrEnemy;

	public MonoBehaviour[] arrThienThach;

	public MonoBehaviour[] arrBoss;

	public Sprite[] ArrSpriteEnemy1;

	private MonoBehaviour clsBoss;

	public Transform trnThienThach;

	private MapData data;

	internal static float distance = 0.8f;

	private static int width = 7;

	private static int height = 6;

	private Vector3[] Grid;

	private float disMove = 0.5f;

	internal static bool isDone;

	public static int countWave;

	public static int currentWave;

	public static int idMap;

	internal static int idBoss;

	internal static InfoMap infoMap;

	public Transform trnBg;

	private string pathLocal;

	public Material[] matsBg;

	public void ResetData()
	{
		if (!GamePlayManager.Instance)
		{
			return;
		}
		GamePlayManager.Instance.SetBullet(-GamePlayManager.Instance.bullet);
		GamePlayManager.Instance.typeBullet = TypeBullet.Bullet1;
		EventDispatcher.Instance.PostEvent(EventID.Disable, null);
		GamePlayManager.Instance.SetDefault();
		base.StopAllCoroutines();
	}

	internal void Init()
	{
		Material material = this.matsBg[UnityEngine.Random.Range(0, this.matsBg.Length)];
		IEnumerator enumerator = this.trnBg.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.GetComponent<MeshRenderer>().material = material;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.trnThienThach = GamePlay.Instance.trnThienThach;
		this.clsBoss = null;
		LoadMap.idBoss = -1;
		if (!PlayerPrefs.HasKey("map"))
		{
			PlayerPrefs.SetInt("map", 1);
		}
		LoadMap.idMap = GameManager.Instance.level;
		LoadMap.currentWave = (LoadMap.countWave = 1);
		LoadMap.isDone = false;
		this.Grid = new Vector3[LoadMap.width * LoadMap.height];
		this.Move(true);
		this.LoadDataMap();
		GamePlayManager.state = StateGame.PLAY;
		this.InitBonus();
	}

	private void InitBonus()
	{
		this.SetBonus(TypeBonus.Bullet1, TypeBullet.Bullet1);
		this.SetBonus(TypeBonus.Bullet2, TypeBullet.Bullet2);
		this.SetBonus(TypeBonus.Bullet3, TypeBullet.Bullet3);
		this.SetBonus(TypeBonus.Bullet5, TypeBullet.BulletLaser);
		this.SetBonus(TypeBonus.Bullet6, TypeBullet.Bullet4);
		this.SetBonus(TypeBonus.Bullet7, TypeBullet.Bullet5);
		this.SetBonus(TypeBonus.BulletAll, TypeBullet.BulletAll);
		this.SetBonus(TypeBonus.Upgrade, TypeBullet.Power);
		this.SetBonus(TypeBonus.Rocket, TypeBullet.None);
	}

	private void SetBonus(TypeBonus type, TypeBullet id)
	{
		if (id == TypeBullet.None)
		{
			GamePlayManager.Instance.SetRocket(PlayerPrefs.GetInt(type.ToString()));
		}
		else
		{
			for (int i = 0; i < PlayerPrefs.GetInt(type.ToString()); i++)
			{
				Vector3 pos = new Vector3(UnityEngine.Random.Range(-3.2f, 3.2f), UnityEngine.Random.Range(6.8f, 7.5f));
				EnemyManager.Instance.InstanceItem(new TypeItem[]
				{
					TypeItem.BonusBullet
				}).Init(pos, id);
			}
		}
		PlayerPrefs.SetInt(type.ToString(), 0);
	}

	private void Move(bool left = false)
	{
		base.transform.DOMoveX((!left) ? this.disMove : (-this.disMove), 3f, false).SetEase(Ease.OutQuad).OnComplete(delegate
		{
			this.Move(!left);
		});
	}

	internal static Vector3 GridToVector(float dx, float dy, float dis = -1f)
	{
		if (dis == -1f)
		{
			dis = LoadMap.distance;
		}
		return new Vector3(dx - (float)LoadMap.width / 2f, -dy) * dis;
	}

	internal static Vector3 GridToVector2(float dx, float dy, float dis = -1f)
	{
		if (dis == -1f)
		{
			dis = LoadMap.distance;
		}
		return new Vector3(dx - (float)(LoadMap.width / 2), dy * dis);
	}

	internal bool isWaveBoss()
	{
		return LoadMap.currentWave == LoadMap.countWave && LoadMap.idBoss != -1;
	}

	internal void CheckWave()
	{
		base.StopCoroutine(this.CheckTurn());
		base.StartCoroutine(this.CheckTurn());
	}

	internal List<GameObject> GetListEnemy()
	{
		List<GameObject> list = new List<GameObject>();
		Enemy[] componentsInChildren = base.transform.GetComponentsInChildren<Enemy>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].Health > 0f)
			{
				list.Add(componentsInChildren[i].gameObject);
			}
		}
		if (this.clsBoss)
		{
			list.Add(this.clsBoss.gameObject);
		}
		ThienThach[] componentsInChildren2 = this.trnThienThach.GetComponentsInChildren<ThienThach>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			if (componentsInChildren2[j].HP > 0f)
			{
				list.Add(componentsInChildren2[j].gameObject);
			}
		}
		return list;
	}

	private IEnumerator CheckTurn()
	{
		LoadMap._CheckTurn_c__Iterator0 _CheckTurn_c__Iterator = new LoadMap._CheckTurn_c__Iterator0();
		_CheckTurn_c__Iterator._this = this;
		return _CheckTurn_c__Iterator;
	}

	internal void NextMap()
	{
		if (LoadMap.currentWave < LoadMap.countWave)
		{
			return;
		}
		base.StopAllCoroutines();
		base.CancelInvoke();
		LoadMap.idMap++;
		LoadMap.currentWave = 1;
		this.LoadDataMap();
	}

	private void LoadWave()
	{
		if (LoadMap.currentWave > LoadMap.countWave)
		{
			if (!this.data.haveBoss)
			{
				GameManager.Instance.gamePlayManager.ChangeState(StateGame.NEXTLEVEL);
				return;
			}
			this.LoadBoss();
		}
		else
		{
			base.StartCoroutine(this.LoadMap1());
		}
	}

	private int[] InitBonus(int total)
	{
		int num = UnityEngine.Random.Range(LoadMap.infoMap.Bonus[0], LoadMap.infoMap.Bonus[1] + 1);
		int[] array = new int[num];
		for (int i = 0; i < num; i++)
		{
			int num2 = UnityEngine.Random.Range(0, total);
			array[i] = num2;
		}
		return array;
	}

	private IEnumerator LoadMap1()
	{
		LoadMap._LoadMap1_c__Iterator1 _LoadMap1_c__Iterator = new LoadMap._LoadMap1_c__Iterator1();
		_LoadMap1_c__Iterator._this = this;
		return _LoadMap1_c__Iterator;
	}

	private IEnumerator LoadEnemy(Enemy Enemy, string path, InfoEnemy prInfo, float speed, bool rotation)
	{
		LoadMap._LoadEnemy_c__Iterator2 _LoadEnemy_c__Iterator = new LoadMap._LoadEnemy_c__Iterator2();
		_LoadEnemy_c__Iterator.path = path;
		_LoadEnemy_c__Iterator.Enemy = Enemy;
		_LoadEnemy_c__Iterator.prInfo = prInfo;
		_LoadEnemy_c__Iterator.speed = speed;
		_LoadEnemy_c__Iterator.rotation = rotation;
		return _LoadEnemy_c__Iterator;
	}

	private void LoadBoss()
	{
		this.clsBoss = UnityEngine.Object.Instantiate<MonoBehaviour>(this.arrBoss[LoadMap.idBoss]);
		this.clsBoss.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
		this.clsBoss.name = this.arrBoss[LoadMap.idBoss].name;
		this.clsBoss.transform.SetParent(base.transform);
		this.clsBoss.transform.position = new Vector3(0f, 8f);
		this.clsBoss.transform.DOLocalMoveY(-5f, 3f, false).OnComplete(delegate
		{
			this.clsBoss.SendMessage("MoveDone", SendMessageOptions.DontRequireReceiver);
		});
	}

	private IEnumerator LoadEnemySpace(InfoEnemy prInfo, int count, int[] bonus)
	{
		LoadMap._LoadEnemySpace_c__Iterator3 _LoadEnemySpace_c__Iterator = new LoadMap._LoadEnemySpace_c__Iterator3();
		_LoadEnemySpace_c__Iterator.prInfo = prInfo;
		_LoadEnemySpace_c__Iterator.bonus = bonus;
		_LoadEnemySpace_c__Iterator.count = count;
		_LoadEnemySpace_c__Iterator._this = this;
		return _LoadEnemySpace_c__Iterator;
	}

	private IEnumerator LoadEnemy(InfoEnemy prInfo, int count, int[] bonus)
	{
		LoadMap._LoadEnemy_c__Iterator4 _LoadEnemy_c__Iterator = new LoadMap._LoadEnemy_c__Iterator4();
		_LoadEnemy_c__Iterator.prInfo = prInfo;
		_LoadEnemy_c__Iterator.bonus = bonus;
		_LoadEnemy_c__Iterator.count = count;
		_LoadEnemy_c__Iterator._this = this;
		return _LoadEnemy_c__Iterator;
	}

	private IEnumerator LoadThienThach(InfoEnemy prInfo, int[] bonus, int count)
	{
		LoadMap._LoadThienThach_c__Iterator5 _LoadThienThach_c__Iterator = new LoadMap._LoadThienThach_c__Iterator5();
		_LoadThienThach_c__Iterator.prInfo = prInfo;
		_LoadThienThach_c__Iterator.bonus = bonus;
		_LoadThienThach_c__Iterator.count = count;
		_LoadThienThach_c__Iterator._this = this;
		return _LoadThienThach_c__Iterator;
	}

	private Vector3[] MoveDontSort(int dx, int dy)
	{
		List<Vector3> list = new List<Vector3>();
		if ((float)dx <= (float)LoadMap.width / 2f)
		{
			list.Add(LoadMap.GridToVector(0f, (float)dy, -1f) - new Vector3(LoadMap.distance * 3f, 0f));
			list.Add(LoadMap.GridToVector((float)dx, (float)dy, -1f));
		}
		else
		{
			list.Add(LoadMap.GridToVector((float)(LoadMap.width - 1), (float)dy, -1f) + new Vector3(LoadMap.distance * 3f, 0f));
			list.Add(LoadMap.GridToVector((float)dx, (float)dy, -1f));
		}
		return list.ToArray();
	}

	private Vector3[] MovePath0(Vector3 posStart, InfoEnemy info)
	{
		List<Vector3> list = new List<Vector3>();
		posStart -= base.transform.position;
		Vector3 item = LoadMap.GridToVector((float)info.dx, (float)info.dy, -1f);
		list.Add(new Vector3(posStart.x * (float)info.Arrow, posStart.y));
		list.Add(new Vector3((float)(3 * info.Arrow), posStart.y));
		list.Add(new Vector3((float)(3 * info.Arrow), item.y));
		list.Add(item);
		return list.ToArray();
	}

	private Vector3[] MovePath1(Vector3 posStart, InfoEnemy info)
	{
		if (info.Arrow == -1)
		{
			posStart[0] = -posStart[0];
		}
		List<Vector3> list = new List<Vector3>();
		Vector3 vector = LoadMap.GridToVector((float)info.dx, (float)info.dy, LoadMap.distance);
		float num = (vector.x - posStart.x) / 10f;
		float num2 = (vector.y - posStart.y) / 10f;
		list.Add(posStart);
		list.Add(posStart + new Vector3(2f * num, -3f * num2));
		list.Add(posStart + new Vector3(5f * num, 0f));
		list.Add(posStart + new Vector3(7f * num, num2));
		list.Add(vector - new Vector3(num, 6f * num2));
		list.Add(vector - new Vector3(0.5f * num, 4f * num2));
		list.Add(vector);
		return list.ToArray();
	}

	private Vector3[] MovePath2(int dx, int dy, Vector3 posStart)
	{
		if (dx >= LoadMap.width / 2)
		{
			posStart[0] = -posStart[0];
		}
		List<Vector3> list = new List<Vector3>();
		Vector3 item = LoadMap.GridToVector((float)dx, (float)dy, LoadMap.distance);
		float num = (item[0] - posStart[0]) / 10f;
		float num2 = (item[1] - posStart[1]) / 10f;
		list.Add(posStart);
		list.Add(posStart + new Vector3(2f * num, -3f * num2));
		list.Add(posStart + new Vector3(5f * num, 0f));
		list.Add(posStart + new Vector3(7f * num, 3f * num2));
		list.Add(posStart + new Vector3(10f * num, 6f * num2));
		list.Add(posStart + new Vector3(10f * num, 8f * num2));
		list.Add(item);
		return list.ToArray();
	}

	private void LoadDataMap()
	{
		this.data = Resources.Load<MapData>("Maps/Map" + LoadMap.idMap);
		LoadMap.countWave = this.data.lsInfo.Count;
		LoadMap.idBoss = (int)((!this.data.haveBoss) ? ((TypeBoss)(-1)) : this.data.typeBoss);
		this.LoadWave();
	}
	public GameObject WinPanel;
    private void Update()
    {
		if(WinPanel.activeInHierarchy){
			ClearEnemys();
		}
			
		
    }
    public void ClearEnemys()
	{
		int objcol = this.transform.childCount;
		for (int i = 0; i < objcol; i++)
		{
			GameObject enem = this.transform.GetChild(i).gameObject;
			Destroy(enem);
		}

	}
}
