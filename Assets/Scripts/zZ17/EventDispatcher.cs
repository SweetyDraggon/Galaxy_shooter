using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace zZ17
{
	public class EventDispatcher : MonoBehaviour
	{
		private static EventDispatcher s_instance;

		private Dictionary<EventID, Action<object>> _listeners = new Dictionary<EventID, Action<object>>();

		private List<Action> _listenerGamePlay = new List<Action>();

		private static Action<Action> __f__am_cache0;

		public static EventDispatcher Instance
		{
			get
			{
				if (EventDispatcher.s_instance == null)
				{
					GameObject gameObject = new GameObject();
					EventDispatcher.s_instance = gameObject.AddComponent<EventDispatcher>();
					gameObject.name = "Singleton - EventDispatcher";
				}
				return EventDispatcher.s_instance;
			}
			private set
			{
			}
		}

		public static bool HasInstance()
		{
			return EventDispatcher.s_instance != null;
		}

		private void Awake()
		{
			if (EventDispatcher.s_instance != null && EventDispatcher.s_instance.GetInstanceID() != base.GetInstanceID())
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				EventDispatcher.s_instance = this;
			}
		}

		private void OnDestroy()
		{
			if (EventDispatcher.s_instance == this)
			{
				this.ClearAllListener();
				EventDispatcher.s_instance = null;
			}
		}

		public void RegisterListener(EventID eventID, Action<object> callback)
		{
			if (this._listeners.ContainsKey(eventID))
			{
				Dictionary<EventID, Action<object>> listeners;
				(listeners = this._listeners)[eventID] = (Action<object>)Delegate.Combine(listeners[eventID], callback);
			}
			else
			{
				this._listeners.Add(eventID, null);
				Dictionary<EventID, Action<object>> listeners;
				(listeners = this._listeners)[eventID] = (Action<object>)Delegate.Combine(listeners[eventID], callback);
			}
		}

		public void PostEvent(EventID eventID, object param = null)
		{
			if (!this._listeners.ContainsKey(eventID))
			{
				return;
			}
			Action<object> action = this._listeners[eventID];
			if (action != null)
			{
				action(param);
			}
			else
			{
				this._listeners.Remove(eventID);
			}
		}

		public void RemoveListener(EventID eventID, Action<object> callback)
		{
			if (this._listeners.ContainsKey(eventID))
			{
				Dictionary<EventID, Action<object>> listeners;
				(listeners = this._listeners)[eventID] = (Action<object>)Delegate.Remove(listeners[eventID], callback);
			}
		}

		public void ClearAllListener()
		{
			this._listeners.Clear();
		}

		public void RegisterGamePlay(Action callback)
		{
			this._listenerGamePlay.Add(callback);
		}

		public void PostEventGamePlay()
		{
			this._listenerGamePlay.ForEach(delegate(Action x)
			{
				x();
			});
		}
	}
}
