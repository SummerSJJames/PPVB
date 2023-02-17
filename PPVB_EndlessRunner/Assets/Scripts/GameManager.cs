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

    public void SetMultiplayer(bool b)
    {
        multiplayer = b;
    }

    void Update()
    {
        if (timePlayed < float.MaxValue) timePlayed += Time.deltaTime;
        else timePlayed = float.MaxValue;
    }

    public void GameOver()
    {
        FindObjectOfType<GameOverManager>().gameOverScreen.SetActive(true);
        gameRunning = false;
    }
}
