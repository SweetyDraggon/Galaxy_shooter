using System;
using UnityEngine;
using UnityEngine.UI;

public class UbhDebugInfo : UbhMonoBehaviour
{
	private const float INTERVAL_SEC = 1f;

	private float _LastUpdateTime;

	private int _Frame;

	public Text txt;

	private void Start()
	{
		if (!Debug.isDebugBuild)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this._LastUpdateTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		this._Frame++;
		float num = Time.realtimeSinceStartup - this._LastUpdateTime;
		if (1f <= num)
		{
			float num2 = (float)this._Frame / num;
			this.txt.text = "FPS : " + ((int)num2).ToString();
			this._LastUpdateTime = Time.realtimeSinceStartup;
			this._Frame = 0;
		}
	}
}
