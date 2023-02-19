using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHider : MonoBehaviour
{
	public GameObject menuHider;
	
	
	// Start is called before the first frame update
	void Start()
    {
			
	}

	public void SetHideMenu(bool state)
	{
		menuHider.SetActive(state);
	}

	private void OnEnable()
	{
		SetHideMenu(ResetLevel.hideMenu);
	}
}
