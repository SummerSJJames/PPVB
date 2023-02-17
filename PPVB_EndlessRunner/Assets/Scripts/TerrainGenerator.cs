using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] Transform tile;
    [SerializeField] Transform obstacle;
    [SerializeField] float startPositionX;
    [SerializeField] float floorLevel;
    
    [SerializeField] bool optionalSecondTerrain;
    [SerializeField] float optionalSecondaryFloorLevel;
    Transform lastTile;

    void Start()
    {
        StartCoroutine(GenerateFloor());
    }

    IEnumerator GenerateFloor()
    {
        yield return new WaitUntil(() => GameManager.instance.gameRunning);
        bool random;
        int randomOdds = 5;
        lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
        while (GameManager.instance.gameRunning)
        {
            //We check if the last tile has moved left bij set amount;
            if (lastTile.position.x <= startPositionX - 0.45f)
            {
                lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
                //If the random decides we want a gap in the terrain
                if (Random.Range(0, randomOdds) == 0)
                {
                    random = true;
                    int odds = 1;
                    float distance = 0.45f;
                    while (random)
                    {
                        //If the last tile has moved by the distance
                        if (lastTile.position.x <= startPositionX - distance)
                        {
                            //When it has, we check if we want there to be a gap again;
                            if (Random.Range(0, odds) == 0)
                            {
                                //We decrease the odds and increase the distance the block has to travel
                                odds++;
                                distance += 0.45f;
                            }
                            else random = false;
                        }

                        yield return null;
                    }
                }
                else
                    lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
            }

            yield return null;
        }
    }

    void SpawnObstacle()
    {
        
    }

    Transform SpawnTile(Transform t, Vector2 pos, quaternion rot)
    {
        if (optionalSecondTerrain)
            Instantiate(tile, new Vector2(startPositionX, optionalSecondaryFloorLevel), quaternion.identity);
        return Instantiate(t, pos, rot);
    }
}