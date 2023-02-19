using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPause : BackButton
{
	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			PerformBackAction();
		}
	}

}
