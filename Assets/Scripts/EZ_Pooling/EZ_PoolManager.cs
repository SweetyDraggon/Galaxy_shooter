using System;
using System.Collections.Generic;
using UnityEngine;

namespace EZ_Pooling
{
	[AddComponentMenu("EZ Pooling/EZ_PoolManager")]
	public class EZ_PoolManager : MonoBehaviour
	{
		public List<EZ_PrefabPoolOption> prefabPoolOptions = new List<EZ_PrefabPoolOption>();

		public bool showDebugLog;

		public bool isRootExpanded = true;

		public bool autoAddMissingPrefabPool;

		public bool usePoolManager = true;

		private static Dictionary<string, EZ_PrefabPool> Pools = new Dictionary<string, EZ_PrefabPool>();

		private static Transform parentTransform;

		private static EZ_PoolManager instance;

		private List<EZ_PrefabPoolOption> itemsMarkedForDeletion = new List<EZ_PrefabPoolOption>();

		public static EZ_PoolManager Instance
		{
			get
			{
				return EZ_PoolManager.instance;
			}
		}

		private void Awake()
		{
			EZ_PoolManager.instance = this;
			EZ_PoolManager.parentTransform = base.transform;
			EZ_PoolManager.Pools.Clear();
			this.itemsMarkedForDeletion.Clear();
			if (!this.usePoolManager)
			{
				return;
			}
			for (int i = 0; i < this.prefabPoolOptions.Count; i++)
			{
				EZ_PrefabPoolOption eZ_PrefabPoolOption = this.prefabPoolOptions[i];
				Transform prefabTransform = eZ_PrefabPoolOption.prefabTransform;
				string name = prefabTransform.name;
				if (eZ_PrefabPoolOption.instancesToPreload <= 0 && !eZ_PrefabPoolOption.poolCanGrow)
				{
					this.itemsMarkedForDeletion.Add(eZ_PrefabPoolOption);
				}
				else if (prefabTransform == null)
				{
					UnityEngine.Debug.LogWarning("Item at index " + (i + 1) + " in the Pool has no prefab !");
				}
				else
				{
					if (EZ_PoolManager.Pools.ContainsKey(name))
					{
						UnityEngine.Debug.LogWarning("Duplicates found in the Pool : " + name);
					}
					List<Transform> list = new List<Transform>();
					for (int j = 0; j < eZ_PrefabPoolOption.instancesToPreload; j++)
					{
						Transform transform = UnityEngine.Object.Instantiate<Transform>(prefabTransform, Vector3.zero, prefabTransform.rotation);
						transform.name = name;
						transform.parent = EZ_PoolManager.parentTransform;
						transform.gameObject.SetActive(false);
						list.Add(transform);
					}
					EZ_PrefabPool eZ_PrefabPool = new EZ_PrefabPool(list);
					eZ_PrefabPool.showDebugLog = eZ_PrefabPoolOption.showDebugLog;
					eZ_PrefabPool.poolCanGrow = eZ_PrefabPoolOption.poolCanGrow;
					eZ_PrefabPool.parentTransform = EZ_PoolManager.parentTransform;
					eZ_PrefabPool.cullDespawned = eZ_PrefabPoolOption.cullDespawned;
					eZ_PrefabPool.cullAbove = eZ_PrefabPoolOption.cullAbove;
					eZ_PrefabPool.cullDelay = eZ_PrefabPoolOption.cullDelay;
					eZ_PrefabPool.cullAmount = eZ_PrefabPoolOption.cullAmount;
					eZ_PrefabPool.enableHardLimit = eZ_PrefabPoolOption.enableHardLimit;
					eZ_PrefabPool.hardLimit = eZ_PrefabPoolOption.hardLimit;
					eZ_PrefabPool.recycle = eZ_PrefabPoolOption.recycle;
					EZ_PoolManager.Pools.Add(name, eZ_PrefabPool);
				}
			}
			foreach (EZ_PrefabPoolOption current in this.itemsMarkedForDeletion)
			{
				this.prefabPoolOptions.Remove(current);
			}
			this.itemsMarkedForDeletion.Clear();
		}

		private void Update()
		{
			foreach (KeyValuePair<string, EZ_PrefabPool> current in EZ_PoolManager.Pools)
			{
				EZ_PrefabPool eZ_PrefabPool = EZ_PoolManager.Pools[current.Key];
				eZ_PrefabPool.Poll();
			}
		}

		private static void CreateMissingPrefabPool(Transform missingTrans, string name)
		{
			EZ_PrefabPool eZ_PrefabPool = new EZ_PrefabPool();
			eZ_PrefabPool.parentTransform = EZ_PoolManager.parentTransform;
			eZ_PrefabPool.poolCanGrow = true;
			EZ_PoolManager.Pools.Add(name, eZ_PrefabPool);
			EZ_PrefabPoolOption eZ_PrefabPoolOption = new EZ_PrefabPoolOption();
			eZ_PrefabPoolOption.prefabTransform = missingTrans;
			eZ_PrefabPoolOption.poolCanGrow = true;
			EZ_PoolManager.Instance.prefabPoolOptions.Add(eZ_PrefabPoolOption);
			if (EZ_PoolManager.Instance.showDebugLog)
			{
				UnityEngine.Debug.Log("EZ_PoolManager created Pool Item for missing item : " + name);
			}
		}

		public static Transform Spawn(Transform transToSpawn, Vector3 position, Quaternion rotation)
		{
			if (transToSpawn == null)
			{
				UnityEngine.Debug.LogWarning("No Transform passed to Spawn() !");
				return null;
			}
			if (!EZ_PoolManager.Instance.usePoolManager)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(transToSpawn, Vector3.zero, transToSpawn.rotation);
				transform.name = transToSpawn.name;
				transform.parent = EZ_PoolManager.parentTransform;
				return transform;
			}
			string name = transToSpawn.name;
			if (!EZ_PoolManager.Pools.ContainsKey(name))
			{
				if (!EZ_PoolManager.Instance.autoAddMissingPrefabPool)
				{
					UnityEngine.Debug.LogWarning(name + " passed to Spawn() is not in the Pool Manager.");
					return null;
				}
				EZ_PoolManager.CreateMissingPrefabPool(transToSpawn, name);
			}
			return EZ_PoolManager.Pools[name].Spawn(transToSpawn, position, rotation);
		}

		public static void Despawn(Transform transToDespawn)
		{
			if (transToDespawn == null)
			{
				UnityEngine.Debug.LogWarning("No Transform passed to Despawn() !");
				return;
			}
			if (!EZ_PoolManager.Instance.usePoolManager)
			{
				UnityEngine.Object.Destroy(transToDespawn.gameObject);
				return;
			}
			if (!transToDespawn.gameObject.activeInHierarchy)
			{
				return;
			}
			string name = transToDespawn.name;
			if (!EZ_PoolManager.Pools.ContainsKey(name))
			{
				UnityEngine.Debug.LogWarning(name + " passed to Despawn() is not in the Pool.");
				return;
			}
			EZ_PoolManager.Pools[name].Despawn(transToDespawn);
		}

		public static EZ_PrefabPool GetPool(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (!EZ_PoolManager.Pools.ContainsKey(name))
			{
				return null;
			}
			return EZ_PoolManager.Pools[name];
		}
	}
}
