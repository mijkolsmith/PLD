using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerController controller;
    public float horizontalMove = 0f;
    public float runSpeed = 40f;
    public bool jump = false;
    public bool crouch = false;
    bool canCrouch = false;
    private string horizontalString = "Player Horizontal";
    private string jumpString = "Player Jump";
    private string crouchString = "Player Crouch";
    private string shootString = "Player Shoot";

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: do you want to crouch? decide for later
        if (other.gameObject.name == "Ground")
        {
            canCrouch = false;
        }
        else
        {
            canCrouch = true;
        }
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw(horizontalString) * runSpeed;
        if(Input.GetButtonDown(jumpString))
        {
            jump = true;
        }
        if(Input.GetButtonDown(crouchString) && canCrouch)
        {
            crouch = true;
        }
        else if(Input.GetButtonUp(crouchString))
        {
            crouch = false;
        }
        if(Input.GetButtonDown(shootString))
		{
            controller.Shoot();
		}
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}