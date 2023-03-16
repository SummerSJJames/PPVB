using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rain : MonoBehaviour
{
    [SerializeField] GameObject drop;
    [SerializeField] float leftOfScreen;
    [SerializeField] float rightOfScreen;
    randomEvent currentEvent;
    bool playing;

    void OnEnable()
    {
        RandomEventsManager.ChooseEvent += PlayEvent;
    }

    void OnDisable()
    {
        RandomEventsManager.ChooseEvent -= PlayEvent;
    }

    void PlayEvent(randomEvent e)
    {
        currentEvent = e;
        if (currentEvent != randomEvent.rain)
        {
            StopAllCoroutines();
            playing = false;
            return;
        }
        
        if (!playing) StartCoroutine(PlayRain());
    }

    IEnumerator PlayRain()
    {
        playing = true;
        while (currentEvent == randomEvent.rain)
        {
            Debug.Log("Raining");
            Instantiate(drop, new Vector2(Random.Range(leftOfScreen, rightOfScreen), 5.5f), quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.35f) / GameManager.instance.speed);
        }
        playing = false;
    }
}