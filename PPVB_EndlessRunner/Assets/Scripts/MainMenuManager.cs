using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class MainMenuManager : MonoBehaviour
{
    GameManager gm;
    System.Diagnostics.Process _process;

    [SerializeField] TMP_InputField name_player1;
    [SerializeField] TMP_InputField name_player2;

    [SerializeField] GameObject player2Name;
    [SerializeField] GameObject multiplayerBorder;
    [SerializeField] GameObject singleBorder;

    public RectTransform leaderboard_ContentHolder;
    [SerializeField] Scrollbar lb_Scroll;

    void Start()
    {
        gm = GameManager.instance;
        gm.lb_Content = leaderboard_ContentHolder;
        gm.ResetValues();
        gm.SetupLeaderboard();
        SetBorder();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            lb_Scroll.value += 0.02f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            lb_Scroll.value -= 0.02f;
        lb_Scroll.value = Mathf.Clamp(lb_Scroll.value, 0, 1);

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
        Debug.Log("Setting border");
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

    public void SetMultiplayer(bool b)
    {
        gm.SetMultiplayer(b);
    }

    public void Play()
    {
        TriggerKeyboard(false);
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