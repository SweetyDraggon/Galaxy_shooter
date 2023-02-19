using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using zZ17;

public class PanelMoreLive : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public GameObject goBg;

	public Button btnBuyCoin;

	public Button btnShowVideo;

	public PanelGameOver panelGameOver;

	private static Action<long> __f__am_cache0;

	private void Awake()
	{
		Observable.EveryUpdate().Subscribe(delegate(long _)
		{
		});
	}

	//private void OnEnable()
	//{
	//	if (Application.platform == RuntimePlatform.Android)
	//	{
	//		if (Advertisements.Instance.IsRewardVideoAvailable())
	//		{
	//			this.btnShowVideo.enabled = true;
	//			this.btnShowVideo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
	//		}
	//		else
	//		{
	//			this.btnShowVideo.enabled = false;
	//			this.btnShowVideo.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	//		}
	//	}
	//	else if (Application.platform == RuntimePlatform.WindowsEditor)
	//	{
	//		this.btnShowVideo.enabled = false;
	//		this.btnShowVideo.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	//	}
	//}

	public void ShowPanel()
	{
		base.gameObject.SetActive(true);
		this.btnBuyCoin.interactable = (GameManager.Instance.coin >= 500);
	}

	public void ClickBuyCoin(Button btn)
	{
		btn.Click(delegate
		{
			this.Continues();
		});
	}

	public void Continues()
	{
		GameManager.Instance.ChangeCoin(-500);
		base.gameObject.SetActive(false);
		this.panelGameOver.Hide();
		GamePlayManager.Instance.SetBullet((!GameManager.Instance.isEndless) ? 0 : (-3));
		PlayerController.Instance.OnInit();
	}

	private void MoreLive(bool param)
	{
		if (!param)
		{
			return;
		}
		base.gameObject.SetActive(false);
		this.panelGameOver.Hide();
		GamePlayManager.Instance.SetBullet((!GameManager.Instance.isEndless) ? 0 : (-3));
		PlayerController.Instance.OnInit();
	}

	//public void ClickShowVideo(Button btn)
	//{
	//	btn.Click(delegate
	//	{
 //           if(AdsControl.Instance.GetRewardAvailable())
 //           {


 //               AdsControl.Instance.PlayDelegateRewardVideo(delegate
 //               {
 //                   //function
                 
 //                   this.MoreLive(true);
 //               });

 //           }
           
	//	});
	//}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.pointerCurrentRaycast.gameObject == this.goBg)
		{
			base.gameObject.SetActive(false);
		}
	}
}
