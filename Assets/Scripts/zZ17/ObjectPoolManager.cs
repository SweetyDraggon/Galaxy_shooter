using System;
using System.Collections.Generic;
using UnityEngine;

namespace zZ17
{
	public class ObjectPoolManager : MonoBehaviour
	{
		protected const string objectPoolManager = "ObjectPoolManager";

		private static ObjectPoolManager instance;

		protected Dictionary<GameObject, ObjectPool> objectPool;

		protected static ObjectPoolManager Instance
		{
			get
			{
				if (ObjectPoolManager.instance == null)
				{
					GameObject gameObject = new GameObject();
					ObjectPoolManager.instance = gameObject.AddComponent<ObjectPoolManager>();
					gameObject.name = "ObjectPoolManager";
				}
				return ObjectPoolManager.instance;
			}
		}

		protected virtual void Awake()
		{
			if (ObjectPoolManager.instance == null)
			{
				ObjectPoolManager.instance = this;
			}
			this.objectPool = new Dictionary<GameObject, ObjectPool>();
		}

		protected virtual void OnDestory()
		{
			ObjectPoolManager.instance = null;
		}

		public static void Init(GameObject gObj, int count = 7)
		{
			if (!ObjectPoolManager.Instance.objectPool.ContainsKey(gObj))
			{
				ObjectPoolManager.Instance.objectPool.Add(gObj, new ObjectPool(gObj, ObjectPoolManager.instance.gameObject, count));
			}
		}

		public static GameObject Spawn(GameObject gObj, Vector3 position, Quaternion rotation, int count = 7)
		{
			GameObject gameObject = ObjectPoolManager.Spawn(gObj, count);
			gameObject.transform.position = position;
			gameObject.transform.rotation = rotation;
			return gameObject;
		}

		public static GameObject Spawn(GameObject gObj, int count = 7)
		{
			ObjectPoolManager.Init(gObj, count);
			return ObjectPoolManager.Instance.objectPool[gObj].GetObject();
		}

		public static void Despawn(GameObject gObj)
		{
			if (gObj != null)
			{
				gObj.SetActive(false);
			}
		}

		public static void DelayDespawn(GameObject gObj, float time)
		{
		}
	}
}
