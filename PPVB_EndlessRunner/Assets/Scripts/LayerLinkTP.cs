using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerLinkTP : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.25f, 0);
        }
        else if (transform.position.y >= 5.5f)
        {
            transform.position = new Vector3(transform.position.x, -5.25f, 0);
        }
    }
}