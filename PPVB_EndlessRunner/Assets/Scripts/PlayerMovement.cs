using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] float groundDistance = 0.1f;
    [SerializeField] ParticleSystem stunParticles;
    [SerializeField] ParticleSystem slowParticles;
    [SerializeField] float strength = 8f;
    bool jumping;
    bool stunned;

    [SerializeField] float moveSpeed = 1;
    float speed;

    //Making these editable variables so they can be changed per player
    [SerializeField] string horizontalAxisName = "Horizontal";
    [SerializeField] string jumpButton = "w";
    
    [SerializeField] float jumpForce = 1;
    float jump;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Transform bottomRight;

    [SerializeField] Animator animator;
    [SerializeField] Animator dust;

    [SerializeField] GameObject graphics;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = moveSpeed;
        jump = jumpForce;
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
        if (rb.gravityScale <= -1)
        {
            scale = new Vector3(-scale.x, scale.y, scale.z);
            //If i want to invert controls
            // horizontal = -horizontal;
        }
        // if (rb.gravityScale >= 1 || horizontal < 0) scale = new Vector3(-1, 1, 1);
        // else if (rb.gravityScale >= 1 || horizontal < 0) scale = new Vector3(-1, 1, 1);
        graphics.transform.localScale = scale;

        Vector2 targetVelocity = new Vector2(horizontal * speed, rb.velocity.y);
        Vector2 currentVelocity = rb.velocity;
        rb.AddForce((targetVelocity - currentVelocity) * strength);
        
        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    bool IsGrounded()
    {
        //Ray casting to see if either the left or right side of the player is close enough to the ground
        var ground1 = Physics2D.Raycast(bottomLeft.position, -transform.up, groundDistance);
        var ground2 = Physics2D.Raycast(bottomRight.position, -transform.up, groundDistance);
        return ground1.collider && ground1.collider.gameObject.CompareTag("Ground") ||
               ground2.collider && ground2.collider.gameObject.CompareTag("Ground");
    }

    // void Jump() => rb.velocity = new Vector2(rb.velocity.x, jump);
    void Jump() => rb.AddRelativeForce(new Vector2(rb.velocity.x, jump), ForceMode2D.Impulse);

    public IEnumerator Stunned(float time)
    {
        Debug.Log("Stun");
        stunned = true;
        speed = moveSpeed / 2;
        jump = jumpForce / 2;
        stunParticles.Play();
        yield return new WaitForSeconds(time / GameManager.instance.speed);
        stunned = false;
        speed = moveSpeed;
        jump = jumpForce;
        stunParticles.Stop();
        Debug.Log("Finished stun");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 14)
        {
            speed = moveSpeed / 3;
            jump = jumpForce / 2.5f;
            slowParticles.Play();
        }
        else if (!stunned && col.gameObject.layer == 6)
        {
            speed = moveSpeed;
            jump = jumpForce;
            slowParticles.Stop();
        }
    }
}