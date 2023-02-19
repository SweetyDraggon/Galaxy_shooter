using System;
using UnityEngine;

namespace zZ17
{
	public static class EventDispatcherExtension
	{
		public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
		{
			EventDispatcher.Instance.RegisterListener(eventID, callback);
		}

		public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
		{
			EventDispatcher.Instance.PostEvent(eventID, param);
		}

		public static void PostEvent(this MonoBehaviour sender, EventID eventID)
		{
			EventDispatcher.Instance.PostEvent(eventID, null);
		}
	}
}
