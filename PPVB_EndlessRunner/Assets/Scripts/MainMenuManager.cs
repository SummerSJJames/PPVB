using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    GameManager gm;
    
    [SerializeField] GameObject multiplayerBorder;
    [SerializeField] GameObject singleBorder;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        TouchScreenKeyboard.Open("Keyboard yis");
        SetBorder();
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            gm.multiplayer = false;
            SetBorder();
        }
        else if (Input.GetKeyDown("o"))
        {
            gm.multiplayer = true;
            SetBorder();
        }
    }

    void SetBorder()
    {
        if (gm.multiplayer)
        {
            multiplayerBorder.SetActive(true);
            singleBorder.SetActive(false);
        }
        else
        {
            multiplayerBorder.SetActive(false);
            singleBorder.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
