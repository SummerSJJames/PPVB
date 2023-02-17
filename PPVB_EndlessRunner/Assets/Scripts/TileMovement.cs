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
        transform.position += direction * (speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destroy")) Destroy(gameObject);
    }
}
