using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomEventsManager : MonoBehaviour
{
    [SerializeField] AudioSource three;
    [SerializeField] AudioSource two;
    [SerializeField] AudioSource one;
    [SerializeField] AudioSource flip;
    [SerializeField] AudioSource explosion;
    [SerializeField] AudioSource rain;

    [SerializeField] TMP_Text modeText;
    [SerializeField] Animator countdown;

    float TimeBeforeEvent;
    bool playing;

    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        TimeBeforeEvent = 5;
    }

    void Update()
    {
        if (playing) return;

        if (TimeBeforeEvent > 0)
            TimeBeforeEvent -= Time.deltaTime;
        else StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        playing = true;
        countdown.SetTrigger("Play");
        three.Play();
        yield return new WaitForSeconds(1);
        two.Play();
        yield return new WaitForSeconds(1);
        one.Play();
        yield return new WaitForSeconds(1);
        switch (Random.Range(0, 3))
        {
            case 0:
                flip.Play();
                modeText.text = "FLIP";
                break;
            case 1:
                explosion.Play();
                modeText.text = "EXPLOSION";
                break;
            case 2:
                rain.Play();
                modeText.text = "RAIN";
                break;
        }

        TimeBeforeEvent = 3f;
        playing = false;
    }
}