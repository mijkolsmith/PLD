using UnityEngine;
using Unity;
using Pathfinding;

public abstract class Enemy : MonoBehaviour, IDamageable
{
	public HealthComponent HealthComponent { get; set; }

	public void TakeDamage(int damage)
	{
		HealthComponent.health -= damage;
		if (HealthComponent.health == 0)
		{
			OnHealthZero();
		}
	}

	public void OnHealthZero()
	{
		Destroy(gameObject);
	}

	protected GameObject player;
	protected int range;
	protected int startHealth;
	private AIDestinationSetter destinationSetter;
	private IAstarAI ai;

	protected virtual void Start()
    {
		HealthComponent = new HealthComponent(startHealth);
		player = GameObject.Find("Terrain").GetComponent<LevelGenerator>().player;
		destinationSetter = GetComponent<AIDestinationSetter>();
		ai = GetComponent<IAstarAI>();
	}

	protected virtual void Update()
    {
		if (player != null)
		{
			if (transform.localPosition.x - player.transform.localPosition.x <= range && transform.position.y - player.transform.localPosition.y <= range && transform.position.x - player.transform.localPosition.x >= -range && transform.position.y - player.transform.localPosition.y >= -range)
			{
				destinationSetter.target = player.transform;
				ai.destination = player.transform.localPosition;
			}
			else
			{
				destinationSetter.target = null;
				ai.destination = transform.localPosition;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(collision.gameObject);
			TakeDamage(1);
		}
	}
}