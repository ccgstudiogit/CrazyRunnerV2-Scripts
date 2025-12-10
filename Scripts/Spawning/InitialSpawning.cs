using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpawning : MonoBehaviour
{
    // General Variables
    private SpawnManager spawnManager;
    private ObstacleSpawner obstacleSpawner;
    private CoinSpawner coinSpawner;
    private float thisLaneXPos;
    private float thisLaneYPos;
    private int distanceBuffer = 14; // Minimum distance between objects
    private int startingDistanceBuffer = 20; // Makes sure obstacles don't spawn too close to player's start (keep at multiples of 5)
    private int gaurenteedSpawn = 2; // Gaurenteed spawn after x empty spots
    private int cooldownInt = 1; // Obstacles cannot spawn within x positions from themselves
    private int emptySpawnCounter;
    private int resetCooldownInt;

    // Obstacle Variables
    [SerializeField] private int chanceForSingleLaneShortSpawn = 4;
    [SerializeField] private int chanceForSingleLaneTallSpawn = 15;

    // Coin Variables
    [SerializeField] private int chanceForCoinSpawn = 11;
    [SerializeField] private int maxCoinLines = 1;
    private int minimumAmountOfCoins = 2;
    private int maximumAmountOfCoins = 4;
    private int coinLineLength;
    private int coinCounter;
    private int numberOfCoinLines;
    private bool spawningCoins;

    private void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        obstacleSpawner = GetComponentInParent<ObstacleSpawner>();
        coinSpawner = GetComponentInParent<CoinSpawner>();

        thisLaneXPos = transform.position.x;
        thisLaneYPos = transform.position.y;

        resetCooldownInt = cooldownInt;

        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        // Uses a for-loop to iterate through viable spawn positions, then spawns if necessary requirements are met
        // Currently does not support the spawning of multi-lane obstacles
        emptySpawnCounter = 0;
        cooldownInt = 0;
        numberOfCoinLines = 0;

        for (int z = startingDistanceBuffer; z < spawnManager.zBound; z += distanceBuffer)
        {
            Vector3 spawnPos = new Vector3(thisLaneXPos, thisLaneYPos, z);

            if (cooldownInt <= 0 && !spawningCoins)
            {
                if (SuccessfulSpawn(chanceForCoinSpawn) && numberOfCoinLines < maxCoinLines)
                {
                    coinCounter = 0;
                    spawningCoins = true;

                    coinLineLength = DetermineCoinLineLength();
                    coinSpawner.SpawnCoin(spawnPos);
                    coinCounter++;

                    emptySpawnCounter = 0;
                    cooldownInt = resetCooldownInt;
                    cooldownInt--;

                    numberOfCoinLines++;
                }
                // Single-lane tall obstacles only spawn halfway through the initial spawning
                else if (SuccessfulSpawn(chanceForSingleLaneTallSpawn) && z > spawnManager.zBound / 2)
                {
                    obstacleSpawner.SpawnSingleLaneTallObs(spawnPos);
                    emptySpawnCounter = 0;
                    cooldownInt = resetCooldownInt;
                    cooldownInt--;
                }
                else if (SuccessfulSpawn(chanceForSingleLaneShortSpawn) || emptySpawnCounter >= gaurenteedSpawn)
                {
                    obstacleSpawner.SpawnSingleLaneShortObs(spawnPos);
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
            else if (spawningCoins)
            {
                coinSpawner.SpawnCoin(spawnPos);
                coinCounter++;

                if (coinCounter >= coinLineLength)
                {
                    spawningCoins = false;
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

    private int DetermineCoinLineLength()
    {
        int randomInt = Random.Range(minimumAmountOfCoins, maximumAmountOfCoins);
        return randomInt;
    }
}
