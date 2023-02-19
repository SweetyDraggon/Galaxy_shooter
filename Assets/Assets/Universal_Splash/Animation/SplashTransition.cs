using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashTransition : MonoBehaviour
{
	public string scene = "<Insert scene name>";
	public float duration = 1.0f;
	public Color color = Color.black;

	private static GameObject canvasObject;
	private GameObject m_overlay;

	/// <summary>
	/// Performs the transition to the next scene.
	/// </summary>
	public void PerformTransition()
	{
		StartFade(scene, duration, color);
	}

	/// <summary>
	/// Starts the fade to the new level.
	/// </summary>
	/// <param name="level">The name of the level to load.</param>
	/// <param name="duration">The duration of the fade.</param>
	/// <param name="fadeColor">The color of the fade.</param>
	private void StartFade(string level, float duration, Color fadeColor)
	{
		StartCoroutine(RunFade(level, duration, fadeColor));
	}

	/// <summary>
	/// This coroutine performs the actual work of fading out of the current scene and into the new scene.
	/// </summary>
	/// <param name="level">The name of the level to load.</param>
	/// <param name="duration">The duration of the fade.</param>
	/// <param name="fadeColor">The color of the fade.</param>
	/// <returns></returns>
	private IEnumerator RunFade(string level, float duration, Color fadeColor)
	{
		var bgTex = new Texture2D(1, 1);
		bgTex.SetPixel(0, 0, fadeColor);
		bgTex.Apply();

		m_overlay = new GameObject();
		var image = m_overlay.AddComponent<Image>();
		var rect = new Rect(0, 0, bgTex.width, bgTex.height);
		var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
		image.material.mainTexture = bgTex;
		image.sprite = sprite;
		var newColor = image.color;
		image.color = newColor;
		image.canvasRenderer.SetAlpha(0.0f);

		m_overlay.transform.localScale = new Vector3(1, 1, 1);
		m_overlay.GetComponent<RectTransform>().sizeDelta = canvasObject.GetComponent<RectTransform>().sizeDelta;
		m_overlay.transform.SetParent(canvasObject.transform, false);
		m_overlay.transform.SetAsFirstSibling();

		var time = 0.0f;
		var halfDuration = duration / 2.0f;
		while (time < halfDuration)
		{
			time += Time.deltaTime;
			image.canvasRenderer.SetAlpha(Mathf.InverseLerp(0, 1, time / halfDuration));
			yield return new WaitForEndOfFrame();
		}

		image.canvasRenderer.SetAlpha(1.0f);

		Debug.Log("Scene Change");
		SceneManager.LoadScene(level);

		yield return new WaitForEndOfFrame();

		

		time = 0.0f;
		while (time < halfDuration)
		{
			time += Time.deltaTime;
			image.canvasRenderer.SetAlpha(Mathf.InverseLerp(1, 0, time / halfDuration));
			yield return new WaitForEndOfFrame();
		}

		image.canvasRenderer.SetAlpha(0.0f);
		yield return new WaitForEndOfFrame();

	//	Destroy(canvasObject);
	}

}
