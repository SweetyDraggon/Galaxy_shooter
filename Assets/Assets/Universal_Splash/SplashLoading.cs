using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashLoading : MonoBehaviour
{
	public float loadingDuration = 2f;
	private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
		
    }

	private void Update()
	{
				if (timer < loadingDuration)
				{
					timer += Time.deltaTime;
				}
				else
				{
					timer = 0f;
					SceneManager.LoadScene(1);
				}


	}

}
