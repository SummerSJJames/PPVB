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

        //Making it randomly choose a y position to activate its collider at so it can make all the layers sticky

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
        //Moving it down
        transform.position += -(Vector3.up * (gm.speed * speedDivider * Time.deltaTime));

        if (transform.position.y <= yPos) collider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 6) return;
        //Spawning a sticky tile when it detects it has collided with a tile
        Instantiate(stickyTile, col.transform.position, quaternion.identity);
        Destroy(col.gameObject);
        Destroy(gameObject);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(5f);
        //Auto destroy in case no collision is detected
        Destroy(gameObject);
    }
}