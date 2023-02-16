using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] Transform focusedPlayer;
    
    [SerializeField] GameObject tile;
    [SerializeField] float startPositionX;
    [SerializeField] float top;
    [SerializeField] float bottom;
}
