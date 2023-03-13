using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [HideInInspector] public Player player;
    [HideInInspector] public Vector2 throwDirection;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] GameObject itemHolder;
    [SerializeField] string useItemBtn;

    public I_Pickup heldItem;
    Transform heldItemTransform;

    [SerializeField] Transform arrow;

    void Start()
    {
        arrow.gameObject.SetActive(false);
        player = GetComponent<Player>();
    }

    void Update()
    {
        arrow.gameObject.SetActive(heldItem != null);
        itemHolder.SetActive(heldItem != null);
        if (heldItem == null || heldItemTransform == null) return;

        heldItemTransform.position = itemHolder.transform.position;
        arrow.Rotate(-Vector3.forward, rotationSpeed * (Time.deltaTime * 10));
        throwDirection = transform.TransformDirection(arrow.up);
        // Debug.Log(arrow.up);

        if (Input.GetKeyDown(useItemBtn))
        {
            heldItemTransform.parent = null;
            heldItemTransform.gameObject.layer = 13;
            heldItem.Use();
            heldItem = null;
            heldItemTransform = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (heldItem != null || col.gameObject.layer != 8) return;

        if (col.TryGetComponent<I_Pickup>(out var item))
        {
            heldItem = item;
            heldItemTransform = col.transform;
            heldItem.PickedUp(this);
            if (heldItemTransform.TryGetComponent<TileMovement>(out var m))
                Destroy(m);

            //Destroy(col.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, throwDirection);
    }
}