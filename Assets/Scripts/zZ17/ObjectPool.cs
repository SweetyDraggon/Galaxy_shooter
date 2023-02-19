using System;
using System.Collections.Generic;
using UnityEngine;

namespace zZ17
{
	public class ObjectPool
	{
		public const int createCount = 7;

		protected GameObject parent;

		protected GameObject prefab;

		protected List<GameObject> list;

		public ObjectPool(GameObject prefab, GameObject parent = null, int count = 7)
		{
			this.prefab = prefab;
			this.parent = parent;
			this.list = new List<GameObject>();
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = this.CreateObject();
				gameObject.SetActive(false);
				this.list.Add(gameObject);
			}
		}

		public GameObject GetObject()
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (!this.list[i].activeSelf)
				{
					this.list[i].SetActive(true);
					return this.list[i];
				}
			}
			return this.CreateObject();
		}

		public GameObject CreateObject()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
			if (this.parent != null)
			{
				gameObject.transform.parent = this.parent.transform;
			}
			this.list.Add(gameObject);
			return gameObject;
		}
	}
}
