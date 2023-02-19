using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mr1
{
	public class PathData : ScriptableObject
	{
		public bool bShowPath;

		[SerializeField]
		public List<Vector3> linePoints;

		[SerializeField]
		public List<Vector3> points;

		public Vector3 startPoint
		{
			get
			{
				return this.linePoints[0];
			}
		}

		public Vector3 endPoint
		{
			get
			{
				return this.linePoints[this.linePoints.Count - 1];
			}
		}

		public PathData()
		{
			this.linePoints = new List<Vector3>();
		}
	}
}
