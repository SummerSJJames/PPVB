using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;

    [SerializeField] TMP_Text multiplayerEnd;
    [SerializeField] TMP_Text singlePlayerEnd;

    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public void GameOver(bool multi)
    {
        gameOverScreen.SetActive(true);
        singlePlayerEnd.text = singlePlayerEnd.text.Replace("[SCORE]", GameManager.instance.score.ToString());
        multiplayerEnd.gameObject.SetActive(multi);
        singlePlayerEnd.gameObject.SetActive(!multi);
    }
    
    public void LoadScene(int index)
    {
        GameManager.instance.gameRunning = true;
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
