using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] Vector3 direction;

    [SerializeField] Transform left;
    [SerializeField] Transform right;

    float goalPosX;
    Vector3 startPos;

    Transform target;

    void Start()
    {
        goalPosX = left.position.x;
        startPos = right.position;
        target = right;
    }

    void Update()
    {
        transform.position += direction * ((speed * Time.deltaTime) * GameManager.instance.speed);

        //We check if the target has reached the position all the way on the left
        if (target.position.x <= goalPosX)
        {
            //Check which one we have to teleport to the right
            var moveTarget = target == left ? right : left;
            moveTarget.position = startPos;
            //Set the new target
            target = moveTarget;
        }
    }
}