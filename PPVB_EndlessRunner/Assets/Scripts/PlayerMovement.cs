using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float groundDistance = 0.1f;
    [SerializeField] float moveSpeed = 1;
    //Making these editable variables so they can be changed per player
    [SerializeField] string horizontalAxisName = "Horizontal";
    [SerializeField] string jumpButton = "w";
    [SerializeField] float jumpForce = 1;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Transform bottomRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //groundDistance = GetComponent<Collider2D>().bounds.extents.y;
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(jumpButton) && IsGrounded())
            Jump();
    }

    void MovePlayer()
    {
        var horizontal = Input.GetAxisRaw(horizontalAxisName);
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
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