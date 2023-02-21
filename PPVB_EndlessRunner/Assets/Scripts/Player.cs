using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool outsideBounds;
    public string playerName;

    [SerializeField] Transform boundaryBubble;
    SpriteRenderer sRenderer;
    [SerializeField] float outsideTimeLimit;
    [SerializeField] TMP_Text countdown;
    PlayerManager manager;
    float timer;

    void Start()
    {
        timer = outsideTimeLimit;
        sRenderer = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<PlayerManager>();
        boundaryBubble.gameObject.SetActive(false);
        countdown.text = "3";
        GameManager.instance.speed = 1f;
    }

    void Update()
    {
        outsideBounds = !sRenderer.isVisible;

        if (outsideBounds)
        {
            boundaryBubble.gameObject.SetActive(true);
            boundaryBubble.position = new Vector3(-8, transform.position.y, 0);
            timer -= Time.deltaTime;
            countdown.text = timer.ToString("0");

            if (timer <= 0)
            {
                //Destroy(gameObject);
                manager.Lose(this);
            }
        }
        else
        {
            timer = outsideTimeLimit;
            boundaryBubble.gameObject.SetActive(false);
        }

        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.25f, 0) ;
        }
    }
}
