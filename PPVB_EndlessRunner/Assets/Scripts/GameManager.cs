using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameRunning;

    public bool multiplayer;
    public float timePlayed;

    [SerializeField] float maxSpeed;
    [HideInInspector] public float speed = 1;

    void Awake()
    {
        if (instance)
        {
            if (instance != this) Destroy(gameObject);
        }
        else instance = this;

        gameRunning = true;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        speed = 1f;
    }

    public void SetMultiplayer(bool b)
    {
        multiplayer = b;
    }

    void Update()
    {
        if (timePlayed < float.MaxValue) timePlayed += Time.deltaTime;
        else timePlayed = float.MaxValue;

        if (speed < maxSpeed) speed += Time.deltaTime * 0.1f;
    }

    public void GameOver()
    {
        FindObjectOfType<GameOverManager>().gameOverScreen.SetActive(true);
        gameRunning = false;
    }
}