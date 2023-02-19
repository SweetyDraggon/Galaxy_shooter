using DG.Tweening;
using System;
using UnityEngine;
using zZ17;

public class Rocket : MonoBehaviour
{
	public ParticleSystem goEff1;

	public ParticleSystem goEff;

	public GameObject goEff3;

	public LayerMask layerMask;

	internal void Init()
	{
		Debug.Log("Init");
		base.transform.DOMove(Vector3.zero, 0.3f, false).SetUpdate(true).OnComplete(delegate
		{
			Debug.Log("DoMove check");
			this.goEff.gameObject.SetActive(true);
			this.goEff1.Stop();
			Debug.Log("goEff1 check");

			base.GetComponent<SpriteRenderer>().enabled = false;
			UnityEngine.Object.Destroy(base.gameObject, 4f);
			Debug.Log("Destroy check");

			EventDispatcher.Instance.PostEvent(EventID.BulletEnemy, null);
			RaycastHit2D[] array = Physics2D.BoxCastAll(Vector2.zero, new Vector2(7.2f, 12.8f), 0f, Vector2.zero, (float)this.layerMask);
			Debug.Log("Cast check, arr = " + array.Length);
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Debug.Log("Enemy to bomb " + i);
					if (array[i].transform.tag != "Player" && array[i].transform.tag == "Enemy" && array[i].transform.parent)
					{
						Debug.Log("Simple Enemy");
						Transform parent = array[i].transform.parent;
						UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(this.goEff3, parent.position, Quaternion.identity), 1f);
						parent.SendMessage("OnHit",
							50 * GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")], 
							SendMessageOptions.DontRequireReceiver);
					}
					if (array[i].transform.GetComponent<Item>())
					{
						Debug.Log("Item");
						Item component = array[i].transform.GetComponent<Item>();
						if (component.checkTypes(new TypeItem[]
						{
							TypeItem.BulletEnemy9,
							TypeItem.BulletSpace,
							TypeItem.Shit,
							TypeItem.Trung
						}))
						{
							UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(this.goEff3, array[i].transform.position, Quaternion.identity), 1f);
							component.transform.HideObject();
						}
					}
					if (array[i].transform.GetComponent<BulletEnemy12>())
					{
						Debug.Log("Bullet Enemy 12");
						UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(this.goEff3, array[i].transform.position, Quaternion.identity), 1f);
						array[i].transform.HideObject();
					}
				}
			}
		});
	}
}
