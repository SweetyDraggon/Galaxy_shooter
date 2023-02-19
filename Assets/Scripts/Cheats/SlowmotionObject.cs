using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowmotionObject : MonoBehaviour
{
	private void OnEnable()
	{
		Time.timeScale = 0.4f;
	//	Debug.Log("========== to 0.4f ===========Time Scale = " + Time.timeScale);
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
	//	Debug.Log("========== to 1f ===========Time Scale = " + Time.timeScale);
	}

	private void Update()
	{
	//	Debug.Log("Time Scale = " + Time.timeScale);
		if (GamePlayManager.state == StateGame.PLAY)
			Time.timeScale = 0.4f;
	}
}
