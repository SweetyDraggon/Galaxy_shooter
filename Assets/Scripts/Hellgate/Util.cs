using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Hellgate
{
	public class Util
	{
		public const string Android = "android";

		public const string iOS = "ios";

		public const string PC = "pc";

		public static GameObject GetChildObject(GameObject gObj, string strName)
		{
			Transform[] componentsInChildren = gObj.GetComponentsInChildren<Transform>(true);
			GameObject result = null;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].name == strName)
				{
					result = componentsInChildren[i].gameObject;
					break;
				}
			}
			return result;
		}

		public static GameObject FindChildObject(GameObject gObj, string strName)
		{
			Transform[] componentsInChildren = gObj.GetComponentsInChildren<Transform>(true);
			GameObject result = null;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].name == strName)
				{
					result = componentsInChildren[i].gameObject;
					break;
				}
			}
			return result;
		}

		public static T FindChildObject<T>(GameObject gObj, string strName)
		{
			GameObject gameObject = Util.FindChildObject(gObj, strName);
			if (gameObject != null)
			{
				return gameObject.GetComponent<T>();
			}
			return default(T);
		}

		public static T GetListObject<T>(List<object> list)
		{
			List<object>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is T)
				{
					return (T)((object)enumerator.Current);
				}
			}
			return default(T);
		}

		public static List<T> GetListObjects<T>(List<object> list)
		{
			List<T> list2 = new List<T>();
			List<object>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is T)
				{
					list2.Add((T)((object)enumerator.Current));
				}
			}
			return list2;
		}

		public static Sprite FindSprite(List<Sprite> list, string strName)
		{
			List<Sprite>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == strName)
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		public static TextAsset FindTextAsset(List<TextAsset> list, string strName)
		{
			List<TextAsset>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == strName)
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		public static GameObject FindGameObject(List<GameObject> list, string strName)
		{
			List<GameObject>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == strName)
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		public static UnityEngine.Object FindObject(List<UnityEngine.Object> list, string strName)
		{
			List<UnityEngine.Object>.Enumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.name == strName)
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		public static bool ToBoolean(string str)
		{
			string a = (str ?? string.Empty).Trim();
			return !string.Equals(a, "false", StringComparison.OrdinalIgnoreCase) && (string.Equals(a, "true", StringComparison.OrdinalIgnoreCase) || a != "0");
		}

		public static string ConvertCamelToUnderscore(string input)
		{
			return Regex.Replace(input, "(?x)( [A-Z][a-z,0-9]+ | [A-Z]+(?![a-z]) )", "_$0").ToLower();
		}

		public static List<T> GetDistinctValues<T>(List<T> list)
		{
			List<T> list2 = new List<T>();
			for (int i = 0; i < list.Count; i++)
			{
				if (!list2.Contains(list[i]))
				{
					list2.Add(list[i]);
				}
			}
			return list2;
		}

		public static T[] GetDistinctValues<T>(T[] array)
		{
			return Util.GetDistinctValues<T>(new List<T>(array)).ToArray();
		}

		public static void Merge<K, V>(Dictionary<K, V> dic, Dictionary<K, V> mergeDic)
		{
			if (dic == null)
			{
				dic = mergeDic;
				return;
			}
			if (dic == null)
			{
				return;
			}
			foreach (KeyValuePair<K, V> current in mergeDic)
			{
				if (!dic.ContainsKey(current.Key))
				{
					dic.Add(current.Key, current.Value);
				}
			}
		}

		public static void Merge<K, V>(List<Dictionary<K, V>> list, List<Dictionary<K, V>> mergeList)
		{
			if (list == null)
			{
				list = mergeList;
				return;
			}
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (i > mergeList.Count)
				{
					Util.Merge<K, V>(list[i], mergeList[i]);
				}
			}
		}

		public static void Merge(IDictionary iDic, IDictionary mergeIDic)
		{
			if (iDic == null)
			{
				iDic = mergeIDic;
				return;
			}
			if (iDic == null)
			{
				return;
			}
			IEnumerator enumerator = mergeIDic.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.Current;
					if (!iDic.Contains(current))
					{
						iDic.Add(current, mergeIDic[current]);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public static void Merge(IList iList, IList mergeIList)
		{
			if (iList == null)
			{
				iList = mergeIList;
				return;
			}
			if (iList == null)
			{
				return;
			}
			for (int i = 0; i < iList.Count; i++)
			{
				if (i < mergeIList.Count)
				{
					Util.Merge((IDictionary)iList[i], (IDictionary)mergeIList[i]);
				}
			}
		}

		public static List<object> GetValue(IList iList, string keyName, List<object> list = null)
		{
			if (list == null)
			{
				list = new List<object>();
			}
			IEnumerator enumerator = iList.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Dictionary<string, object> iDic = (Dictionary<string, object>)enumerator.Current;
					Util.GetValue(iDic, keyName, list);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return list;
		}

		public static List<object> GetValue(IDictionary iDic, string keyName, List<object> list = null)
		{
			if (list == null)
			{
				list = new List<object>();
			}
			IEnumerator enumerator = iDic.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.Current;
					IList iList;
					IDictionary dictionary;
					if (current.ToString() == keyName)
					{
						if (!list.Contains(iDic[current]))
						{
							list.Add(iDic[current]);
						}
					}
					else if ((iList = (iDic[current] as IList)) != null)
					{
						Util.GetValue(iList, keyName, list);
					}
					else if ((dictionary = (iDic[current] as IDictionary)) != null)
					{
						Util.GetValue(dictionary as Dictionary<string, object>, keyName, list);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return list;
		}

		public static bool IsInteger(Type type)
		{
			return type == typeof(sbyte) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(byte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong);
		}

		public static bool IsFloat(Type type)
		{
			return type == typeof(float) | type == typeof(double) | type == typeof(decimal);
		}

		public static bool IsNumeric(Type type)
		{
			return type == typeof(byte) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(decimal) || type == typeof(double) || type == typeof(float);
		}

		public static bool IsText(Type type)
		{
			return type == typeof(string) || type == typeof(char);
		}

		public static bool IsValueType(Type type)
		{
			return Util.IsText(type) || Util.IsNumeric(type);
		}

		public static bool IsArray(Type type)
		{
			return type.IsArray || typeof(IList).IsAssignableFrom(type);
		}

		public static object TextureConvertSprite(object obj)
		{
			if (obj is Texture2D)
			{
				Texture2D texture2D = obj as Texture2D;
				Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), Vector2.zero);
				sprite.name = texture2D.name;
				obj = sprite;
			}
			return obj;
		}

		public static void DestroyAllChildOfObject(GameObject gObj)
		{
			Transform[] componentsInChildren = gObj.GetComponentsInChildren<Transform>();
			for (int i = 1; i < componentsInChildren.Length; i++)
			{
				UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
			}
		}

		public static string GetDevice()
		{
			string result = string.Empty;
			if (Application.platform == RuntimePlatform.Android)
			{
				result = "android";
			}
			return result;
		}
	}
}
