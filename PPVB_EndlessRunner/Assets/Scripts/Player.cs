using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool outsideBounds;
    SpriteRenderer sRenderer;
    [SerializeField] float outsideTimeLimit;
    PlayerManager manager;
    float timer;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        outsideBounds = !sRenderer.isVisible;

        if (outsideBounds)
        {
            timer += Time.deltaTime;

            if (timer >= outsideTimeLimit)
            {
                //Destroy(gameObject);
                manager.Lose(this);
            }
        }
        else timer = 0;

        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, 0) ;
        }
    }
}
