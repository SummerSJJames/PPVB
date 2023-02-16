using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    Player[] players;
    [SerializeField] float boundaryLeft;
    [SerializeField] float boundaryRight;

    void Start()
    {
        players = FindObjectsOfType<Player>();
    }

    void Update()
    {
        //foreach (var p in players)
        //{
            //p.outsideBounds = p.transform.position.x < boundaryLeft ||
                              //p.transform.position.x > boundaryRight;
        //}
    }
}