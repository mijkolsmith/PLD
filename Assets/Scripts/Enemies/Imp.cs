using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy
{
	protected int range = 20;
	protected override void Start()
	{
		base.range = range;
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}
}