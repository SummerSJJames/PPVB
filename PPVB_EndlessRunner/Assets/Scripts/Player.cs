using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool outsideBounds;

    void Update()
    {
        if (outsideBounds)
        {
            Debug.Log("PLAYER IS OUTSIDE BOUNDS!");
        }
    }
}
