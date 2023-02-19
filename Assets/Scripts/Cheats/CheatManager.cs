using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public void UnlockNextLevel()
	{
		PlayerPrefs.SetInt("max", PlayerPrefs.GetInt("max", 1) + 1);
	}

	public void UnlockAllLevels()
	{
		PlayerPrefs.SetInt("max", 46);
	}

	public void AddCoins(int num)
	{
		GameManager.Instance.ChangeCoin(num);
	}

}
