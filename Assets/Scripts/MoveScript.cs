using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public PlayerController controller;
    public float horizontalMove = 0f;
    public float runSpeed = 40f;
    public bool jump = false;
    public bool crouch = false;
    bool canCrouch = false;
    private string horizontalString;
    private string jumpString;
    private string crouchString;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        horizontalString = "Player Horizontal";
        jumpString = "Player Jump";
        crouchString = "Player Crouch";
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
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}