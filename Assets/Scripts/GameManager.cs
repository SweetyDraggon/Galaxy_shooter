using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using zZ17;

public class GameManager : MonoBehaviour
{
	private sealed class _RunAction_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float time;

		internal UnityAction callback;

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

		public _RunAction_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
				case 0u:
					this._current = new WaitForSeconds(this.time);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this.callback();
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

	public HomeManager homeManager;

	public SelectLevelManager selectLevelManager;

	public GamePlayManager gamePlayManager;

	public ShopManager shopManager;

	public int coin;

	public int level;

	public bool sound;

	public bool music;

	public Sprite[] checks;

	public GameConfig config;

	public LoadingManager loadManager;
	public EnemyManager EnemysManager;
	public int childecol;
	public GameObject[] currentchild;


	public const string url = "https://play.google.com/store/apps/details?id=";

	private static GameManager s_instance;

	public bool isEndless;

	internal static Scene scene;

	
	public static GameManager Instance
	{
		get
		{
			if (GameManager.s_instance == null)
			{
				GameObject gameObject = new GameObject();
				GameManager.s_instance = gameObject.AddComponent<GameManager>();
				gameObject.name = "GameManager";
			}
			return GameManager.s_instance;
		}
		private set
		{
		}
	}

	public static bool HasInstance()
	{
		return GameManager.s_instance != null;
	}

	private void Awake()
	{
		if (GameManager.s_instance != null && GameManager.s_instance.GetInstanceID() != base.GetInstanceID())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			GameManager.s_instance = this;
		}
		Camera.main.aspect = 0.5625f;
        Application.targetFrameRate = 60;
	}

	private void Start()
	{
		this.Init();
	}

	private void Init()
	{
		if (!PlayerPrefs.HasKey("power"))
		{
			PlayerPrefs.SetInt("power", 1);
		}
		if (PlayerPrefs.HasKey("coin"))
		{
			this.coin = PlayerPrefs.GetInt("coin");
		}
		else
		{
			PlayerPrefs.SetInt("coin", 0);
		}
		this.sound = (PlayerPrefs.GetInt("sound") == 0);
		this.music = (PlayerPrefs.GetInt("music") == 0);
		AudioListener.volume = 1;
		SoundManager.Instance.SoundBulletPlayer.mute = !this.sound;
		SoundManager.Instance.SoundBG.mute = !this.music;
	
		this.ChangeCoin(0);
		this.ChangeState(Scene.HOME);
	}

	
	public void ChangeCoin(int param)
	{
		if (this.coin + param < 0)
		{
			return;
		}
		this.coin += param;
		PlayerPrefs.SetInt("coin", this.coin);
		this.PostEvent(EventID.Coin, null);
	}

	public void ChangeSound(Image check)
	{
		this.sound = !this.sound;
		check.sprite = this.checks[this.sound.GetHashCode()];
		PlayerPrefs.SetInt("sound", (!this.sound).GetHashCode());
		//AudioListener.volume = (float)this.sound.GetHashCode();
		SoundManager.Instance.ClickButton.mute = !this.sound;
		SoundManager.Instance.SoundBulletPlayer.mute = !this.sound;
		SoundManager.Instance.SoundPlayer.mute = !this.sound;
		if (!SoundManager.Instance.ClickButton.mute)
		{/*
			SoundManager.Instance.ClickButton.Play();
			SoundManager.Instance.SoundPlayer.Play();
			SoundManager.Instance.SoundBulletPlayer.Play();*/
		}
		
	}

	public void ChangeMusic(Image check)
	{
		this.music = !this.music;
		check.sprite = this.checks[this.music.GetHashCode()];
		PlayerPrefs.SetInt("music", (!this.music).GetHashCode());
		SoundManager.Instance.SoundBG.mute = !this.music;
		if (!SoundManager.Instance.SoundBG.mute)
		{
			SoundManager.Instance.SoundBG.Play();
		}

	}
	public void ClearEnemys() 
	{
		int childnum;
		childecol = EnemysManager.transform.childCount;
		for (int i = 0; i < childecol; i++)
		{

			EnemysManager.DestroysChilds();
		}
		/*
		for ( childnum = 0; childnum < childecol; childnum++)
		{
			UnityEngine.Debug.Log("work");
			currentchild[childnum] = EnemysManager.transform.GetChild(childnum).gameObject;
		}
		
		childnum = 0;
		while (childnum != childecol) {
			Destroy(currentchild[childnum]);
			childnum++;
		}*/
	}

	public void ChangeState(Scene param)
	{
	/*	if (GameManager.scene == Scene.GAMEPLAY && param != GameManager.scene)
		{
			
		}*/

		GameManager.scene = param;
		this.homeManager.Hide();
		this.selectLevelManager.Hide();
		this.gamePlayManager.Hide();
		this.shopManager.Hide();
		if (param != Scene.GAMEPLAY)
		{
			EventDispatcher.Instance.PostEventGamePlay();
		}
		switch (param)
		{
		case Scene.HOME:
			SoundManager.Instance.PlaySoundBg(param);
			this.homeManager.Show(1f);
			break;
		case Scene.SELECTLEVEL:
			SoundManager.Instance.PlaySoundBg(param);
			this.selectLevelManager.Show(1f);
			break;
		case Scene.GAMEPLAY:
			SoundManager.Instance.PlaySoundBg(param);
				
			this.loadManager.loadMap.ResetData();

			this.gamePlayManager.gamePlay.Show(1f);
			this.gamePlayManager.Show(1f);
			this.loadManager.Init();
			break;
		case Scene.SHOP:
			this.shopManager.Show(1f);
			break;
		}
	}

	private void Update()
	{
		if (Application.platform == RuntimePlatform.WindowsEditor && UnityEngine.Input.GetKeyDown(KeyCode.C))
		{
			this.ChangeCoin(50);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.PostEvent(EventID.Bullet, TypeBullet.BulletAll);
		}
		childecol = EnemysManager.transform.childCount;
	}

	public void CallCoroutine(float time = 1f, UnityAction callback = null)
	{
		base.StartCoroutine(this.RunAction(callback, time));
	}

	private IEnumerator RunAction(UnityAction callback, float time)
	{
		GameManager._RunAction_c__Iterator0 _RunAction_c__Iterator = new GameManager._RunAction_c__Iterator0();
		_RunAction_c__Iterator.time = time;
		_RunAction_c__Iterator.callback = callback;
		return _RunAction_c__Iterator;
	}
}
