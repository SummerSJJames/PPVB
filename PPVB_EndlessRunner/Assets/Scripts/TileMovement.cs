using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    public float speed;

    void Update()
    {
        //Making sure the testing code only happens in editor
        #if (UNITY_EDITOR)
            if (GameManager.instance.testing) return;
        #endif
        speed = GameManager.instance.speed;
        //Continuously moving the tile left scaling speed with game speed 
        transform.position += direction * (speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destroy")) Destroy(gameObject);
    }
}