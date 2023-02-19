using System;
using UnityEngine;

[RequireComponent(typeof(GUIText))]
public class UbhAutoScaleGUIText : MonoBehaviour
{
	private GUIText _GuiText;

	private float _OrgFontSize;

	private void Awake()
	{
		this._GuiText = base.GetComponent<GUIText>();
		this._OrgFontSize = (float)this._GuiText.fontSize;
	}

	private void Update()
	{
		float num = (float)Screen.width / 600f;
		float num2 = (float)Screen.height / 450f;
		float num3 = (Screen.height >= Screen.width) ? num : num2;
		this._GuiText.fontSize = (int)(this._OrgFontSize * num3);
	}
}
