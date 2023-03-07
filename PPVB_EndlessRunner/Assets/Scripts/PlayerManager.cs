using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] multiplayerObjects;
    [SerializeField] Player[] players;

    [SerializeField] TMP_Text playerText;
    [SerializeField] TMP_Text scoreText;
    //[SerializeField] Vector3[] spawnPoints;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        gm.ResetValues();

        foreach (var obj in multiplayerObjects)
            obj.SetActive(gm.multiplayer);

        if (!string.IsNullOrWhiteSpace(GameManager.player1)) players[0].playerName = GameManager.player1;
        if (!string.IsNullOrWhiteSpace(GameManager.player2)) players[1].playerName = GameManager.player2;

        players[0].gameObject.SetActive(true);
        players[1].gameObject.SetActive(gm.multiplayer);
        
        scoreText.gameObject.SetActive(!gm.multiplayer);
    }

    void Update()
    {
        if (gm.multiplayer) return;

        if (gm.gameRunning) scoreText.text = gm.score.ToString();
    }

    public void Lose(Player player)
    {
        Debug.Log("Game over");
        foreach (var p in players)
        {
            p.GetComponent<Player>().enabled = false;
            if (p != player) playerText.text = playerText.text.Replace("[PLAYER]", p.playerName);
        }

        GameManager.instance.GameOver();
    }
}