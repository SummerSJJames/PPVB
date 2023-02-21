using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class MainMenuManager : MonoBehaviour
{
    GameManager gm;
    System.Diagnostics.Process _process;

    [SerializeField] TMP_Text name_player1;
    [SerializeField] TMP_Text name_player2;

    [SerializeField] GameObject player2Name;
    [SerializeField] GameObject multiplayerBorder;
    [SerializeField] GameObject singleBorder;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        SetBorder();
    }

    void Update()
    {
        if (_process != null) return;
        if (Input.GetKeyDown("i"))
        {
            gm.SetMultiplayer(false);
            SetBorder();
        }
        else if (Input.GetKeyDown("o"))
        {
            gm.SetMultiplayer(true);
            SetBorder();
        }
    }

    public void SetBorder()
    {
        if (gm.multiplayer)
        {
            multiplayerBorder.SetActive(true);
            singleBorder.SetActive(false);
            player2Name.SetActive(true);
        }
        else
        {
            multiplayerBorder.SetActive(false);
            singleBorder.SetActive(true);
            player2Name.SetActive(false);
        }
    }

    public void Play()
    {
        TriggerKeyboard(false);
        if (string.IsNullOrEmpty(name_player1.text))
            name_player1.text = "Player 1";

        if (string.IsNullOrEmpty(name_player2.text))
            name_player2.text = "Player 2";

        GameManager.player1 = name_player1.text;
        GameManager.player2 = name_player2.text;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TriggerKeyboard(bool visible)
    {
        if (visible && _process == null)
            _process = System.Diagnostics.Process.Start("osk.exe");
        else if (_process != null)
        {
            if (!_process.HasExited)
                _process.Kill();
            _process = null;
        }
    }
}