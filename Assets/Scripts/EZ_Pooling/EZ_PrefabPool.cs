using System;
using System.Collections.Generic;
using UnityEngine;

namespace EZ_Pooling
{
	public class EZ_PrefabPool
	{
		public bool showDebugLog;

		public bool poolCanGrow;

		public bool cullDespawned;

		public int cullAbove = 10;

		public float cullDelay = 2f;

		public int cullAmount = 1;

		public bool enableHardLimit;

		public int hardLimit = 10;

		public bool recycle;

		public Transform parentTransform;

		public List<Transform> spawnedList = new List<Transform>();

		public List<Transform> despawnedList = new List<Transform>();

		private float TimeOfLastCull;

		public EZ_PrefabPool()
		{
			this.spawnedList.Clear();
			this.despawnedList.Clear();
		}

		public EZ_PrefabPool(List<Transform> list)
		{
			this.spawnedList.Clear();
			this.despawnedList = list;
		}

		public Transform Spawn(Transform transToSpawn, Vector3 position, Quaternion rotation)
		{
			if (this.despawnedList.Count == 0)
			{
				if (!this.poolCanGrow)
				{
					if (this.recycle)
					{
						if (this.showDebugLog)
						{
							UnityEngine.Debug.Log(transToSpawn.name + " has been recycled. Despawning and Spawning Immediately.");
						}
						this.Despawn(this.spawnedList[0]);
						return this.Spawn(transToSpawn, position, rotation);
					}
					UnityEngine.Debug.LogWarning(transToSpawn.name + " has used up all the free preallocated instances. Please increase your Preload Amount.");
					return null;
				}
				else if (this.enableHardLimit && this.spawnedList.Count >= this.hardLimit)
				{
					if (this.recycle)
					{
						if (this.showDebugLog)
						{
							UnityEngine.Debug.Log(transToSpawn.name + " has been recycled. Despawning and Spawning Immediately.");
						}
						this.Despawn(this.spawnedList[0]);
						return this.Spawn(transToSpawn, position, rotation);
					}
					UnityEngine.Debug.LogWarning(transToSpawn.name + " has already reached its hard limit. Please increase your hard limit Qty.");
					return null;
				}
				else
				{
					Transform transform = UnityEngine.Object.Instantiate<Transform>(transToSpawn, Vector3.zero, transToSpawn.rotation);
					transform.name = transToSpawn.name;
					transform.parent = this.parentTransform;
					transform.gameObject.SetActive(false);
					this.despawnedList.Add(transform);
					if (this.showDebugLog)
					{
						UnityEngine.Debug.Log("EZ_PoolManager Instantiated an extra '" + transToSpawn.name);
					}
				}
			}
			Transform transform2 = this.despawnedList[0];
			if (transform2 == null)
			{
				UnityEngine.Debug.LogWarning("User cannot destroy a gameobject while in the despawnedList.");
				return null;
			}
			transform2.position = position;
			transform2.rotation = rotation;
			transform2.gameObject.SetActive(true);
			if (this.showDebugLog)
			{
				UnityEngine.Debug.Log("EZ_PoolManager spawned " + transToSpawn.name);
			}
			transform2.BroadcastMessage("OnSpawned", SendMessageOptions.DontRequireReceiver);
			this.despawnedList.Remove(transform2);
			this.spawnedList.Add(transform2);
			return transform2;
		}

		public void Despawn(Transform transToDespawn)
		{
			transToDespawn.BroadcastMessage("OnDespawned", SendMessageOptions.DontRequireReceiver);
			transToDespawn.parent = this.parentTransform;
			transToDespawn.gameObject.SetActive(false);
			if (this.showDebugLog)
			{
				UnityEngine.Debug.Log("EZ_PoolManager despawned " + transToDespawn.name);
			}
			this.spawnedList.Remove(transToDespawn);
			this.despawnedList.Add(transToDespawn);
		}

		public void Poll()
		{
			if (Time.time > this.TimeOfLastCull + this.cullDelay)
			{
				if (!this.cullDespawned || this.despawnedList.Count <= this.cullAbove)
				{
					return;
				}
				this.TimeOfLastCull = Time.time;
				for (int i = 0; i < this.cullAmount; i++)
				{
					if (this.despawnedList.Count == 0)
					{
						return;
					}
					if (this.showDebugLog)
					{
						UnityEngine.Debug.Log(this.despawnedList[0].name + " Culled!");
					}
					UnityEngine.Object.Destroy(this.despawnedList[0].gameObject);
					this.despawnedList.RemoveAt(0);
				}
			}
		}
	}
}
