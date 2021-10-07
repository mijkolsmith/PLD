public class Bat : Enemy
{
	private new readonly int startHealth = 2;
	private new readonly int range = 20;

	protected override void Start()
	{
		base.startHealth = startHealth;
		base.range = range;
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}
}