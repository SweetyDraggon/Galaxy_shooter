using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class GamePlayManager : MonoBehaviour
{
	
	private sealed class _ShowWin_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GamePlayManager _this;

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

		public _ShowWin_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (LoadMap.idBoss != -1)
				{
					this._current = new WaitForSeconds(7f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
				}
				else
				{
					this._current = new WaitForSeconds(2f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
				}
				return true;
			case 1u:
				break;
			case 2u:
				break;
			default:
				return false;
			}
			GamePlayManager.state = StateGame.NEXTLEVEL;
			PlayerController.Instance.transform.DOMove(Vector3.zero, 0.5f, false).OnComplete(delegate
			{
				if (Application.platform == RuntimePlatform.Android)
				{
					//if (Advertisements.Instance.IsInterstitialAvailable())
					//{
					//	Advertisements.Instance.ShowInterstitial(delegate()
					//	{
					//		this._this.panelComplete.Show(0f);
					//	});
					//}
					//else
					//{
						this._this.panelComplete.Show(0f);
					//}
				}
				else
				{
					this._this.panelComplete.Show(0f);
				}
			});
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

		internal void __m__0()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				//if (Advertisements.Instance.IsInterstitialAvailable())
				//{
				//	Advertisements.Instance.ShowInterstitial(delegate()
				//	{
				//		this._this.panelComplete.Show(0f);
				//	});
				//}
				//else
				//{
					this._this.panelComplete.Show(0f);
				//}
			}
			else
			{
				this._this.panelComplete.Show(0f);
			}
		}

		internal void __m__1()
		{
			this._this.panelComplete.Show(0f);
		}
	}

	public GamePlay gamePlay;

	[SerializeField]
	public PanelPause panelPause;
	public PanelPause panel1, panel2;

	[SerializeField]
	private PanelComplete panelComplete;
	public static bool isPauseInfin= false;

	public PanelGameOver panelGameOver;
	public PanelGameOver panel3, panel4;
    public GameObject loading;
	public Text txtHealth;

	public Text txtRocket;

	public Text txtBullet;

	public Text txtMana;

	public Text txtScore;

	public Text txtMap;

	public int health;

	public int rocket;
    public static bool isPause;
	public int bullet;

	public int mana;

	public int score;

	public int map;

	public int maxBullet;

	public TypeBullet typeBullet;

	private const string TEXT_SCORE = "Score: ";

	private const string TEXT_MAP = "Map: ";

	public static StateGame state;

	public static GamePlayManager Instance;

	
	private void Awake()
	{
	

		GamePlayManager.Instance = this;
		this.bullet = 1;
		this.RegisterListener(EventID.Bullet, delegate(object param)
		{
			TypeBullet typeBullet = (TypeBullet)param;
			if (typeBullet == this.typeBullet || typeBullet == TypeBullet.BulletAll)
			{
				this.bullet++;
			}
			else if (typeBullet == TypeBullet.Power)
			{
				PlayerController.Instance.isMaxBullet = true;
			}
			if (this.isBullet(typeBullet))
			{
				this.typeBullet = typeBullet;
			}
			PlayerController.Instance.SetBullet();
			this.ShowText(this.txtBullet, ref this.bullet, 0, string.Empty);
		});
	}

	public bool isBullet(TypeBullet param)
	{
		UnityEngine.Debug.Log("Bullet Check = " + (param != TypeBullet.BulletAll && param != TypeBullet.Power && param != TypeBullet.None));
		return param != TypeBullet.BulletAll && param != TypeBullet.Power && param != TypeBullet.None;
	}

	private void OnEnable()
	{
		this.SetDefault();
		this.gamePlay.gameObject.SetActive(true);
		this.panelPause.Hide();

		UnityEngine.Debug.Log("Current Level = " + GameManager.Instance.level);
	}

	private void OnDisable()
	{
		if (this.gamePlay)
		{
			this.gamePlay.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
		{
			EventDispatcher.Instance.PostEvent(EventID.Bullet, TypeBullet.BulletLaser);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.W))
		{
			EventDispatcher.Instance.PostEvent(EventID.Bullet, TypeBullet.Bullet4);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.E))
		{
			EventDispatcher.Instance.PostEvent(EventID.Bullet, TypeBullet.Bullet5);
		}
		
    //    if (Input.GetKeyDown(KeyCode.Escape) && !loading.activeInHierarchy)
    //    {
    //        isPause = !isPause;
    //    }
        if (isPause)
        {
            this.ChangeState(StateGame.PAUSE);
        }
        else
        {
            this.panelPause.Hide();
            this.ChangeState(StateGame.PLAY);

        }
		Pausepaneldefinition();


	}
	public void Pausepaneldefinition()
	{
		if (isPauseInfin == false)
		{/*
			GameObject go = GameObject.Find("PanelPause");

			UnityEngine.Debug.Log(go);
			panelPause = go.GetComponent<PanelPause>();
*/
			panelPause = panel1;
			panelGameOver = panel3;
		}
		else
		if (isPauseInfin == true)
		{/*
			GameObject go = GameObject.Find("PanelPauseInfinity");
			UnityEngine.Debug.Log(go);
			panelPause = go.GetComponent<PanelPause>();*/

			panelPause = panel2;

			panelGameOver = panel4;
		}
	}

	//   private void OnApplicationFocus(bool focus)
	//   {
	//       if (!focus)
	//       {
	//           isPause = true;
	//       }
	//   }

	public void ClickPause(Button btn)
	{
		btn.Click(delegate
		{
            isPause = true;
			this.ChangeState(StateGame.PAUSE);
		});

	}

	public void ChangeState(StateGame param)
	{
		switch (param)
		{
		case StateGame.LOADING:
			this.gamePlay.loadingManager.loadMap.ResetData();
			this.panelPause.Hide();
			this.panelGameOver.Hide();
			this.panelComplete.Hide();
			break;
		case StateGame.PAUSE:
			if (Application.platform == RuntimePlatform.Android)
			{
				//if (Advertisements.Instance.IsInterstitialAvailable())
				//{
				//	Advertisements.Instance.ShowInterstitial(delegate()
				//	{
				//		this.panelPause.Show(0f);
				//	});
				//}
				//else
				//{
					this.panelPause.Show(0f);
				//}
			}
			else
			{
				this.panelPause.Show(0f);
			}
			break;
		case StateGame.NEXTLEVEL:
			if (GameManager.Instance.isEndless && LoadMap.idMap < GameManager.Instance.selectLevelManager.btnLevels.Length)
			{
				GameManager.Instance.loadManager.loadMap.NextMap();
			}
			else
			{
				base.StartCoroutine(this.ShowWin());
			}
			break;
		case StateGame.GAMEOVER:
			if (Application.platform == RuntimePlatform.Android)
			{
				//if (Advertisements.Instance.IsInterstitialAvailable())
				//{
				//	Advertisements.Instance.ShowInterstitial(delegate()
				//	{
				//		this.panelGameOver.Show(0f);
				//	});
				//}
				//else
				//{
					this.panelGameOver.Show(0f);
				//}
			}
			else
			{
				this.panelGameOver.Show(0f);
			}
			break;
		}
		if (param != StateGame.NEXTLEVEL)
		{
			GamePlayManager.state = param;
		}
	}

	private IEnumerator ShowWin()
	{
		GamePlayManager._ShowWin_c__Iterator0 _ShowWin_c__Iterator = new GamePlayManager._ShowWin_c__Iterator0();
		_ShowWin_c__Iterator._this = this;

		

		return _ShowWin_c__Iterator;
	}

	public void HideWhenWin()
	{
		this.panelPause.Hide();
		this.panelGameOver.Hide();
	}

	public void HideWhenWinChoose()
	{
		this.panelComplete.Hide();
	}

	public void SetDefault()
	{
		this.ShowText(this.txtHealth, ref this.health, -this.health + GameManager.Instance.config.dataGame.Health, string.Empty);
		this.ShowText(this.txtMana, ref this.mana, -this.mana + GameManager.Instance.config.dataGame.Mana, string.Empty);
		this.ShowText(this.txtRocket, ref this.rocket, -this.rocket + GameManager.Instance.config.dataGame.Rocket, string.Empty);
		this.ShowText(this.txtScore, ref this.score, -this.score, Localisation.GetString("Score:"));
		this.txtMap.enabled = !GameManager.Instance.isEndless;
		this.ShowText(this.txtMap, ref this.map, -this.map + GameManager.Instance.level, Localisation.GetString("Map:"));
		this.PostEvent(EventID.Bullet, TypeBullet.Bullet1);
		this.ShowText(this.txtBullet, ref this.bullet, -this.bullet + 1, string.Empty);
		PlayerController.Instance.OnInit();
	}

	public void SetHealth(int value)
	{
		if (value + this.health < 0)
		{
			value = 0;
		}
		this.ShowText(this.txtHealth, ref this.health, value, string.Empty);
	}

	public void SetBullet(int value = 1)
	{
		value = ((value + this.bullet >= 1) ? value : (value = -this.bullet + 1));
		this.ShowText(this.txtBullet, ref this.bullet, value, string.Empty);
	}

	public void SetMana(int value)
	{
		if (this.mana + value >= 25)
		{
			this.SetRocket(1);
			this.mana -= 25;
		}
		this.ShowText(this.txtMana, ref this.mana, value, string.Empty);
	}

	public void SetRocket(int value)
	{
		this.ShowText(this.txtRocket, ref this.rocket, value, string.Empty);
	}

	public void SetScore(int value)
	{
		this.ShowText(this.txtScore, ref this.score, value, Localisation.GetString("Score:"));
	}

	
}
