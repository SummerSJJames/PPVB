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

    PlayerMovement movement;
    
    [SerializeField] Transform boundaryBubble;
    [SerializeField] SpriteRenderer sRenderer;
    [SerializeField] float outsideTimeLimit;
    [SerializeField] TMP_Text countdown;
    PlayerManager manager;
    float timer;

    void Start()
    {
        timer = outsideTimeLimit;
        manager = FindObjectOfType<PlayerManager>();
        movement = GetComponent<PlayerMovement>();
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

            if (timer <= 0)
            {
                //Destroy(gameObject);
                manager.Lose(this);
            }
            else countdown.text = timer.ToString("0");
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

    public void Stun()
    {
        
    }
}
