using EZ_Pooling;
using System;
using UnityEngine;

public class test_sandbox : MonoBehaviour
{
	public Transform[] prefabs;

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 70f, 100f, 30f), "Spawn prefab 1"))
		{
			EZ_PoolManager.Spawn(this.prefabs[0], UnityEngine.Random.insideUnitSphere * 4f, UnityEngine.Random.rotation);
		}
		if (GUI.Button(new Rect(10f, 100f, 100f, 30f), "Spawn prefab 2"))
		{
			EZ_PoolManager.Spawn(this.prefabs[1], UnityEngine.Random.insideUnitSphere * 4f, UnityEngine.Random.rotation);
		}
		if (GUI.Button(new Rect(10f, 130f, 100f, 30f), "Spawn prefab 3"))
		{
			EZ_PoolManager.Spawn(this.prefabs[2], UnityEngine.Random.insideUnitSphere * 4f, UnityEngine.Random.rotation);
		}
		if (GUI.Button(new Rect(10f, 160f, 100f, 30f), "Spawn prefab 4"))
		{
			EZ_PoolManager.Spawn(this.prefabs[3], UnityEngine.Random.insideUnitSphere * 4f, UnityEngine.Random.rotation);
		}
	}
}
