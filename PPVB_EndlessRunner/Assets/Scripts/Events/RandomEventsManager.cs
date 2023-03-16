using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum randomEvent
{
    flip,
    explosion,
    rain
};

public class RandomEventsManager : MonoBehaviour
{
    [SerializeField] AudioSource three;
    [SerializeField] AudioSource two;
    [SerializeField] AudioSource one;
    [SerializeField] AudioSource flip;
    [SerializeField] AudioSource explosion;
    [SerializeField] AudioSource rain;

    [SerializeField] TMP_Text modeText;
    [SerializeField] GameObject countDownText;
    [SerializeField] Animator countdown;

    float TimeBeforeEvent;
    bool playing;

    GameManager gm;

    public delegate void OnChooseEvent(randomEvent e);

    public static OnChooseEvent ChooseEvent;

    void Start()
    {
        gm = GameManager.instance;
        TimeBeforeEvent = 5;
        ChooseEvent += PickEvent;
    }

    void PickEvent(randomEvent e)
    {
    }

    void Update()
    {
        if (!gm.gameRunning)
        {
            countDownText.SetActive(false);
            StopAllCoroutines();
            return;
        }
        if (playing) return;

        if (TimeBeforeEvent > 0)
            TimeBeforeEvent -= Time.deltaTime;
        else
        {
            //Randomly choosing an event
            var ev = Random.Range(0, 3) switch
            {
                0 => randomEvent.flip,
                1 => randomEvent.explosion,
                2 => randomEvent.rain,
                _ => randomEvent.flip
            };
            StartCoroutine(CountDown(ev));
        }
    }


    IEnumerator CountDown(randomEvent e)
    {
        //Playing the right audio and counting down then calling the events
        playing = true;
        countdown.SetTrigger("Play");
        three.Play();
        yield return new WaitForSeconds(1);
        two.Play();
        yield return new WaitForSeconds(1);
        one.Play();
        yield return new WaitForSeconds(1);
        switch (e)
        {
            case randomEvent.flip:
                flip.Play();
                modeText.text = "FLIP";
                break;
            case randomEvent.explosion:
                explosion.Play();
                modeText.text = "EXPLOSION";
                break;
            case randomEvent.rain:
                rain.Play();
                modeText.text = "RAIN";
                break;
            default:
                break;
        }
        ChooseEvent?.Invoke(e);
        yield return new WaitForSeconds(1.25f);
        TimeBeforeEvent = 15 / gm.speed;
        playing = false;
    }
}