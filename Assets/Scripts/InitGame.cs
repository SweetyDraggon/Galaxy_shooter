using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
	private void Awake()
	{
		this.Init();
	}

	private void Init()
	{
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<GameManager>();
		gameObject.name = "GameManager";
		SceneManager.LoadScene("Game");
	}
}
