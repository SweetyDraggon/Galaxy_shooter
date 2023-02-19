using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetLevel : MonoBehaviour
{
	public static ResetLevel Instance;
	public static bool hideMenu = false;
    // Start is called before the first frame update
    void Start()
    {
		if (Instance)
		{
			Destroy(this.gameObject);
			return;
		}

		DontDestroyOnLoad(this.gameObject);
		Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void RestartLevel()
	{
		StartCoroutine(Restart(GameManager.Instance.level));
	}
	public void RestartLevelInfinity()
	{
		StartCoroutine(RestartInfinity(GameManager.Instance.level));
	}

	public void NextLevel()
	{
		StartCoroutine(Restart(GameManager.Instance.level + 1));
	}

	public void LoadLevel(int levelToLoad)
	{
		StartCoroutine(Restart(levelToLoad));
	}

	private IEnumerator Restart(int loadLevel)
	{
		var currentLevel = loadLevel;
		hideMenu = true;
		Debug.Log("Before restart");


		SceneManager.LoadScene("Game");
		Debug.Log("After restart");
		
		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 1f;

		GameManager.Instance.level = currentLevel;
		GameManager.Instance.ChangeState(Scene.GAMEPLAY);
		GamePlayManager.isPause = false;
		GameManager.Instance.loadManager.loadMap.ResetData();
		hideMenu = false;

		//	GamePlayManager.Instance.SetHideMenu(false);

		/*Debug.Log("Something happening 1");
		
		GameManager.Instance.loadManager.loadMap.Init();
		PlayerController.Instance.OnInit();
		Debug.Log("Something happened");*/
		yield break;
	}
	private IEnumerator RestartInfinity(int loadLevel)
	{
		var currentLevel = loadLevel;
		hideMenu = true;
		Debug.Log("Before restart");


		SceneManager.LoadScene("Game");
		Debug.Log("After restart");

		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 1f;

			GameManager.Instance.level = 1;
			GameManager.Instance.isEndless = true;
			GameManager.Instance.ChangeState(Scene.GAMEPLAY);

		

	//	GamePlayManager.panelPause = GamePlayManager.panel1;
		GamePlayManager.isPause = false;
		GameManager.Instance.loadManager.loadMap.ResetData();
		GamePlayManager.isPauseInfin = true;
		hideMenu = false;

		//	GamePlayManager.Instance.SetHideMenu(false);

		/*Debug.Log("Something happening 1");
		
		GameManager.Instance.loadManager.loadMap.Init();
		PlayerController.Instance.OnInit();
		Debug.Log("Something happened");*/
		yield break;
	}

}
