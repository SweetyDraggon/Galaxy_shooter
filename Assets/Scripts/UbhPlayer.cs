using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(UbhSpaceship))]
public class UbhPlayer : UbhMonoBehaviour
{
	private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UbhPlayer _this;

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
				this._this._Spaceship = this._this.GetComponent<UbhSpaceship>();
				this._this._Manager = UnityEngine.Object.FindObjectOfType<UbhManager>();
				this._this._BackgroundTransform = UnityEngine.Object.FindObjectOfType<UbhBackground>().transform;
				this._this._AudioShot = this._this.GetComponent<AudioSource>();
				break;
			case 1u:
				break;
			default:
				return false;
			}
			this._this.Shot();
			this._current = new WaitForSeconds(this._this._ShotDelay);
			if (!this._disposing)
			{
				this._PC = 1;
			}
			return true;
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

	public const string NAME_ENEMY_BULLET = "EnemyBullet";

	public const string NAME_ENEMY = "Enemy";

	private const string AXIS_HORIZONTAL = "Horizontal";

	private const string AXIS_VERTICAL = "Vertical";

	private readonly Vector2 VIEW_PORT_LEFT_BOTTOM = new Vector2(0f, 0f);

	private readonly Vector2 VIEW_PORT_RIGHT_TOP = new Vector2(1f, 1f);

	[SerializeField]
	private GameObject _BulletPrefab;

	[SerializeField]
	private float _ShotDelay;

	[SerializeField]
	private UbhUtil.AXIS _UseAxis;

	private UbhSpaceship _Spaceship;

	private UbhManager _Manager;

	private Transform _BackgroundTransform;

	private bool _IsTouch;

	private float _LastXpos;

	private float _LastYpos;

	private Vector2 _TempVector2 = Vector2.zero;

	private AudioSource _AudioShot;

	private IEnumerator Start()
	{
		UbhPlayer._Start_c__Iterator0 _Start_c__Iterator = new UbhPlayer._Start_c__Iterator0();
		_Start_c__Iterator._this = this;
		return _Start_c__Iterator;
	}

	private void Update()
	{
		if (UbhUtil.IsMobilePlatform())
		{
			this.TouchMove();
		}
		else
		{
			this.KeyMove();
		}
	}

	private void KeyMove()
	{
		this._TempVector2.x = UnityEngine.Input.GetAxisRaw("Horizontal");
		this._TempVector2.y = UnityEngine.Input.GetAxisRaw("Vertical");
		this.Move(this._TempVector2.normalized);
	}

	private void TouchMove()
	{
		float num = 0f;
		float num2 = 0f;
		if (Input.GetMouseButtonDown(0))
		{
			this._IsTouch = true;
			Vector3 vector = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			num = vector.x;
			num2 = vector.y;
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 vector2 = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			num = vector2.x;
			num2 = vector2.y;
			if (this._IsTouch)
			{
				this._TempVector2.x = (num - this._LastXpos) * 10f;
				this._TempVector2.y = (num2 - this._LastYpos) * 10f;
				this.Move(this._TempVector2.normalized);
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this._IsTouch = false;
		}
		this._LastXpos = num;
		this._LastYpos = num2;
	}

	private void Move(Vector2 direction)
	{
		Vector2 vector;
		Vector2 vector2;
		if (this._Manager != null && this._Manager._ScaleToFit)
		{
			vector = Camera.main.ViewportToWorldPoint(this.VIEW_PORT_LEFT_BOTTOM);
			vector2 = Camera.main.ViewportToWorldPoint(this.VIEW_PORT_RIGHT_TOP);
		}
		else
		{
			Vector2 a = this._BackgroundTransform.localScale;
			vector = a * -0.5f;
			vector2 = a * 0.5f;
		}
		Vector2 vector3 = base.transform.position;
		if (this._UseAxis == UbhUtil.AXIS.X_AND_Z)
		{
			vector3.y = base.transform.position.z;
		}
		vector3 += direction * this._Spaceship._Speed * Time.deltaTime;
		vector3.x = Mathf.Clamp(vector3.x, vector.x, vector2.x);
		vector3.y = Mathf.Clamp(vector3.y, vector.y, vector2.y);
		if (this._UseAxis == UbhUtil.AXIS.X_AND_Z)
		{
			base.transform.SetPosition(vector3.x, base.transform.position.y, vector3.y);
		}
		else
		{
			base.transform.position = vector3;
		}
	}

	private void Shot()
	{
		if (this._BulletPrefab != null)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.Instance.GetGameObject(this._BulletPrefab, base.transform.position, base.transform.rotation, false);
			if (this._AudioShot != null)
			{
				this._AudioShot.Play();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		this.HitCheck(c.transform);
	}

	private void OnTriggerEnter(Collider c)
	{
		this.HitCheck(c.transform);
	}

	private void HitCheck(Transform colTrans)
	{
		string name = colTrans.name;
		if (name.Contains("EnemyBullet"))
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.Instance.ReleaseGameObject(colTrans.parent.gameObject, false);
		}
		if (name.Contains("Enemy"))
		{
			UbhManager ubhManager = UnityEngine.Object.FindObjectOfType<UbhManager>();
			if (ubhManager != null)
			{
				ubhManager.GameOver();
			}
			this._Spaceship.Explosion();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
