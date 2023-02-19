using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UbhSimpleBullet : UbhMonoBehaviour
{
	public int _Power = 1;

	[SerializeField]
	private float _Speed = 10f;

	private void OnEnable()
	{
		base.rigidbody2D.velocity = base.transform.up.normalized * this._Speed;
	}
}
