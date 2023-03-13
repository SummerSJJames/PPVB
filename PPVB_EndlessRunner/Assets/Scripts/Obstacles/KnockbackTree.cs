using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTree : MonoBehaviour
{
    [SerializeField] float knockbackForce;
    Vector3 dir;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3) return;
        Debug.Log("Knock back Player!");
        dir = (collision.gameObject.transform.position - transform.position).normalized;
        collision.rigidbody.AddRelativeForce(new Vector2(dir.x * knockbackForce, dir.y * knockbackForce / 5), ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, dir);
    }
}