using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float moveSpeed = 40.0f;

    private float horizontalMovement = 0.0f;
    private bool isJumping = false;

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetButton("Jump"))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMovement * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }
}
