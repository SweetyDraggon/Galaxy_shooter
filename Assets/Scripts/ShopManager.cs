using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class ShopManager : MonoBehaviour
{
	[SerializeField]
	private Text txtCoin;

	private static TweenCallback __f__am_cache0;

	private void Awake()
	{
		this.RegisterListener(EventID.Coin, delegate(object param)
		{
			this.ShowCoin(this.txtCoin);
		});
	}

	private void OnEnable()
	{
		this.PostEvent(EventID.Coin, 0);
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ChangeState(Scene.HOME);
        }

    }
    public void ClickBack(Button btn)
	{
		btn.Click(delegate
		{
			GameManager.Instance.ChangeState(Scene.HOME);
		});
	}
}
