using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool outsideBounds;
    public string playerName;

    [SerializeField] Transform boundaryBubble;
    SpriteRenderer sRenderer;
    [SerializeField] float outsideTimeLimit;
    PlayerManager manager;
    float timer;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<PlayerManager>();
        boundaryBubble.gameObject.SetActive(false);
    }

    void Update()
    {
        outsideBounds = !sRenderer.isVisible;

        if (outsideBounds)
        {
            boundaryBubble.gameObject.SetActive(true);
            boundaryBubble.position = new Vector3(-8, transform.position.y, 0);
            timer += Time.deltaTime;

            if (timer >= outsideTimeLimit)
            {
                //Destroy(gameObject);
                manager.Lose(this);
            }
        }
        else
        {
            timer = 0;
            boundaryBubble.gameObject.SetActive(false);
        }

        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, 0) ;
        }
    }
}
