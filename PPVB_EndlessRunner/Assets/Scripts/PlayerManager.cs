using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] multiplayerObjects;
    [SerializeField] TMP_Text playerText;
    [SerializeField] Player[] players;
    //[SerializeField] Vector3[] spawnPoints;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;

        foreach (var obj in multiplayerObjects)
            obj.SetActive(gm.multiplayer);

        players[0].playerName = GameManager.player1;
        players[1].playerName = GameManager.player2;
        
        players[0].gameObject.SetActive(true);
        players[1].gameObject.SetActive(gm.multiplayer);
    }

    public void Lose(Player player)
    {
        Debug.Log("Game over");
        foreach (var p in players)
        {
            p.GetComponent<Player>().enabled = false;
            if (p != player) playerText.text = playerText.text.Replace("[Player]", p.playerName);
        }

        GameManager.instance.GameOver();
    }
}