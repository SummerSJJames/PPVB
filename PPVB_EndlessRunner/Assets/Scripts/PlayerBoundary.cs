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
        for (int i = 0; i < players.Length; i++)
        {
            players[i].outsideBounds = players[i].transform.position.x < boundaryLeft ||
                                       players[i].transform.position.x > boundaryRight;
        }
    }
}