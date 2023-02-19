using System;
using UnityEngine;
using zZ17;

public class BulletEnemy12 : MonoBehaviour
{
	public float MissileSpeed;

	private float turn = 2.5f;

	private float lastTurn;

	public bool isMove;

	public Rigidbody2D rocketRigidbody;

	private void Awake()
	{
		Action<object> callback = delegate(object x)
		{
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	public void Init(Vector3 posInit)
	{
		base.CancelInvoke("Explode");
		base.transform.position = posInit;
		base.Invoke("Explode", 3f);
		this.isMove = true;
	}

	private void FixedUpdate()
	{
		if (!this.isMove)
		{
			return;
		}
		Quaternion b = Quaternion.LookRotation(base.transform.position - PlayerController.Instance.transform.position, Vector3.forward);
		b.x = 0f;
		b.y = 0f;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * this.turn);
		this.rocketRigidbody.velocity = base.transform.up * this.MissileSpeed;
		if (this.turn < 40f)
		{
			this.lastTurn += Time.deltaTime * Time.deltaTime * 50f;
			this.turn += this.lastTurn;
		}
	}

	private void Explode()
	{
		this.isMove = false;
		base.CancelInvoke("Explode");
		base.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			this.Explode();
			other.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
		}
	}
}
