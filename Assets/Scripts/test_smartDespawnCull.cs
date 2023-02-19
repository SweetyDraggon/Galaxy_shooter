using EZ_Pooling;
using System;
using UnityEngine;

public class test_smartDespawnCull : MonoBehaviour
{
	public Transform prefab;

	private void Start()
	{
		base.InvokeRepeating("Auto", 0f, 0.8f);
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 70f, 200f, 30f), "Spawn 20 Prefabs"))
		{
			for (int i = 0; i < 20; i++)
			{
				EZ_PoolManager.Spawn(this.prefab, UnityEngine.Random.insideUnitSphere * 4f, UnityEngine.Random.rotation);
			}
		}
	}

	private void Auto()
	{
		EZ_PoolManager.Spawn(this.prefab, UnityEngine.Random.insideUnitSphere, UnityEngine.Random.rotation);
	}
}
