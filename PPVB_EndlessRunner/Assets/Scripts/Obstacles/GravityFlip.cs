using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 3) return;
        Debug.Log("FLIP");

        var player = col.gameObject.GetComponent<PlayerMovement>();
        
        //Invert the players current gravity scale
        player.rb.gravityScale = -player.rb.gravityScale;

        player.transform.Rotate(Vector3.forward, 180);
    }
}
