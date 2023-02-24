using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StunGrenade : Throwable
{
    [SerializeField] float blastRadius;
    [SerializeField] float explosionForce = 1.5f;
    [SerializeField] GameObject explosion;
    public bool hasLanded;

    protected override void Landed()
    {
        if (hasLanded) return;
        hasLanded = true;
        Explode();
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, quaternion.identity);
        List<Player> players = new List<Player>();
        //Get every player in the blast radius
        Array.ForEach(Physics2D.OverlapCircleAll(transform.position, blastRadius), x =>
        {
            if (x.TryGetComponent<Player>(out var p)) players.Add(p);
        });
        //Stun every player and knock them back
        foreach (var p in players)
        {
            Debug.Log(p.name);
            var dir = (p.transform.position - transform.position);
            var multiplier = explosionForce / dir.magnitude;
            multiplier = Mathf.Clamp(multiplier, 1f, explosionForce);
            p.GetComponent<Rigidbody2D>()
                .AddRelativeForce((p.transform.position - transform.position).normalized * multiplier,
                    ForceMode2D.Impulse);
            p.Stun();
        }

        Destroy(gameObject);
    }
}