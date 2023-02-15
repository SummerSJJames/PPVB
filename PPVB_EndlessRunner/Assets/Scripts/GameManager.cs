using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool multiplayer;

    void Awake()
    {
        if (instance)
        {
            if (instance != this) Destroy(gameObject);
        }
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetMultiplayer(bool b)
    {
        multiplayer = b;
    }
}