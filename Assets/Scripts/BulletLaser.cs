using DG.Tweening;
using System;
using UnityEngine;

public class BulletLaser : MonoBehaviour
{
	public GameObject goEff;

	public LayerMask layerMask;

	private RaycastHit2D rayHitAll;

	public float damage = 5f;

	public Renderer render;

	public Transform trnRender;

	public Transform trnBullet;

	private void Awake()
	{
		this.render.material.DOOffset(Vector2.right, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}

	private void Update()
	{
		this.rayHitAll = Physics2D.Linecast(base.transform.position, base.transform.position + Vector3.up * 12f, this.layerMask);
		if (this.rayHitAll)
		{
			this.damage = this.GetDamage();
			if (this.rayHitAll.transform.tag == "Enemy")
			{
				SoundManager.Instance.PlaySoundBulletPlayer(5);
				this.rayHitAll.transform.parent.SendMessage("OnHit", this.damage, SendMessageOptions.DontRequireReceiver);
			}
			float y = (this.rayHitAll.transform.position.y - 0.3f - base.transform.position.y) / 12f;
			this.trnRender.localScale = new Vector3(1f, y, 1f);
			Vector3 position = this.trnRender.position;
			position.y = this.rayHitAll.transform.position.y;
			this.goEff.transform.position = position;
			this.goEff.SetActive(true);
			this.trnRender.position = (this.goEff.transform.position + base.transform.position) / 2f;
		}
		else
		{
			this.goEff.SetActive(false);
			this.goEff.transform.position = new Vector3(base.transform.position.x, 10f);
			float y2 = (this.goEff.transform.position.y - 0.3f - base.transform.position.y) / 12f;
			this.trnRender.localScale = new Vector3(1f, y2, 1f);
			this.trnRender.position = (this.goEff.transform.position + base.transform.position) / 2f;
		}
	}

	public float GetDamage()
	{
		float num = 0.7f + (float)GamePlayManager.Instance.bullet / 10f;
		float x = (!PlayerController.Instance.isMaxBullet) ? (0.5f + (float)GamePlayManager.Instance.bullet / 10f) : 2f;
		this.trnBullet.localScale = new Vector3(x, 1f, 1f);
		if (PlayerController.Instance.isMaxBullet)
		{
			num = 6f;
		}
		return num * GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")];
	}
}
