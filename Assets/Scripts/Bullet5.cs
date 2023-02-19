using DG.Tweening;
using System;
using UnityEngine;

public class Bullet5 : MonoBehaviour
{
	public float speed = 3f;

	internal TypeBullet idBullet;

	private Rigidbody2D _rigid;

	public GameObject eff;

	public ParticleSystem effBullet;

	private ParticleSystem.EmissionModule emissionModule;

	private void Awake()
	{
		this.emissionModule = this.effBullet.emission;
	}

	private void OnParticleCollision(GameObject other)
	{
		SoundManager.Instance.PlaySoundBulletPlayer(7);
		other.transform.parent.SendMessage("OnHit", this.GetDamage(), SendMessageOptions.DontRequireReceiver);
	}

	public float GetDamage()
	{
		this.emissionModule.rateOverTime = (float)((!PlayerController.Instance.isMaxBullet) ? (10 + GamePlayManager.Instance.bullet * 2) : 30);
		float num = 5f + (float)GamePlayManager.Instance.bullet / 10f;
		if (PlayerController.Instance.isMaxBullet)
		{
			num = 6f;
		}
		return num * GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")];
	}

	internal void Init(float prDamage, float prSpeed, Vector3 pos, float rot = 0f, float scale = 1f)
	{
		this.emissionModule.rateOverTime = (float)(10 + GamePlayManager.Instance.bullet * 2);
		base.gameObject.SetActive(true);
		this.idBullet = GamePlayManager.Instance.typeBullet;
		base.transform.DOKill(false);
		base.transform.localScale = Vector3.zero;
		base.transform.position = PlayerController.Instance.arrGun1[0].position + pos;
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
		this.speed = prSpeed;
		base.transform.DOScale(1f, 0.3f);
	}
}
