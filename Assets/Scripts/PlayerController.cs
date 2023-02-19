using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private sealed class _SetSafe_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal PlayerController _this;

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

		public _SetSafe_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				PlayerController.isDie = false;
				this._this.goSafe.SetActive(true);
				this._this.collider.isTrigger = true;
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitUntil(() => !GamePlay.Instance.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._current = new WaitForSeconds(4f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				this._this.goSafe.SetActive(false);
				this._this.collider.isTrigger = false;
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

	private sealed class _CheckInit_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
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

		public _CheckInit_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => GamePlay.Instance);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitUntil(() => !GamePlay.Instance.loadingManager.gameObject.activeSelf);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				PlayerController.isDie = false;
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
			return GamePlay.Instance;
		}

		private static bool __m__1()
		{
			return !GamePlay.Instance.loadingManager.gameObject.activeSelf;
		}
	}

	private sealed class _CheckState_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
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

		public _CheckState_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitUntil(() => !PlayerController.isDie);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (!GameManager.Instance.loadManager.loadMap.trnThienThach.GetComponentInChildren<ThienThach>() && !GameManager.Instance.loadManager.loadMap.GetComponentInChildren<Enemy>())
				{
					GameManager.Instance.loadManager.loadMap.CheckWave();
				}
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
			return !PlayerController.isDie;
		}
	}

	private sealed class _PlayerDie_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal PlayerController _this;

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

		public _PlayerDie_c__Iterator3()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
			{
				SoundManager.Instance.PlaySoundPlayer(0);
				PlayerController.isDie = true;
				this._this.laser.SetActive(false);
				Renderer arg_62_0 = this._this.spriteRender;
				bool enabled = false;
				this._this.collider.enabled = enabled;
				arg_62_0.enabled = enabled;
				for (int i = 0; i < this._this.eff.Length; i++)
				{
					this._this.eff[i].Stop();
				}
				this._this.effDie.gameObject.SetActive(true);
				this._this.effDie.Play();
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			case 1u:
				GameManager.Instance.gamePlayManager.ChangeState(StateGame.GAMEOVER);
				this._this.StartCoroutine(this._this.CheckState());
				this._this.gameObject.SetActive(false);
				this._this.transform.position = new Vector3(this._this.dxDie, this._this.dyInit);
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

	public bool isMove;

	private bool? load;

	private float dMinX = -3.5f;

	private float dMinY = -5f;

	private float dMaxX = 3.5f;

	private float dMaxY = 5.5f;

	public Transform[] arrGun1;

	public static PlayerController Instance;

	public Bullet5 bullet5;

	internal SpriteRenderer spriteRender;

	public Button btnRocket;

	public Button btnPause;

	public GameObject goSafe;

	private PolygonCollider2D collider;

	internal static bool isDie;

	private float dxDie;

	private float dyInit = -8f;

	public Vector3 deltaPos;

	public GameObject goRocket;

	public Transform trContent;

	public Transform trRocket;

	public Color[] colorBullet;

	public ParticleSystem[] eff;

	public ParticleSystem effDie;

	public ParticleSystem effBullet;

	public bool isMaxBullet;

	public GameObject laser;

	private void Awake()
	{
		PlayerController.Instance = null;
		PlayerController.Instance = this;
		this.collider = base.GetComponent<PolygonCollider2D>();
		this.spriteRender = base.GetComponent<SpriteRenderer>();
		Renderer arg_39_0 = this.spriteRender;
		bool enabled = true;
		this.collider.enabled = enabled;
		arg_39_0.enabled = enabled;
		this.isMaxBullet = false;
	}

	private void Start()
	{
		this.OnInit();
		PlayerController.isDie = true;
		this.SetButtonRocket(false);
		this.btnPause.gameObject.SetActive(true);
		this.OnMouseUp();
	}

	private bool isOnScreen(Vector3 pos)
	{
		return pos.x > -3.6f && pos.x < 3.6f && pos.y > base.transform.position.y && pos.y < 6.8f;
	}

	private void FireBullet()
	{
		if (!PlayerController.isDie && GamePlayManager.state == StateGame.PLAY)
		{
			BulletManager.Instance.AddBullet();
		}
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.SetSafe());
	}

	internal IEnumerator SetSafe()
	{
		PlayerController._SetSafe_c__Iterator0 _SetSafe_c__Iterator = new PlayerController._SetSafe_c__Iterator0();
		_SetSafe_c__Iterator._this = this;
		return _SetSafe_c__Iterator;
	}

	private void CancelMaxBullet()
	{
		this.isMaxBullet = false;
	}

	internal void SetBullet()
	{
		if (this.isMaxBullet)
		{
			base.CancelInvoke("CancelMaxBullet");
			base.Invoke("CancelMaxBullet", 5f);
		}
	//	else
	//	{
	//		
	//	}
		base.CancelInvoke("FireBullet");
		this.SetBullet2();
	}

	private void SetBullet2()
	{
		base.CancelInvoke("FireBullet");
		if (GamePlayManager.Instance.typeBullet == TypeBullet.BulletLaser)
		{
			this.laser.gameObject.SetActive(true);
			this.bullet5.gameObject.SetActive(false);
		}
		else if (GamePlayManager.Instance.typeBullet == TypeBullet.Bullet5)
		{
			this.laser.gameObject.SetActive(false);
			this.bullet5.gameObject.SetActive(true);
		}
		else
		{
			this.laser.gameObject.SetActive(false);
			this.bullet5.gameObject.SetActive(false);
			float repeatRate = 0.4f;
			switch (GamePlayManager.Instance.typeBullet)
			{
			case TypeBullet.Bullet1:
				repeatRate = 0.15f;
				break;
			case TypeBullet.Bullet2:
				repeatRate = 0.22f;
				break;
			case TypeBullet.Bullet3:
				repeatRate = 0.08f;
				break;
			case TypeBullet.Bullet4:
				repeatRate = 0.2f;
				break;
			}
			base.InvokeRepeating("FireBullet", 0f, repeatRate);
		}
	}

	private void Update()
	{
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
#if !UNITY_EDITOR
        if (UnityEngine.Input.touchCount > 0)
		{
			if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Vector2 point = Camera.main.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
				this.deltaPos = new Vector3(base.transform.position[0] - point[0], base.transform.position[1] - point[1]);
				Collider2D collider2D = Physics2D.OverlapPoint(point);
				if (collider2D && collider2D.transform == base.transform)
				{
					this.isMove = true;
				}
			}
			else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
				position = new Vector3(this.deltaPos[0] + position[0], this.deltaPos[1] + position[1]);
				position = new Vector3((position[0] >= this.dMinX) ? ((position[0] <= this.dMaxX) ? position[0] : this.dMaxX) : this.dMinX, (position[1] >= this.dMinY) ? ((position[1] <= this.dMaxY) ? position[1] : this.dMaxY) : this.dMinY);
				base.transform.position = position;
			}
			else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				this.isMove = false;
			}
		}
