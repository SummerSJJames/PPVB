using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float groundDistance = 0.1f;
    bool jumping;

    [SerializeField] float moveSpeed = 1;

    //Making these editable variables so they can be changed per player
    [SerializeField] string horizontalAxisName = "Horizontal";
    [SerializeField] string jumpButton = "w";
    [SerializeField] float jumpForce = 1;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Transform bottomRight;

    [SerializeField] Animator animator;
    [SerializeField] Animator dust;

    [SerializeField] GameObject graphics;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //groundDistance = GetComponent<Collider2D>().bounds.extents.y;
    }

    void Update()
    {
        if (!GameManager.instance.gameRunning) return;
        
        MovePlayer();

        if (IsGrounded())
        {
            jumping = false;
            animator.SetTrigger("Run");
            dust.SetTrigger("Run");
            if (Input.GetKeyDown(jumpButton))
            {
                Jump();
                jumping = true;
                animator.SetTrigger("Jump");
                dust.SetTrigger("Jump");
            }
        }
        else if (!jumping)
        {
            animator.SetTrigger("Fall");
            dust.SetTrigger("Fall");
        }
    }

    void MovePlayer()
    {
        var horizontal = Input.GetAxisRaw(horizontalAxisName);
        Vector3 scale = new Vector3(horizontal < 0 ? -1 : 1, 1, 1);
        graphics.transform.localScale = scale;

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // if (horizontal > 0) sRenderer.flipX = false;
        // else if (horizontal < 0) sRenderer.flipX = true;
    }

    bool IsGrounded()
    {
        //Ray casting to see if either the left or right side of the player is close enough to the ground
        var ground1 = Physics2D.Raycast(bottomLeft.position, Vector2.down, groundDistance);
        var ground2 = Physics2D.Raycast(bottomRight.position, Vector2.down, groundDistance);
        return ground1.collider && ground1.collider.gameObject.CompareTag("Ground") ||
               ground2.collider && ground2.collider.gameObject.CompareTag("Ground");
    }

    void Jump() => rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position, transform.position + -(Vector3.up * 2f));
    // }
}