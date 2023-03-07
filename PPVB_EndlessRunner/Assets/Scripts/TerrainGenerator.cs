using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    bool generatingHole;
    [SerializeField] bool canGenerateHole = true;
    [SerializeField] Transform tile;
    [SerializeField] Transform obstacle;
    [SerializeField] float startPositionX;
    [SerializeField] float floorLevel;
    [SerializeField] bool dontGenerateObstacles;

    [SerializeField] bool optionalSecondTerrain;
    [SerializeField] float optionalSecondaryFloorLevel;
    float obstacleDelay;
    Transform lastTile;

    void Start()
    {
        //Making sure the testing code only happens in editor
#if (UNITY_EDITOR)
        if (GameManager.instance.testing) return;
#endif
        StartCoroutine(GenerateFloor());
        // if (!dontGenerateObstacles) StartCoroutine(SpawnObstacle());
    }

    IEnumerator GenerateFloor()
    {
        yield return new WaitUntil(() => GameManager.instance.gameRunning);
        //bool generatingHole;
        int randomOdds = 10;
        lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
        while (true)
        {
            //We check if the last tile has moved left by set amount;
            if (lastTile.position.x <= startPositionX - 0.45f)
            {
                //If the random decides we want a gap in the terrain
                float xpos = lastTile.position.x + 0.49f;
                if (canGenerateHole && Random.Range(0, randomOdds) == 0)
                {
                    generatingHole = true;
                    int odds = 2;
                    float distance = 1.35f;
                    xpos += 0.49f;
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
                                xpos += 0.49f;
                            }
                            else generatingHole = false;
                        }

                        yield return null;
                    }

                    StartCoroutine(TimeBeforeHole());
                }

                lastTile = SpawnTile(tile,
                    new Vector2(xpos, floorLevel), quaternion.identity);
                if (!dontGenerateObstacles) SpawnObstacle();
            }

            if (!lastTile)
                lastTile = SpawnTile(tile,
                    new Vector2(startPositionX, floorLevel), quaternion.identity);

            yield return null;
        }
    }

    IEnumerator TimeBeforeHole()
    {
        canGenerateHole = false;
        yield return new WaitForSeconds(Random.Range(1f, 6f) / GameManager.instance.speed);
        canGenerateHole = true;
    }

    void SpawnObstacle()
    {
        if (obstacleDelay > 0)
        {
            obstacleDelay -= Time.deltaTime;
            return;
        }
        Instantiate(obstacle, new Vector2(startPositionX, floorLevel + 0.7f), quaternion.identity);
        obstacleDelay = Random.Range(2f, 6f) / GameManager.instance.speed;
    }

    Transform SpawnTile(Transform t, Vector2 pos, quaternion rot)
    {
        if (optionalSecondTerrain)
            Instantiate(t, new Vector2(pos.x, optionalSecondaryFloorLevel), rot);
        return Instantiate(t, pos, rot);
    }
}