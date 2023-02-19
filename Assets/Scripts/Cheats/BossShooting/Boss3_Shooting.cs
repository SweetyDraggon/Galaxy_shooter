using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3_Shooting : MonoBehaviour
{
	public GameObject bulletPrefab;
	public float shootDelay = 1f;
	public float bulletSpeed = 4f;
	private Transform playerShip;

    // Start is called before the first frame update
    void Start()
    {
		playerShip = PlayerController.Instance.transform;
    }



    // Update is called once per frame
    void Update()
    {
		if (CalcTime())
			Shoot();
    }


	private float timer = 0f;
	bool CalcTime()
	{
		if (timer > shootDelay)
		{
			timer = 0f;
			return true;
		}
		timer += Time.deltaTime;
		return false;
	}


	void Shoot()
	{
		// Calculate Direction vector
		var dir = playerShip.position - transform.position;

		// Shooting Bullet
		var tempBullet = Instantiate(bulletPrefab, transform.position, new Quaternion());
		tempBullet.transform.LookAt2D(playerShip.transform);
		tempBullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
		Destroy(tempBullet, 5f);

	}
}
