using System;
using UnityEngine;
using zZ17;

public class Bullet : MonoBehaviour
{
	public Sprite[] arrSprite;

	public float speed = 3f;

	internal float damage = 2f;

	internal TypeBullet idBullet;

	private SpriteRenderer _spriteRender;

	private Rigidbody2D _rigid;

	public GameObject eff;

	private void Awake()
	{
		this._rigid = base.GetComponent<Rigidbody2D>();
		this._spriteRender = base.GetComponent<SpriteRenderer>();
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
		this._spriteRender.sprite = this.arrSprite[(int)this.idBullet];
		base.transform.position = PlayerController.Instance.arrGun1[0].position + pos;
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
		this.damage = prDamage * GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")];
		this.speed = prSpeed;
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
					SoundManager.Instance.PlaySoundBulletPlayer((int)this.idBullet);
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
