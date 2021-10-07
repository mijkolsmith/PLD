using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 dir;
    private float speed;
	public void Initialize(Vector3 endPos, float speed)
	{
		dir = (endPos - transform.localPosition).normalized;
        this.speed = speed;
    }

	private void Update()
    {
        transform.localPosition += dir * Time.deltaTime * speed;
        //TODO: delete the bullets / object pool
    }
}