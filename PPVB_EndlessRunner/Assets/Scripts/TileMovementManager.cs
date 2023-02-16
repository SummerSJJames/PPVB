using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovementManager : MonoBehaviour
{
    public TileMovementManager instance;
    public float speed = 1;

    void Awake()
    {
        if (!instance) instance = this;
    }
}
