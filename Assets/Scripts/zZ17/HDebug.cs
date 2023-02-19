using System;
using System.Collections.Generic;
using UnityEngine;

namespace zZ17
{
	public class HDebug : MonoBehaviour
	{
		private class LogData
		{
			public string condition;

			public string stackTrace;

			public LogType type;
		}

		[Range(0.3f, 1f), SerializeField]
		private float height = 0.3f;

		[Range(0.3f, 1f), SerializeField]
		private float width = 1f;

		[SerializeField]
		private int margin = 10;

		[SerializeField]
		private int fontSize = 14;

		[SerializeField]
		private float backgroundAlpha = 0.5f;

		[SerializeField]
		private Color backgroundColor = Color.black;

		[SerializeField]
		private Color textColor = Color.white;

		[SerializeField]
		private int screenMaxLog = 7;

		private static Queue<HDebug.LogData> data;

		private GUIStyle container;

		private GUIStyle text;

		protected void Awake()
		{
			Texture2D texture2D = new Texture2D(1, 1);
			this.backgroundColor.a = this.backgroundAlpha;
			texture2D.SetPixel(0, 0, this.backgroundColor);
			texture2D.Apply();
			this.container = new GUIStyle();
			this.container.normal.background = texture2D;
			this.container.wordWrap = false;
			this.container.padding = new RectOffset(5, 5, 5, 5);
			this.container.fontSize = this.fontSize;
			this.text = new GUIStyle();
			this.text.normal.textColor = this.textColor;
			HDebug.data = new Queue<HDebug.LogData>();
		}

		protected void Update()
		{
			while (this.screenMaxLog < HDebug.data.Count)
			{
				HDebug.data.Dequeue();
			}
		}

		protected void OnEnable()
		{
			Application.logMessageReceived += new Application.LogCallback(this.HandlelogMessageReceived);
		}

		protected void OnDisable()
		{
			Application.logMessageReceived -= new Application.LogCallback(this.HandlelogMessageReceived);
		}

		protected void OnGUI()
		{
			if (HDebug.data.Count <= 0)
			{
				return;
			}
			float num = (float)Screen.width * this.width - (float)(this.margin * 2);
			float num2 = (float)Screen.height * this.height - (float)(this.margin * 2);
			GUILayout.BeginArea(new Rect((float)this.margin, (float)this.margin, num, num2), this.container);
			List<HDebug.LogData> list = new List<HDebug.LogData>(HDebug.data);
			int i = list.Count - 1;
			while (i >= 0)
			{
				switch (list[i].type)
				{
				case LogType.Error:
				case LogType.Assert:
				case LogType.Exception:
					this.text.normal.textColor = Color.red;
					break;
				case LogType.Warning:
					this.text.normal.textColor = Color.yellow;
					break;
				case LogType.Log:
					goto IL_DC;
				default:
					goto IL_DC;
				}
				IL_F6:
				GUILayout.Label(list[i].condition, this.text, new GUILayoutOption[0]);
				GUILayout.Label(list[i].stackTrace, this.text, new GUILayoutOption[0]);
				i--;
				continue;
				IL_DC:
				this.text.normal.textColor = Color.white;
				goto IL_F6;
			}
			GUILayout.EndArea();
		}

		protected void HandlelogMessageReceived(string condition, string stackTrace, LogType type)
		{
			HDebug.LogData logData = new HDebug.LogData();
			logData.condition = condition;
			logData.stackTrace = stackTrace;
			logData.type = type;
			HDebug.data.Enqueue(logData);
		}

		public static void Log(object message)
		{
			if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.Log(message);
			}
		}

		public static void LogWarning(object message)
		{
			if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.LogWarning(message);
			}
		}

		public static void LogError(object message)
		{
			if (Debug.isDebugBuild)
			{
				UnityEngine.Debug.LogError(message);
			}
		}
	}
}
