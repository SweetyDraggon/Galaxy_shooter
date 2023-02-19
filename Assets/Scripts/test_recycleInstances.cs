using EZ_Pooling;
using System;
using UnityEngine;

public class test_recycleInstances : MonoBehaviour
{
	public Transform prefab;

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 70f, 100f, 30f), "Spawn"))
		{
			EZ_PoolManager.Spawn(this.prefab, UnityEngine.Random.insideUnitSphere, UnityEngine.Random.rotation);
		}
	}
}
