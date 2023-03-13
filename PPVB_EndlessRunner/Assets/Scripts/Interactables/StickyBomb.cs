using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StickyBomb : Throwable
{
    [SerializeField] float blastRadius;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject stickyTile;
    public bool hasLanded;

    protected override void Landed()
    {
        if (hasLanded) return;
        hasLanded = true;
        Explode();
    }
//6
    private void Explode()
    {
        Instantiate(explosion, transform.position, quaternion.identity);
        List<GameObject> tiles = new List<GameObject>();
        //Get every player in the blast radius
        Array.ForEach(Physics2D.OverlapCircleAll(transform.position, blastRadius), x =>
        {
            if (x.gameObject.layer == 6) tiles.Add(x.gameObject);
        });
        //Stun every player and knock them back
        foreach (var t in tiles)
        {
            Instantiate(stickyTile, t.transform.position, quaternion.identity);
            Destroy(t);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, direction);
    }
}
