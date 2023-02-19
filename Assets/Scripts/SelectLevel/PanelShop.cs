using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using zZ17;

namespace SelectLevel
{
	public class PanelShop : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		[SerializeField]
		private Text txtCoin;

		public GameObject goBg;

		internal ItemShop itemSelect;

		public Transform trnSelect;

		public Button btnGiam;

		public Button btnTang;

		private void Awake()
		{
			this.RegisterListener(EventID.Coin, delegate(object param)
			{
				this.ShowCoin(this.txtCoin);
			});
			this.btnTang.onClick.AddListener(delegate
			{
				this.btnTang.Click(delegate
				{
					if (this.itemSelect == null)
					{
						return;
					}
					if (this.itemSelect.price <= GameManager.Instance.coin)
					{
						this.ChangeValue(-1);
					}
				});
			});
			this.btnGiam.onClick.AddListener(delegate
			{
				this.btnGiam.Click(delegate
				{
					this.ChangeValue(1);
				});
			});
		}

		private void ChangeValue(int value)
		{
			if (this.itemSelect == null)
			{
				return;
			}
			if (PlayerPrefs.GetInt(this.itemSelect.type.ToString()) - value < 0)
			{
				return;
			}
			PlayerPrefs.SetInt(this.itemSelect.type.ToString(), PlayerPrefs.GetInt(this.itemSelect.type.ToString()) - value);
			this.itemSelect.txtCount.text = "x" + PlayerPrefs.GetInt(this.itemSelect.type.ToString());
			GameManager.Instance.ChangeCoin(this.itemSelect.price * value);
			this.btnGiam.interactable = (PlayerPrefs.GetInt(this.itemSelect.type.ToString()) > 0);
			this.btnTang.interactable = (GameManager.Instance.coin - this.itemSelect.price > 0);
		}

		private void OnEnable()
		{
			this.goBg.gameObject.SetActive(false);
			this.PostEvent(EventID.Coin, 0);
			this.UnSelect();
		}

		public void ClickStart(Button btn)
		{
			btn.Click(delegate
			{
				ResetLevel.Instance.RestartLevel();
			//	this.Hide();
			//	GameManager.Instance.ChangeState(Scene.GAMEPLAY);
			});
		}

		internal void SetSelect(ItemShop item)
		{
			if (this.itemSelect == item)
			{
				this.UnSelect();
			}
			else
			{
				if (this.itemSelect)
				{
					this.itemSelect.SetPause();
				}
				this.goBg.transform.SetAsLastSibling();
				this.goBg.gameObject.SetActive(true);
				item.transform.SetAsLastSibling();
				this.itemSelect = item;
				this.trnSelect.gameObject.SetActive(true);
				this.btnGiam.interactable = (PlayerPrefs.GetInt(this.itemSelect.type.ToString()) > 0);
				this.btnTang.interactable = (GameManager.Instance.coin - this.itemSelect.price > 0);
				item.SetPlay();
			}
		}

		internal void UnSelect()
		{
			this.goBg.transform.SetAsFirstSibling();
			this.goBg.gameObject.SetActive(false);
			if (this.itemSelect)
			{
				this.itemSelect.SetPause();
			}
			this.itemSelect = null;
			this.trnSelect.gameObject.SetActive(false);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.pointerCurrentRaycast.gameObject == this.goBg)
			{
				Vector2 vector = Camera.main.ScreenToWorldPoint(eventData.position);
				Vector2 origin = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.zero);
				if (raycastHit2D.transform != null && raycastHit2D.transform.GetComponent<ItemShop>())
				{
					this.UnSelect();
					this.SetSelect(raycastHit2D.transform.GetComponent<ItemShop>());
				}
				else if (Mathf.Abs(vector.x) <= 3f && Mathf.Abs(vector.y) <= 3.1f)
				{
					this.UnSelect();
				}
				else
				{
					base.gameObject.SetActive(false);
				}
			}
		}
	}
}
