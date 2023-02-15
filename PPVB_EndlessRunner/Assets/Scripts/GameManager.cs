using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool multiplayer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMultiplayer(bool b)
    {
        multiplayer = b;
    }
}
