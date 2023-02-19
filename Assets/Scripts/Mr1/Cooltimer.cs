using System;
using UnityEngine;

namespace Mr1
{
	public class Cooltimer : MonoBehaviour
	{
		public float cooltime;

		public Action onFinished;

		public static Cooltimer Set(Component component, float cooltime, Action onFinished)
		{
			Cooltimer cooltimer = component.GetComponent<Cooltimer>();
			if (cooltimer == null)
			{
				cooltimer = component.gameObject.AddComponent<Cooltimer>();
			}
			cooltimer.cooltime = cooltime;
			cooltimer.onFinished = onFinished;
			return cooltimer;
		}

		private void Update()
		{
			this.cooltime = Mathf.Max(0f, this.cooltime - Time.smoothDeltaTime);
			if (this.cooltime <= 0f)
			{
				if (this.onFinished != null)
				{
					this.onFinished();
				}
				UnityEngine.Object.Destroy(this);
			}
		}
	}
}
