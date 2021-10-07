using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public HealthComponent HealthComponent { get; set; }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            HealthComponent.health -= damage;
            if (HealthComponent.health == 0)
            {
                OnHealthZero();
            }
            StartCoroutine(Invincibility());
        }
    }

    private bool invincible = false;
    SpriteRenderer sr;
    private IEnumerator Invincibility()
	{
        invincible = true;
		for (int i = 0; i < 10; i++)
		{
            sr.enabled = false;
            yield return new WaitForSeconds(.1f);
            sr.enabled = true;
        }
        invincible = false;
	}

    public void OnHealthZero()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public PlayerController controller;
    public float horizontalMove = 0f;
    public float runSpeed = 40f;
    public bool jump = false;
    public bool crouch = false;
    private bool canCrouch = false;
    private readonly string horizontalString = "Player Horizontal";
    private readonly string jumpString = "Player Jump";
    private readonly string crouchString = "Player Crouch";
    private readonly string shootString = "Player Shoot";
    public GameObject bulletGO;
    private float bulletSpeed = 15;
    private bool shooting = false;

    private void Start()
    {
        HealthComponent = new HealthComponent(10);
        controller = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: do you want to crouch? decide for later
        if (collision.gameObject.name == "Ground")
        {
            canCrouch = false;
        }
        else
        {
            canCrouch = true;
        }
        if (collision.gameObject.layer == 6)
        { // Layer 6 is lava
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Enemy"))
		{
            TakeDamage(1);
		}
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw(horizontalString) * runSpeed;
        if (Input.GetButtonDown(jumpString))
        {
            jump = true;
        }
        if (Input.GetButtonDown(crouchString) && canCrouch)
        {
            crouch = true;
        }
        else if (Input.GetButtonUp(crouchString))
        {
            crouch = false;
        }
        if (Input.GetButton(shootString) == true && shooting == false)
		{
            StartCoroutine(Shoot());
		}
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public IEnumerator Shoot()
    {
        shooting = true;
        GameObject bullet = Instantiate(bulletGO, transform.localPosition, Quaternion.identity, null);
        bullet.GetComponent<Bullet>().Initialize(Camera.main.ScreenToWorldPoint(Input.mousePosition), bulletSpeed);
        yield return new WaitForSeconds(.2f);
        shooting = false;
    }
}