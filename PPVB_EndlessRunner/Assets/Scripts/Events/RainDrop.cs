using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RainDrop : MonoBehaviour
{
    [SerializeField] Transform stickyTile;
    [SerializeField] float speedDivider = 2f;
    Collider2D collider;
    float yPos;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        collider = GetComponent<Collider2D>();
        collider.enabled = false;

        //yPos = Random.Range(0, 2) == 0 ? 0.5f : -3.5f;

        yPos = Random.Range(0, 3) switch
        {
            0 => 6f,
            1 => 1.5f,
            2 => -3.5f,
            _ => 0
        };

        StartCoroutine(Kill());
    }

    void Update()
    {
        transform.position += -(Vector3.up * (gm.speed * speedDivider * Time.deltaTime));

        if (transform.position.y <= yPos) collider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 6) return;

        Instantiate(stickyTile, col.transform.position, quaternion.identity);
        Destroy(col.gameObject);
        Destroy(gameObject);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}