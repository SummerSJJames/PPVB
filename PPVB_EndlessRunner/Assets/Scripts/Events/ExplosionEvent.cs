using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionEvent : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject indicator;
    [SerializeField] Vector2 leftOfScreen;
    [SerializeField] Vector2 rightOfScreen;
    [SerializeField] float explosionForce = 2f;
    [SerializeField] float blastRadius = 2.25f;
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
        if (currentEvent != randomEvent.explosion)
        {
            playing = false;
            return;
        }
        
        if (!playing) StartCoroutine(Explode());
    }


    IEnumerator Explode()
    {
        playing = true;
        while (currentEvent == randomEvent.explosion)
        {
            //Spawning explosions in a random position;
            var pos = new Vector2(Random.Range(leftOfScreen.x, rightOfScreen.x),
                Random.Range(leftOfScreen.y, rightOfScreen.y));
            var ind = Instantiate(indicator, pos, quaternion.identity);
            yield return new WaitForSeconds(1.75f / GameManager.instance.speed);

            var exp = Instantiate(explosion, pos, quaternion.identity);
            Destroy(ind);
            
            List<Player> players = new List<Player>();
            //Get every player in the blast radius
            Array.ForEach(Physics2D.OverlapCircleAll(exp.transform.position, blastRadius), x =>
            {
                if (x.TryGetComponent<Player>(out var p)) players.Add(p);
            });
            //Stun every player and knock them back
            foreach (var p in players)
            {
                Debug.Log(p.name);
                var dir = (p.transform.position - exp.transform.position);
                var multiplier = explosionForce / dir.magnitude;
                multiplier = Mathf.Clamp(multiplier, 1f, explosionForce);
                p.GetComponent<Rigidbody2D>()
                    .AddForce(dir.normalized * multiplier,
                        ForceMode2D.Impulse);
                p.Stun();
            }
            yield return new WaitForSeconds(Random.Range(1.5f, 3f) / GameManager.instance.speed);
        }
        playing = false;
    } 
}