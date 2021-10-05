using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour
{
	private GameObject player;
	private AIDestinationSetter destinationSetter;
	private IAstarAI ai;
	protected int range;

	protected virtual void Start()
    {
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
}