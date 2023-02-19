using System;
using UnityEngine;
using zZ17;

public class ThanhChan : Enemy
{
	public Sprite[] sprites;

	public SpriteRenderer spriteRender;

	private void Awake()
	{
		Action<object> callback = delegate(object x)
		{
			base.transform.HideObject();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Disable, callback);
	}

	protected virtual void OnEnable()
	{
		this.isDie = (this.isDone = (this.isMove = false));
		this.spriteRender.sprite = this.sprites[0];
		if (this.Eff)
		{
			this.Eff.transform.localPosition = Vector3.zero;
		}
	}

	protected virtual void OnHit(float dame)
	{
		if (this.isDie || GamePlay.Instance.loadingManager.gameObject.activeSelf)
		{
			return;
		}
		if (!EnemyManager.isEnableOnHit(base.transform.position))
		{
			return;
		}
		this.Health -= dame;
		if (this.Health / this.MaxHealth <= 0.5f)
		{
			this.spriteRender.sprite = this.sprites[1];
		}
		if (this.Health <= 0f)
		{
			SoundManager.Instance.PlaySoundEnemy(2);
			this.isDie = true;
			this.Health = 0f;
			this.Eff.Play();
			base.StartCoroutine(base.Disable());
		}
		else
		{
			SoundManager.Instance.PlaySoundEnemy(0);
		}
	}
}
