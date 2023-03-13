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
    int obstacleDelay;
    int objectDelay;
    [SerializeField] GameObject[] multiplayerObjectsToSpawn;
    [SerializeField] GameObject[] singleplayerObjectsToSpawn;
    [SerializeField] float objectsSpawnPosY;
    Transform lastTile;
    GameManager gm;

    void Start()
    {
        //Making sure the testing code only happens in editor
        gm = GameManager.instance;
#if (UNITY_EDITOR)
        if (gm.testing) return;
#endif
        obstacleDelay = Random.Range(0, 3);
        objectDelay = Random.Range(0, 3);
        StartCoroutine(GenerateFloor());
        // if (!dontGenerateObstacles) StartCoroutine(SpawnObstacle());
    }

    IEnumerator GenerateFloor()
    {
        yield return new WaitUntil(() => gm.gameRunning);
        //bool generatingHole;
        int randomOdds = 10;
        lastTile = SpawnTile(tile, new Vector2(startPositionX, floorLevel), quaternion.identity);
        while (true)
        {
            //We check if the last tile has moved left by set amount;
            if (lastTile.position.x <= startPositionX - 0.45f)
            {
                //If the random decides we want a gap in the terrain
                float xPos = lastTile.position.x + 0.49f;
                if (canGenerateHole && Random.Range(0, randomOdds) == 0)
                {
                    generatingHole = true;
                    int odds = 2;
                    float distance = 1.35f;
                    xPos += 0.49f;
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
                                xPos += 0.49f;
                            }
                            else generatingHole = false;
                        }

                        yield return null;
                    }

                    StartCoroutine(TimeBeforeHole());
                }

                lastTile = SpawnTile(tile,
                    new Vector2(xPos, floorLevel), quaternion.identity);
                objectDelay--;
                obstacleDelay--;
                if (!dontGenerateObstacles)
                {
                    if (Random.Range(0, 2) == 0)
                        SpawnObstacle(xPos);
                    else SpawnObject(xPos);
                }
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
        yield return new WaitForSeconds(Random.Range(1f, 6f) / gm.speed);
        canGenerateHole = true;
    }

    void SpawnObject(float x)
    {
        if (objectDelay > 0) return;

        // Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)],
        //     new Vector2(x, objectsSpawnPosY[Random.Range(0, objectsSpawnPosY.Length)]),
        //     quaternion.identity);
        var height = objectsSpawnPosY;
        var spawnHeight = Random.Range(0, 2) == 0 ? height : -height;
        var obj = gm.multiplayer
            ? multiplayerObjectsToSpawn[Random.Range(0, multiplayerObjectsToSpawn.Length)]
            : singleplayerObjectsToSpawn[Random.Range(0, singleplayerObjectsToSpawn.Length)];
        var ob = Instantiate(obj, new Vector2(x, spawnHeight), quaternion.identity);
        if (Math.Abs(spawnHeight - (-height)) < 0.1)
        {
            ob.transform.Rotate(Vector3.forward, 180);
            if (ob.CompareTag("Log") && ob.TryGetComponent<Rigidbody2D>(out var rigid))
                rigid.gravityScale = -1;
        }

        objectDelay = Random.Range(20, 60);
    }

    void SpawnObstacle(float x)
    {
        if (obstacleDelay > 0) return;
        var height = floorLevel + 0.7f;
        var spawnHeight = Random.Range(0, 2) == 0 ? height : -height;
        var ob = Instantiate(obstacle, new Vector2(x, spawnHeight), quaternion.identity);
        if (Math.Abs(spawnHeight - (-height)) < 0.1) ob.Rotate(Vector3.forward, 180);
        obstacleDelay = Random.Range(10, 35);
    }

    Transform SpawnTile(Transform t, Vector2 pos, quaternion rot)
    {
        if (optionalSecondTerrain)
            Instantiate(t, new Vector2(pos.x, optionalSecondaryFloorLevel), rot);
        return Instantiate(t, pos, rot);
    }
}