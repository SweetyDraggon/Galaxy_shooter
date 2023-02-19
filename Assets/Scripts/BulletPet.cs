using DG.Tweening;
using System;
using UnityEngine;

public class BulletPet : MonoBehaviour
{
	public void Init(Vector3 pos)
	{
		base.transform.DOKill(false);
		base.transform.position = pos;
		base.gameObject.SetActive(true);
		base.transform.DOMoveY(8f, (8f - pos.y) * 0.1f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		string tag = collision.tag;
		if (tag != null)
		{
			if (tag == "Enemy")
			{
				collision.transform.parent.SendMessage("OnHit", 0.2f, SendMessageOptions.DontRequireReceiver);
				base.transform.SetAsFirstSibling();
				base.gameObject.SetActive(false);
			}
		}
	}
}
