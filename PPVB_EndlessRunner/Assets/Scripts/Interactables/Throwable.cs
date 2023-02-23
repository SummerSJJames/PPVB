using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour, I_Pickup
{
    [SerializeField] float throwForce;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public ItemHandler player;

    int groundLayer = 6;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PickedUp(ItemHandler p)
    {
        player = p;
        gameObject.layer = 9;
    }

    public virtual void Use()
    {
        direction = player.throwDirection;
        Debug.Log(direction);
        StartCoroutine(Move());
    }

    protected virtual IEnumerator Move()
    {
        rb.AddRelativeForce(direction * throwForce);
        
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    protected virtual void Landed()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!player || player.heldItem == this) return;
        
        Landed();
    }
}
