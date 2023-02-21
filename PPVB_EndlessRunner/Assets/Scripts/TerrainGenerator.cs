using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    bool generatingHole;
    bool canGenerateHole = true;
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
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator GenerateFloor()
    {
        yield return new WaitUntil(() => GameManager.instance.gameRunning);
        //bool generatingHole;
        int randomOdds = 10;
        lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
        while (true)
        {
            //We check if the last tile has moved left bij set amount;
            if (lastTile.position.x <= startPositionX - 0.45f)
            {
                //If the random decides we want a gap in the terrain
                if (canGenerateHole && Random.Range(0, randomOdds) == 0)
                {
                    generatingHole = true;
                    int odds = 2;
                    float distance = 1.35f;
                    while (generatingHole)
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
                            else generatingHole = false;
                        }

                        yield return null;
                    }
                    StartCoroutine(TimeBeforeHole());
                }
                else
                    lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
            }

            yield return null;
        }
    }

    IEnumerator TimeBeforeHole()
    {
        canGenerateHole = false;
        yield return new WaitForSeconds(Random.Range(0, 5));
        canGenerateHole = true;
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            if (!generatingHole)
            {
                Instantiate(obstacle, new Vector2(startPositionX, floorLevel + 0.7f), quaternion.identity);
                yield return new WaitForSeconds(Random.Range(2, 6));
            }
            else yield return null;
        }
    }

    Transform SpawnTile(Transform t, Vector2 pos, quaternion rot)
    {
        if (optionalSecondTerrain)
            Instantiate(tile, new Vector2(startPositionX, optionalSecondaryFloorLevel), quaternion.identity);
        return Instantiate(t, pos, rot);
    }
}