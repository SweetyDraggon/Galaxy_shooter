using System;
using UnityEngine;

namespace Mr1
{
	public class MouseClickDemo : MonoBehaviour
	{
		public Transform target;

		public string pathName;

		private void OnMouseUp()
		{
			Vector3 v = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			v.z = base.transform.position.z;
			this.target.FollowPathToPoint(this.pathName, v, 10f);
		}
	}
}
