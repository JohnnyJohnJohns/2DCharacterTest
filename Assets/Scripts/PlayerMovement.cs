using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float moveDir;
    private Rigidbody2D myRB;

    public SpriteRenderer mySprite;
    public float maxSpeed, moveSpeed, jumpForce;
    public Collider2D groundCheck;
    public LayerMask groundLayers;
    private bool canJump;
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDir > 0)
        {
            mySprite.flipX = false;
        }

        if (moveDir < 0)
        {
            mySprite.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        var moveAxis = Vector3.right * moveDir;

        if (Mathf.Abs(myRB.velocity.x) < maxSpeed)
        {
            myRB.AddForce(moveAxis * moveSpeed, ForceMode2D.Force);
        }

        if (groundCheck.IsTouchingLayers(groundLayers))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            if (context.started)
            {
                myRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (context.canceled && myRB.velocity.y > 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 0);
        }
    }
}
