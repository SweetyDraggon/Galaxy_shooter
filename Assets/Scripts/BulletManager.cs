using System;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
	public Bullet[] prbBullet;

	public Color[] ColorBullet;

	internal static BulletManager Instance;

	public Transform trnBullet4Manager;

	public Bullet4 prefabBullet4;

	internal Vector2[] sizeBullet = new Vector2[]
	{
		new Vector2(0.1f, 0.05f),
		new Vector2(0.2f, 0.1f),
		new Vector2(0.25f, 0.12f),
		new Vector2(0.4f, 0.18f)
	};

	private void Awake()
	{
		BulletManager.Instance = this;
	}

	internal void AddBullet()
	{
		if (GamePlayManager.state != StateGame.PLAY || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		int num = (!PlayerController.Instance.isMaxBullet) ? GamePlayManager.Instance.bullet : 12;
		if (num == 0)
		{
			num = 1;
		}
		if (GamePlayManager.Instance.typeBullet == TypeBullet.Bullet4)
		{
			Bullet4[] array = new Bullet4[num];
			if (this.trnBullet4Manager.childCount > 0)
			{
				for (int i = 0; i < this.trnBullet4Manager.childCount; i++)
				{
					if (this.trnBullet4Manager.GetChild(i).gameObject.activeSelf || num <= 0)
					{
						break;
					}
					Bullet4 component = this.trnBullet4Manager.GetChild(0).GetComponent<Bullet4>();
					component.transform.SetAsLastSibling();
					component.transform.localRotation = Quaternion.identity;
					num--;
					array[num] = component;
				}
			}
			if (num > 0)
			{
				for (int j = 0; j < num; j++)
				{
					Bullet4 bullet = UnityEngine.Object.Instantiate<Bullet4>(this.prefabBullet4);
					bullet.transform.SetParent(this.trnBullet4Manager);
					bullet.transform.localScale = Vector3.one;
					bullet.transform.SetAsLastSibling();
					array[j] = bullet;
				}
			}
			this.Fire3(array);
		}
		else
		{
			Bullet[] array2 = new Bullet[num];
			if (base.transform.childCount > 0)
			{
				for (int k = 0; k < base.transform.childCount; k++)
				{
					if (base.transform.GetChild(k).gameObject.activeSelf || num <= 0)
					{
						break;
					}
					Bullet component2 = base.transform.GetChild(0).GetComponent<Bullet>();
					component2.transform.SetAsLastSibling();
					component2.transform.localRotation = Quaternion.identity;
					num--;
					array2[num] = component2;
				}
			}
			if (num > 0)
			{
				for (int l = 0; l < num; l++)
				{
					Bullet bullet2 = UnityEngine.Object.Instantiate<Bullet>(this.prbBullet[0]);
					bullet2.transform.SetParent(base.transform);
					bullet2.transform.localScale = Vector3.one;
					bullet2.transform.SetAsLastSibling();
					array2[l] = bullet2;
				}
			}
			this.IntanceBullet(array2);
		}
	}

	private void IntanceBullet(Bullet[] prArrBullet)
	{
		if (GamePlayManager.state != StateGame.PLAY)
		{
			return;
		}
		switch (GamePlayManager.Instance.typeBullet)
		{
		case TypeBullet.Bullet1:
			this.Fire0(prArrBullet);
			break;
		case TypeBullet.Bullet2:
			this.Fire1(prArrBullet);
			break;
		case TypeBullet.Bullet3:
			this.Fire2(prArrBullet);
			break;
		}
	}

	private void Fire0(Bullet[] x)
	{
		switch (x.Length)
		{
		case 1:
			x[0].Init(1f, 12f, Vector3.zero, 0f, 1f);
			return;
		case 2:
			x[0].Init(1f, 12f, new Vector3(0.08f, 0f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(-0.08f, 0f), 0f, 1f);
			return;
		case 3:
			x[0].Init(1f, 12f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(-0.08f, 0f), 7.5f, 1f);
			x[2].Init(1f, 12f, new Vector3(0.08f, 0f), -7.5f, 1f);
			return;
		case 4:
			x[0].Init(1f, 12f, new Vector3(0f, 0f), -3.75f, 1f);
			x[1].Init(1f, 12f, new Vector3(0f, 0f), 3.75f, 1f);
			x[2].Init(1f, 12f, new Vector3(0f, 0f), -11.5f, 1f);
			x[3].Init(1f, 12f, new Vector3(0f, 0f), 11.5f, 1f);
			return;
		case 5:
			x[0].Init(1f, 12f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(0f, 0f), -7.5f, 1f);
			x[2].Init(1f, 12f, new Vector3(0f, 0f), 7.5f, 1f);
			x[3].Init(1f, 12f, new Vector3(0f, 0f), -15f, 1f);
			x[4].Init(1f, 12f, new Vector3(0f, 0f), 15f, 1f);
			return;
		case 6:
			x[0].Init(1f, 12f, new Vector3(0.08f, 0f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(-0.08f, 0f), 0f, 1f);
			x[2].Init(1f, 12f, new Vector3(0f, 0f), -7.5f, 1f);
			x[3].Init(1f, 12f, new Vector3(0f, 0f), 7.5f, 1f);
			x[4].Init(1f, 12f, new Vector3(0f, 0f), -15f, 1f);
			x[5].Init(1f, 12f, new Vector3(0f, 0f), 15f, 1f);
			return;
		case 7:
			x[0].Init(1f, 12f, new Vector3(0f, 0.1f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(-0.08f, 0f), 7.5f, 1f);
			x[2].Init(1f, 12f, new Vector3(-0.08f, 0f), 15f, 1f);
			x[3].Init(1f, 12f, new Vector3(0.08f, 0f), -7.5f, 1f);
			x[4].Init(1f, 12f, new Vector3(0.08f, 0f), -15f, 1f);
			x[5].Init(1f, 12f, new Vector3(-0.08f, 0f), -7.5f, 1f);
			x[6].Init(1f, 12f, new Vector3(0.08f, 0f), 7.5f, 1f);
			return;
		case 8:
			x[0].Init(1f, 12f, new Vector3(0.08f, 0f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(-0.08f, 0f), 0f, 1f);
			x[2].Init(1f, 12f, new Vector3(0.08f, 0f), -7.5f, 1f);
			x[3].Init(1f, 12f, new Vector3(0.08f, 0f), 7.5f, 1f);
			x[4].Init(1f, 12f, new Vector3(-0.08f, 0f), -7.5f, 1f);
			x[5].Init(1f, 12f, new Vector3(-0.08f, 0f), 7.5f, 1f);
			x[6].Init(1f, 12f, new Vector3(0f, 0f), 15f, 1f);
			x[7].Init(1f, 12f, new Vector3(0f, 0f), -15f, 1f);
			return;
		case 9:
			x[0].Init(2f, 12f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(1f, 12f, new Vector3(-0.08f, 0f), -7.5f, 1f);
			x[2].Init(1f, 12f, new Vector3(-0.08f, 0f), 7.5f, 1f);
			x[3].Init(1f, 12f, new Vector3(0.08f, 0f), 7.5f, 1f);
			x[4].Init(1f, 12f, new Vector3(0.08f, 0f), -7.5f, 1f);
			x[5].Init(1f, 12f, new Vector3(0.08f, 0f), -15f, 1f);
			x[6].Init(1f, 12f, new Vector3(0.08f, 0f), 15f, 1f);
			x[7].Init(1f, 12f, new Vector3(-0.08f, 0f), -15f, 1f);
			x[8].Init(1f, 12f, new Vector3(-0.08f, 0f), 15f, 1f);
			return;
		case 10:
			x[0].Init(2f, 12f, new Vector3(0.08f, 0f), 0f, 1f);
			x[1].Init(2f, 12f, new Vector3(-0.08f, 0f), 0f, 1f);
			x[2].Init(1f, 12f, new Vector3(0.08f, 0f), -7.5f, 1f);
			x[3].Init(1f, 12f, new Vector3(0.08f, 0f), 7.5f, 1f);
			x[4].Init(1f, 12f, new Vector3(-0.08f, 0f), -7.5f, 1f);
			x[5].Init(1f, 12f, new Vector3(-0.08f, 0f), 7.5f, 1f);
			x[6].Init(1f, 12f, new Vector3(0.08f, 0f), 15f, 1f);
			x[7].Init(1f, 12f, new Vector3(0.08f, 0f), -15f, 1f);
			x[8].Init(1f, 12f, new Vector3(-0.08f, 0f), -15f, 1f);
			x[9].Init(1f, 12f, new Vector3(-0.08f, 0f), 15f, 1f);
			return;
		case 11:
			x[0].Init(1f, 14f, new Vector3(0.08f, 0f), 0f, 1f);
			x[1].Init(2f, 14f, new Vector3(-0.08f, 0f), 0f, 1f);
			x[2].Init(1f, 14f, new Vector3(0.08f, 0f), -7.5f, 1f);
			x[3].Init(2f, 14f, new Vector3(0.08f, 0f), 7.5f, 1f);
			x[4].Init(1f, 14f, new Vector3(-0.08f, 0f), -7.5f, 1f);
			x[5].Init(2f, 14f, new Vector3(-0.08f, 0f), 7.5f, 1f);
			x[6].Init(1f, 14f, new Vector3(0.08f, 0f), 15f, 1f);
			x[7].Init(2f, 14f, new Vector3(0.08f, 0f), -15f, 1f);
			x[8].Init(1f, 14f, new Vector3(-0.08f, 0f), -15f, 1f);
			x[9].Init(2f, 14f, new Vector3(-0.08f, 0f), 15f, 1f);
			return;
		}
		x[0].Init(2f, 16f, new Vector3(0.08f, 0f), 0f, 1f);
		x[1].Init(2f, 16f, new Vector3(-0.08f, 0f), 0f, 1f);
		x[2].Init(2f, 16f, new Vector3(0.08f, 0f), -7.5f, 1f);
		x[3].Init(2f, 16f, new Vector3(0.08f, 0f), 7.5f, 1f);
		x[4].Init(2f, 16f, new Vector3(-0.08f, 0f), -7.5f, 1f);
		x[5].Init(2f, 16f, new Vector3(-0.08f, 0f), 7.5f, 1f);
		x[6].Init(2f, 16f, new Vector3(0.08f, 0f), 15f, 1f);
		x[7].Init(2f, 16f, new Vector3(0.08f, 0f), -15f, 1f);
		x[8].Init(2f, 16f, new Vector3(-0.08f, 0f), -15f, 1f);
		x[9].Init(2f, 16f, new Vector3(-0.08f, 0f), 15f, 1f);
		x[10].Init(2f, 16f, new Vector3(-0.1f, 0f), -22.5f, 1f);
		x[11].Init(2f, 16f, new Vector3(0.1f, 0f), 22.5f, 1f);
	}

	private void Fire1(Bullet[] x)
	{
		switch (x.Length)
		{
		case 1:
			x[0].Init(1.8f, 10f, Vector3.zero, 0f, 1f);
			return;
		case 2:
			x[0].Init(1.8f, 10f, new Vector3(0.12f, 0f), 0f, 1f);
			x[1].Init(1.8f, 10f, new Vector3(-0.12f, 0f), 0f, 1f);
			return;
		case 3:
			x[0].Init(1.8f, 10f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(1.8f, 10f, new Vector3(-0.17f, -0.1f), 0f, 1f);
			x[2].Init(1.8f, 10f, new Vector3(0.17f, -0.1f), 0f, 1f);
			return;
		case 4:
			x[0].Init(1.8f, 10.2f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(1.8f, 10f, new Vector3(-0.15f, -0.15f), 0f, 1f);
			x[2].Init(1.8f, 10f, new Vector3(0.15f, -0.15f), 0f, 1f);
			return;
		case 5:
			x[0].Init(1.8f, 10f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 10f, new Vector3(-0.15f, 0f), 0f, 1f);
			x[2].Init(1.8f, 10f, new Vector3(0.15f, 0f), 0f, 1f);
			x[3].Init(1.8f, 10f, new Vector3(-0.3f, -0.15f), 0f, 1f);
			x[4].Init(1.8f, 10f, new Vector3(0.3f, -0.15f), 0f, 1f);
			return;
		case 6:
			x[0].Init(1.8f, 10.2f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 10f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(1.8f, 10f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(1.8f, 10f, new Vector3(-0.32f, -0.15f), 0f, 1f);
			x[4].Init(1.8f, 10f, new Vector3(0.32f, -0.15f), 0f, 1f);
			return;
		case 7:
			x[0].Init(2.8f, 10.2f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 10f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(1.8f, 10f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(1.8f, 10f, new Vector3(-0.34f, -0.15f), 0f, 1f);
			x[4].Init(1.8f, 10f, new Vector3(0.34f, -0.15f), 0f, 1f);
			return;
		case 8:
			x[0].Init(1.9f, 10.2f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.9f, 10.2f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(1.9f, 10.2f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(1f, 10f, new Vector3(-0.32f, -0.15f), 0f, 1f);
			x[4].Init(1f, 10f, new Vector3(0.32f, -0.15f), 0f, 1f);
			return;
		case 9:
			x[0].Init(2.8f, 10.4f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(2f, 10.2f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(2f, 10.2f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(1f, 10f, new Vector3(-0.36f, -0.15f), 0f, 1f);
			x[4].Init(1f, 10f, new Vector3(0.36f, -0.15f), 0f, 1f);
			return;
		case 10:
			x[0].Init(2f, 10.2f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(2f, 10.2f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(2f, 10.2f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(2f, 10.2f, new Vector3(-0.34f, -0.15f), 0f, 1f);
			x[4].Init(2f, 10.2f, new Vector3(0.34f, -0.15f), 0f, 1f);
			return;
		case 11:
			x[0].Init(3f, 10.4f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(2f, 10.2f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(2f, 10.2f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(2f, 10.2f, new Vector3(-0.36f, -0.15f), 0f, 1f);
			x[4].Init(2f, 10.2f, new Vector3(0.36f, -0.15f), 0f, 1f);
			return;
		}
		x[0].Init(3f, 10.4f, new Vector3(0f, 0.15f), 0f, 1f);
		x[1].Init(3f, 10.4f, new Vector3(-0.19f, 0f), 0f, 1f);
		x[2].Init(3f, 10.4f, new Vector3(0.19f, 0f), 0f, 1f);
		x[3].Init(2f, 10.4f, new Vector3(-0.38f, -0.15f), 0f, 1f);
		x[4].Init(2f, 10.4f, new Vector3(0.38f, -0.15f), 0f, 1f);
	}

	private void Fire2(Bullet[] x)
	{
		switch (x.Length)
		{
		case 1:
			x[0].Init(0.75f, 30f, Vector3.zero, 0f, 1f);
			return;
		case 2:
			x[0].Init(0.75f, 30f, new Vector3(0.12f, 0f), 0f, 1f);
			x[1].Init(0.75f, 30f, new Vector3(-0.12f, 0f), 0f, 1f);
			return;
		case 3:
			x[0].Init(0.8f, 30f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.17f, -0.12f), 0f, 1f);
			x[2].Init(0.8f, 30f, new Vector3(0.17f, -0.12f), 0f, 1f);
			return;
		case 4:
			x[0].Init(0.8f, 30f, new Vector3(-0.25f, -0.15f), 0f, 1f);
			x[1].Init(0.8f, 30f, new Vector3(0.25f, -0.15f), 0f, 1f);
			x[2].Init(0.8f, 30f, new Vector3(0.08f, 0f), 0f, 1f);
			x[3].Init(0.8f, 30f, new Vector3(-0.08f, 0f), 0f, 1f);
			return;
		case 5:
			x[0].Init(0.9f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.15f, 0f), 0f, 1f);
			x[2].Init(0.9f, 30f, new Vector3(0.15f, 0f), 0f, 1f);
			x[3].Init(0.9f, 30f, new Vector3(-0.3f, -0.15f), 0f, 1f);
			x[4].Init(0.9f, 30f, new Vector3(0.3f, -0.15f), 0f, 1f);
			return;
		case 6:
			x[0].Init(1.6f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(0.9f, 30f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(0.9f, 30f, new Vector3(-0.32f, -0.15f), 0f, 1f);
			x[4].Init(0.9f, 30f, new Vector3(0.32f, -0.15f), 0f, 1f);
			return;
		case 7:
			x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(0.9f, 30f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(0.9f, 30f, new Vector3(-0.34f, -0.15f), 0f, 1f);
			x[4].Init(0.9f, 30f, new Vector3(0.34f, -0.15f), 0f, 1f);
			return;
		case 8:
			x[0].Init(1.8f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 30f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(1.8f, 30f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(1f, 30f, new Vector3(-0.32f, -0.15f), 0f, 1f);
			x[4].Init(1f, 30f, new Vector3(0.32f, -0.15f), 0f, 1f);
			return;
		case 9:
			x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 30f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(1.8f, 30f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(1.2f, 30f, new Vector3(-0.36f, -0.15f), 0f, 1f);
			x[4].Init(1.2f, 30f, new Vector3(0.36f, -0.15f), 0f, 1f);
			return;
		case 10:
			x[0].Init(1.6f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.6f, 30f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(1.6f, 30f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(1.6f, 30f, new Vector3(-0.34f, -0.15f), 0f, 1f);
			x[4].Init(1.6f, 30f, new Vector3(0.34f, -0.15f), 0f, 1f);
			return;
		case 11:
			x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.7f, 30f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(1.7f, 30f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(1.7f, 30f, new Vector3(-0.36f, -0.15f), 0f, 1f);
			x[4].Init(1.7f, 30f, new Vector3(0.36f, -0.15f), 0f, 1f);
			return;
		}
		x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
		x[1].Init(2f, 30f, new Vector3(-0.19f, 0f), 0f, 1f);
		x[2].Init(2f, 30f, new Vector3(0.19f, 0f), 0f, 1f);
		x[3].Init(1.9f, 30f, new Vector3(-0.38f, -0.15f), 0f, 1f);
		x[4].Init(1.9f, 30f, new Vector3(0.38f, -0.15f), 0f, 1f);
	}

	private void Fire3(Bullet4[] x)
	{
		switch (x.Length)
		{
		case 1:
			x[0].Init(1.2f, 15f, Vector3.zero, 0f, 1f);
			return;
		case 2:
			x[0].Init(0.75f, 30f, new Vector3(0.12f, 0f), 0f, 1f);
			x[1].Init(0.75f, 30f, new Vector3(-0.12f, 0f), 0f, 1f);
			return;
		case 3:
			x[0].Init(0.8f, 30f, new Vector3(0f, 0f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.17f, -0.12f), 0f, 1f);
			x[2].Init(0.8f, 30f, new Vector3(0.17f, -0.12f), 0f, 1f);
			return;
		case 4:
			x[0].Init(0.8f, 30f, new Vector3(-0.25f, -0.15f), 0f, 1f);
			x[1].Init(0.8f, 30f, new Vector3(0.25f, -0.15f), 0f, 1f);
			x[2].Init(0.8f, 30f, new Vector3(0.08f, 0f), 0f, 1f);
			x[3].Init(0.8f, 30f, new Vector3(-0.08f, 0f), 0f, 1f);
			return;
		case 5:
			x[0].Init(0.9f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.15f, 0f), 0f, 1f);
			x[2].Init(0.9f, 30f, new Vector3(0.15f, 0f), 0f, 1f);
			x[3].Init(0.9f, 30f, new Vector3(-0.3f, -0.15f), 0f, 1f);
			x[4].Init(0.9f, 30f, new Vector3(0.3f, -0.15f), 0f, 1f);
			return;
		case 6:
			x[0].Init(1.6f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.17f, 0f), 0f, 1f);
			x[2].Init(0.9f, 30f, new Vector3(0.17f, 0f), 0f, 1f);
			x[3].Init(0.9f, 30f, new Vector3(-0.32f, -0.15f), 0f, 1f);
			x[4].Init(0.9f, 30f, new Vector3(0.32f, -0.15f), 0f, 1f);
			return;
		case 7:
			x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(0.9f, 30f, new Vector3(-0.19f, 0f), 0f, 1f);
			x[2].Init(0.9f, 30f, new Vector3(0.19f, 0f), 0f, 1f);
			x[3].Init(0.9f, 30f, new Vector3(-0.34f, -0.15f), 0f, 1f);
			x[4].Init(0.9f, 30f, new Vector3(0.34f, -0.15f), 0f, 1f);
			return;
		case 8:
			x[0].Init(1.8f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 30f, new Vector3(-0.17f, 0f), 0f, 1.5f);
			x[2].Init(1.8f, 30f, new Vector3(0.17f, 0f), 0f, 1.5f);
			x[3].Init(1f, 30f, new Vector3(-0.32f, -0.15f), 0f, 2f);
			x[4].Init(1f, 30f, new Vector3(0.32f, -0.15f), 0f, 2f);
			return;
		case 9:
			x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.8f, 30f, new Vector3(-0.19f, -0.25f), 0f, 1.5f);
			x[2].Init(1.8f, 30f, new Vector3(0.19f, -0.25f), 0f, 1.5f);
			x[3].Init(1.2f, 30f, new Vector3(-0.36f, -0.5f), 0f, 2f);
			x[4].Init(1.2f, 30f, new Vector3(0.36f, -0.5f), 0f, 2f);
			return;
		case 10:
			x[0].Init(1.6f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.6f, 30f, new Vector3(-0.4f, -0.3f), 0f, 1.5f);
			x[2].Init(1.6f, 30f, new Vector3(0.4f, -0.3f), 0f, 1.5f);
			x[3].Init(1.6f, 30f, new Vector3(-0.8f, -0.6f), 0f, 2f);
			x[4].Init(1.6f, 30f, new Vector3(0.8f, -0.6f), 0f, 2f);
			return;
		case 11:
			x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
			x[1].Init(1.7f, 30f, new Vector3(-0.3f, -0.3f), 0f, 1.5f);
			x[2].Init(1.7f, 30f, new Vector3(0.3f, -0.3f), 0f, 1.5f);
			x[3].Init(1.7f, 30f, new Vector3(-0.8f, -0.6f), 0f, 2f);
			x[4].Init(1.7f, 30f, new Vector3(0.8f, -0.6f), 0f, 2f);
			return;
		}
		x[0].Init(2f, 30f, new Vector3(0f, 0.15f), 0f, 1f);
		x[1].Init(2f, 30f, new Vector3(-0.5f, -0.3f), 0f, 1.5f);
		x[2].Init(2f, 30f, new Vector3(0.5f, -0.3f), 0f, 1.5f);
		x[3].Init(1.9f, 30f, new Vector3(-0.8f, -0.6f), 0f, 2f);
		x[4].Init(1.9f, 30f, new Vector3(0.8f, -0.6f), 0f, 2f);
	}
}
