using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy
{
	private new readonly int startHealth = 5;
	private new readonly int range = 20;
	private bool shooting = false;
	public GameObject bulletGO;
	public List<Bullet> bullets;
	private float bulletSpeed = 5;

	protected override void Start()
	{
		base.startHealth = startHealth;
		base.range = range;
		base.Start();
	}

	protected override void Update()
	{
		if (player != null)
		{
			if (transform.localPosition.x - player.transform.localPosition.x <= range && transform.position.y - player.transform.localPosition.y <= range && transform.position.x - player.transform.localPosition.x >= -range && transform.position.y - player.transform.localPosition.y >= -range)
			{
				if (shooting == false)
				{
					StartCoroutine(Shoot());
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			//TODO: bugfix, this doesn't work for the first collision
			Physics2D.IgnoreCollision(collision.collider, GetComponentInChildren<Collider2D>(), true);
		}
	}

	private IEnumerator Shoot()
	{
		shooting = true;

		// Spawn a bullet with the player's current position as target
		GameObject bullet1 = Instantiate(bulletGO, transform.localPosition, Quaternion.identity, null);
		bullet1.GetComponent<Bullet>().Initialize(player.transform.localPosition, bulletSpeed);
		yield return new WaitForSeconds(1f);

		// Spawn a bullet with the player's current position as target
		GameObject bullet2 = Instantiate(bulletGO, transform.localPosition, Quaternion.identity, null);
		bullet2.GetComponent<Bullet>().Initialize(player.transform.localPosition, bulletSpeed);
		yield return new WaitForSeconds(1f);

		// Spawn a bullet with the player's current position as target
		GameObject bullet3 = Instantiate(bulletGO, transform.localPosition, Quaternion.identity, null);
		bullet3.GetComponent<Bullet>().Initialize(player.transform.localPosition, bulletSpeed);
		yield return new WaitForSeconds(5f);

		Teleport();
		shooting = false;
	}

	private void Teleport()
	{
		//TODO: Find a available place to teleport
		//TODO: Move there
	}
}