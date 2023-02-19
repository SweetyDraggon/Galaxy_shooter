using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mr1
{
	public class WaypointManager : MonoBehaviour
	{
		private static WaypointManager _instance;

		private PathData _selected_k__BackingField;

		public static WaypointManager instance
		{
			get
			{
				if (WaypointManager._instance == null)
				{
					WaypointManager._instance = UnityEngine.Object.FindObjectOfType<WaypointManager>();
					WaypointManager._instance.Init();
				}
				return WaypointManager._instance;
			}
		}

		public PathData selected
		{
			get;
			set;
		}

		private void Init()
		{
			base.GetComponent<Collider>().enabled = false;
		}

		public PathData GetPathData(string pathName)
		{
			return Resources.Load<PathData>("Paths/" + pathName);
		}
	}
}
