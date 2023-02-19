using System;
using UnityEngine;

namespace EZ_Pooling
{
	[Serializable]
	public class EZ_PrefabPoolOption
	{
		public Transform prefabTransform;

		public int instancesToPreload = 1;

		public bool isPoolExpanded = true;

		public bool showDebugLog;

		public bool poolCanGrow;

		public bool cullDespawned;

		public int cullAbove = 10;

		public float cullDelay = 2f;

		public int cullAmount = 1;

		public bool enableHardLimit;

		public int hardLimit = 10;

		public bool recycle;
	}
}
