using DG.Tweening;
using System;
using UnityEngine;
using zZ17;

public class Bullet4 : MonoBehaviour
{
	public float speed = 3f;

	internal float damage = 2f;

	internal TypeBullet idBullet;

	private Rigidbody2D _rigid;

	public GameObject eff;

	private void Awake()
	{
		this._rigid = base.GetComponent<Rigidbody2D>();
		Action<object> callback = delegate(object x)
		{
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	internal void Init(float prDamage, float prSpeed, Vector3 pos, float rot = 0f, float scale = 1f)
	{
		base.gameObject.SetActive(true);
		this.idBullet = GamePlayManager.Instance.typeBullet;
		base.transform.DOKill(false);
		base.transform.localScale = Vector3.one;
		base.transform.position = PlayerController.Instance.arrGun1[0].position + pos;
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
		this.damage = prDamage * GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")];
		this.speed = prSpeed * 0.5f;
	}

	private void Update()
	{
		base.transform.Translate(Vector3.up * Time.deltaTime * this.speed);
		if (base.transform.position.y > 8f)
		{
			base.transform.HideObject();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.tag;
		if (tag != null)
		{
			if (!(tag == "BulletManager"))
			{
				if (tag == "Enemy")
				{
					SoundManager.Instance.PlaySoundBulletPlayer(6);
					other.transform.parent.SendMessage("OnHit", this.damage, SendMessageOptions.DontRequireReceiver);
					base.transform.SetAsFirstSibling();
					base.gameObject.SetActive(false);
				}
			}
			else
			{
				base.transform.HideObject();
			}
		}
	}
}
