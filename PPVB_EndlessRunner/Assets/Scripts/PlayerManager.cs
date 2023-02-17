using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] multiplayerObjects;
    [SerializeField] GameObject[] players;
    //[SerializeField] Vector3[] spawnPoints;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;

        foreach (var obj in multiplayerObjects)
            obj.SetActive(gm.multiplayer);

        players[0].SetActive(true);
        players[1].SetActive(gm.multiplayer);
    }

    public void Lose(Player player)
    {
        Debug.Log("Game over");
        foreach (var p in players)
        {
            p.GetComponent<Player>().enabled = false;
        }
        
        GameManager.instance.GameOver();
    }
}