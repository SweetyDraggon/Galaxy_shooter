using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class HomeManager : MonoBehaviour
{
	[SerializeField]
	private PanelSetting panelSetting;
    public GameObject settings;
	public GamePlayManager GamePlayManag;
	public GameManager gamemanager;
	public Transform trnLogo;
    bool isOpen;
	public Text txtCoin;
    public GameObject exitPanel;
	private static TweenCallback __f__am_cache0;

	private static TweenCallback __f__am_cache1;

	private static TweenCallback __f__am_cache2;

	private static TweenCallback __f__am_cache3;

	private static TweenCallback __f__am_cache4;

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

	public void OnClickStart(Button btn)
	{
		btn.Click(delegate
		{
			GameManager.Instance.isEndless = false;
			GameManager.Instance.ChangeState(Scene.SELECTLEVEL);
		});

		GamePlayManager.isPauseInfin = false;
		GamePlayManag.Pausepaneldefinition();
	}

	

	public void OnClickSetting(Button btn)
	{
		btn.Click(delegate
		{
			this.panelSetting.Show(1f);
		});
	}

	public void OnClickShop(Button btn)
	{
		btn.Click(delegate
		{
			GameManager.Instance.ChangeState(Scene.SHOP);
		});
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settings.activeInHierarchy)
        {
            panelSetting.Hide();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !settings.activeInHierarchy)
        { isOpen = !isOpen; }
        if(isOpen)
        {
            exitPanel.SetActive(true);
        }
        else
        {
            exitPanel.SetActive(false);
        }
    }
    public void Yes()
    {
        Application.Quit();
    }
    public void No()
    {
		isOpen = false;
		exitPanel.SetActive(false);
    }

	public void OnClickEndless(Button btn)
	{
		btn.Click(delegate
		{
			GameManager.Instance.level = 1;
			GameManager.Instance.isEndless = true;
			GameManager.Instance.ChangeState(Scene.GAMEPLAY);

			GameManager.Instance.loadManager.loadMap.ResetData();

			gamemanager.ClearEnemys();
			GamePlayManager.isPauseInfin = true;
			GamePlayManag.Pausepaneldefinition();
		});
	}
}
