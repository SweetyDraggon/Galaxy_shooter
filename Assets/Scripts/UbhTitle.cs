using System;
using UnityEngine;

public class UbhTitle : UbhMonoBehaviour
{
	private const string TITLE_PC = "Press X";

	private const string TITLE_MOBILE = "Tap To Start";

	[SerializeField]
	private GUIText _StartGUIText;

	private void Start()
	{
		this._StartGUIText.text = ((!UbhUtil.IsMobilePlatform()) ? "Press X" : "Tap To Start");
	}
}
