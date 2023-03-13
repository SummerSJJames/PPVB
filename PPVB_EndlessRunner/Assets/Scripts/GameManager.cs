using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static string player1;
    public static string player2;

    public bool testing;

    public bool gameRunning;

    public bool multiplayer;
    public float timePlayed;

    [HideInInspector] public int score;

    [SerializeField] float maxSpeed;
    [HideInInspector] public float speed = 1;

    [HideInInspector] public RectTransform lb_Content;
    [SerializeField] TMP_Text lb_PlayerEntryObject;
    AudioSource music;
    Dictionary<string, float> entries;

    string scoreFile = "score.txt";
    string folderPath = Directory.GetCurrentDirectory();
    string filePath;

    void Awake()
    {
        if (instance)
        {
            if (instance != this) Destroy(gameObject);
        }
        else instance = this;

        gameRunning = true;
        DontDestroyOnLoad(gameObject);

        filePath = Path.Combine(folderPath, scoreFile);
        entries = new Dictionary<string, float>();
        music = GetComponent<AudioSource>();
    }

    void AddEntry(string player)
    {
        //../..
        StreamWriter writer;

        // if (!File.Exists(filePath))
        writer = File.AppendText(filePath);

        if (!String.IsNullOrWhiteSpace(player))
            writer.WriteLine($"{player}:{score}");
        writer.Close();
    }

    public void SetupLeaderboard()
    {
        Debug.Log("Trying to set up leaderboard...");
        if (!File.Exists(filePath) || !lb_Content) return;
        Debug.Log("Setting up...");

        var reader = File.OpenText(filePath);

        int linesCount = File.ReadAllLines(filePath).Length;
        string[] leaderboardEntries = new string[linesCount];

        for (int i = 0; i < linesCount; i++)
        {
            var line = reader.ReadLine();
            if (String.IsNullOrWhiteSpace(line)) continue;

            string[] words = line.Split(':');
            if (words.Length != 2) continue;

            if (!float.TryParse(words[1], out var n)) continue;

            if (!entries.ContainsKey(words[0]))
            {
                entries.Add(words[0], n);
            }
            else if (n > entries[words[0]]) entries[words[0]] = n;
        }

        entries = entries.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        int count = 1;
        foreach (var kvp in entries)
        {
            var player = Instantiate(lb_PlayerEntryObject, lb_Content, false);
            player.text = $"{count}. {kvp.Key}\nscore: {kvp.Value}";
            count++;
        }

        LayoutRebuilder.MarkLayoutForRebuild(lb_Content);

        reader.Close();
    }

    public void ResetValues()
    {
        speed = 1f;
        score = 0;
        timePlayed = 0;
        music.pitch = 1f;
    }

    void Start()
    {
        ResetValues();
    }

    public void SetMultiplayer(bool b)
    {
        multiplayer = b;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        if (timePlayed < float.MaxValue) timePlayed += Time.deltaTime;
        else timePlayed = float.MaxValue;

        if (speed < maxSpeed) speed += Time.deltaTime * 0.1f;
        music.pitch = (2.5f / maxSpeed) * speed;
        music.pitch = Mathf.Clamp(music.pitch, 0.5f, 3);

        score = Mathf.RoundToInt(timePlayed * 10.75f);
    }

    public void GameOver()
    {
        FindObjectOfType<GameOverManager>().GameOver(multiplayer);
        if (!multiplayer) AddEntry(player1);
        gameRunning = false;
    }
}