using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace zZ17
{
	public static class Extensions
	{
		private sealed class _HideBg_c__AnonStorey0
		{
			internal Transform trn;

			internal void __m__0()
			{
				this.trn.gameObject.SetActive(false);
				this.trn.localScale = Vector3.one;
			}
		}

		public static void Click(this Button target, TweenCallback callback)
		{
			SoundManager.Instance.PlayClickButton();
			target.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo).SetDelay(0f).OnComplete(callback).SetUpdate(true);
		}

		public static void PlayAudio(this AudioSource target, float volume = 1f)
		{
			target.volume = volume;
			if (PlayerPrefs.GetInt("sound") != 1)
			{
				target.Play();
			}
		}

		public static void HideBg(this Transform trn)
		{
			trn.DOScale(0f, 0.2f).SetDelay(0.1f).OnComplete(delegate
			{
				trn.gameObject.SetActive(false);
				trn.localScale = Vector3.one;
			});
		}

		public static GameObject InstanceObject(this Transform trnParent, GameObject prefab)
		{
			IEnumerator enumerator = trnParent.GetEnumerator();
			GameObject gameObject;
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					if (!transform.gameObject.activeSelf)
					{
						gameObject = transform.gameObject;
						gameObject.gameObject.SetActive(true);
						transform.SetAsLastSibling();
						return gameObject;
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
			gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, trnParent);
			gameObject.name = prefab.name;
			return gameObject;
		}

		public static Item InstanceItem(this Transform trnParent, Item prefab)
		{
			IEnumerator enumerator = trnParent.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					if (!transform.gameObject.activeSelf && transform.name == prefab.name)
					{
						transform.gameObject.SetActive(true);
						transform.localRotation = Quaternion.identity;
						transform.SetAsLastSibling();
						return transform.GetComponent<Item>();
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
			Item item = UnityEngine.Object.Instantiate<Item>(prefab, trnParent);
			item.name = prefab.name;
			return item;
		}

		public static GameObject InstanceObject(this Transform trnParent)
		{
			IEnumerator enumerator = trnParent.GetEnumerator();
			GameObject gameObject;
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					if (!transform.gameObject.activeSelf)
					{
						gameObject = transform.gameObject;
						gameObject.SetActive(true);
						transform.SetAsLastSibling();
						return gameObject;
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
			gameObject = new GameObject();
			gameObject.AddComponent<SpriteRenderer>();
			gameObject.transform.SetParent(trnParent);
			return gameObject;
		}

		public static void HideObject(this Transform trn)
		{
			trn.gameObject.SetActive(false);
			trn.SetAsFirstSibling();
		}

		public static void Hide(this MonoBehaviour script)
		{
			Time.timeScale = 1f;
			script.gameObject.SetActive(false);
		}

		public static void Show(this MonoBehaviour script, float timeScale = 1f)
		{
			Time.timeScale = timeScale;
			script.gameObject.SetActive(true);
		}

		public static void ShowCoin(this MonoBehaviour script, Text txt)
		{
			txt.text = GameManager.Instance.coin + string.Empty;
		}

		public static void ShowText(this MonoBehaviour script, Text txt, ref int param, int value, string text = "")
		{
			param += value;
			txt.text = text + param;
		}
	}
}
