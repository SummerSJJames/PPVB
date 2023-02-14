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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDistance = GetComponent<Collider2D>().bounds.extents.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpButton) && IsGrounded())
        {
            Debug.Log("JUMP!");
            Jump();
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw(horizontalAxisName);
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 2f);
        return ground.collider.gameObject.layer == 6;
    }

    void Jump() => rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + -(Vector3.up * 2));
    }
}