using System;
using UnityEngine;

public class basic_object : MonoBehaviour
{
	private void OnSpawned()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	private void OnDespawned()
	{
	}
}
