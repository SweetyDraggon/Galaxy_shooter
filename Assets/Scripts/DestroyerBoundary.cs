using EZ_Pooling;
using System;
using UnityEngine;

public class DestroyerBoundary : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		EZ_PoolManager.Despawn(other.transform);
	}
}
