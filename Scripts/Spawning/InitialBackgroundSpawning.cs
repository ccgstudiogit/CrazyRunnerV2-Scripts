using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBackgroundSpawning : MonoBehaviour
{
    private SpawnManager spawnManager;
    private BackgroundObjectSpawner backgroundObjectSpawner;
    private float thisLaneXPos;
    private float thisLaneYPos;
    private int distanceBuffer = 14; // Minimum distance between objects (same as InitialSpawning script)
    private int startingDistanceBuffer = 5; // (keep at multiples of 5)
    private int gaurenteedSpawn = 2; // Gaurenteed spawn after x empty spots
    private int cooldownInt = 2; // Obstacles cannot spawn within x positions from themselves
    private int resetCooldownInt;
    private int emptySpawnCounter;

    void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        backgroundObjectSpawner = GetComponentInParent<BackgroundObjectSpawner>();

        thisLaneXPos = transform.position.x;
        thisLaneYPos = transform.position.y;

        resetCooldownInt = cooldownInt;

        SpawnBackgroundObjects();
    }

    private void SpawnBackgroundObjects()
    {
        emptySpawnCounter = 0;
        cooldownInt = 0;

        for (int i = startingDistanceBuffer; i < spawnManager.zBound; i += distanceBuffer)
        {
            Vector3 spawnPos = new Vector3(thisLaneXPos, thisLaneYPos, i);

            if (cooldownInt <= 0)
            {
                if (SuccessfulSpawn(backgroundObjectSpawner.chanceForRareBackgroundObject))
                {
                    backgroundObjectSpawner.SpawnRareBackgroundObject(spawnPos);
                    emptySpawnCounter = 0;
                    cooldownInt = resetCooldownInt;
                    cooldownInt--;
                }

                else if (SuccessfulSpawn(backgroundObjectSpawner.chanceForBackgroundObject) || emptySpawnCounter >= gaurenteedSpawn)
                {
                    backgroundObjectSpawner.SpawnBackgroundObject(spawnPos);
                    emptySpawnCounter = 0;
                    cooldownInt = resetCooldownInt;
                    cooldownInt--;
                }

                else
                {
                    emptySpawnCounter++;
                    cooldownInt--;
                }
            }

            else
            {
                emptySpawnCounter++;
                cooldownInt--;
            }
        }
    }

    private bool SuccessfulSpawn(int oddsForSpawn)
    {
        int spawnSelection = GenerateRandomInt(oddsForSpawn);
        if (spawnSelection == 1)
            return true;
        else
            return false;
    }

    private int GenerateRandomInt(int oddsForSpawn)
    {
        int randomInt = Random.Range(0, oddsForSpawn + 1);
        return randomInt;
    }
}
