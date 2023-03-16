using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Throwable : MonoBehaviour, I_Pickup
{
    [SerializeField] float throwForce;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public ItemHandler player;

    public Rigidbody2D rb;

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
        
        StartCoroutine(Move());
    }

    protected virtual IEnumerator Move()
    {
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(5f);
        Landed();
    }

    protected virtual void Landed()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!player || player.heldItem == this) return;

        Landed();
    }
}