#endif
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.deltaPos = new Vector3(base.transform.position[0] - point[0], base.transform.position[1] - point[1]);
            Collider2D collider2D = Physics2D.OverlapPoint(point);
            if (collider2D && collider2D.transform == base.transform)
            {
                this.isMove = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = new Vector3(this.deltaPos[0] + position[0], this.deltaPos[1] + position[1]);
            position = new Vector3((position[0] >= this.dMinX) ? ((position[0] <= this.dMaxX) ? position[0] : this.dMaxX) : this.dMinX, (position[1] >= this.dMinY) ? ((position[1] <= this.dMaxY) ? position[1] : this.dMaxY) : this.dMinY);
            base.transform.position = position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.isMove = false;
        }
#endif
    }

    private void OnMouseDown()
	{
		if (GamePlayManager.state != StateGame.PLAY)
		{
			MonoBehaviour.print(GamePlayManager.state);
			return;
		}
		Vector2 b = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		if (Vector2.Distance(this.trRocket.position, b) <= 0.5f)
		{
			return;
		}
		if (Vector2.Distance(this.btnPause.transform.position, b) <= 0.5f)
		{
			return;
		}
		DOTween.Kill(base.transform, false);
		this.isMove = true;
		Time.timeScale = 1f;
		this.SetButtonRocket(false);
	}

	private void OnMouseUp()
	{
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
		this.isMove = false;
		Time.timeScale = 0.4f;
		this.SetButtonRocket(true);
	}

	private void SetButtonRocket(bool isShow = true)
	{
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
		this.btnRocket.gameObject.SetActive(isShow && !PlayerController.isDie);
		this.btnRocket.interactable = (GamePlay.Instance.gamePlayManager.rocket > 0);
		this.btnPause.gameObject.SetActive(isShow);
		this.btnRocket.transform.position = this.trRocket.position;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.transform.tag == "Enemy")
		{
			coll.transform.parent.SendMessage("OnHit", 50, SendMessageOptions.DontRequireReceiver);
			this.OnHit();
		}
	}

	public void ClickRocket()
	{
		if (GamePlayManager.Instance.rocket > 0)
		{
			GamePlayManager.Instance.SetRocket(-1);
			Rocket rocket = this.InstanceRocket();
			rocket.Init();
		}
	}

	internal Rocket InstanceRocket()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goRocket);
		gameObject.transform.position = base.transform.position;
		gameObject.transform.SetParent(this.trContent);
		gameObject.transform.localScale = Vector3.one;
		return gameObject.GetComponent<Rocket>();
	}

	internal void OnInit()
	{
		GameManager.Instance.gamePlayManager.ChangeState(StateGame.PLAY);
		Renderer arg_25_0 = this.spriteRender;
		bool enabled = true;
		this.collider.enabled = enabled;
		arg_25_0.enabled = enabled;
		for (int i = 0; i < this.eff.Length; i++)
		{
			this.eff[i].Play();
		}
		this.effDie.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		base.transform.position = new Vector3(0f, -8f);
		base.transform.DOMoveY(-4f, 2f, false);
		this.CheckState();
		this.SetBullet2();
	}

	private IEnumerator CheckInit()
	{
		return new PlayerController._CheckInit_c__Iterator1();
	}

	private IEnumerator CheckState()
	{
		return new PlayerController._CheckState_c__Iterator2();
	}

	private void OnHit()
	{
		if (PlayerController.isDie || this.goSafe.activeSelf)
		{
			return;
		}
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
		GameManager.Instance.gamePlayManager.SetHealth(-1);
		Time.timeScale = 1f;
		this.dxDie = base.transform.position[0];
		this.btnRocket.gameObject.SetActive(false);
		PlayerController.isDie = true;
		base.StartCoroutine(this.PlayerDie());
	}

	private IEnumerator PlayerDie()
	{
		PlayerController._PlayerDie_c__Iterator3 _PlayerDie_c__Iterator = new PlayerController._PlayerDie_c__Iterator3();
		_PlayerDie_c__Iterator._this = this;
		return _PlayerDie_c__Iterator;
	}
}
