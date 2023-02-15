using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float groundDistance;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] string horizontalAxisName = "Horizontal";
    [SerializeField] string jumpButton = "w";
    [SerializeField] float jumpForce = 1;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Transform bottomRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDistance = GetComponent<Collider2D>().bounds.extents.y;
    }

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(jumpButton))
        {
            Debug.Log("JUMP!");
            if (IsGrounded())
            {
                Debug.Log("Should jump");
                Jump();
            }
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw(horizontalAxisName);
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    bool IsGrounded()
    {
        var ground1 = Physics2D.Raycast(bottomLeft.position, Vector2.down, 0.1f);
        var ground2 = Physics2D.Raycast(bottomRight.position, Vector2.down, 0.1f);
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