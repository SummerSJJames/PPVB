using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorGrenade : MonoBehaviour
{
    [SerializeField] Animator animator;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    void Update()
    {
        animator.speed = gm.speed;
    }
}
