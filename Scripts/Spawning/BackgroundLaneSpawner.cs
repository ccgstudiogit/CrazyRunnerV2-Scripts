using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLaneSpawner : MonoBehaviour
{
    private BackgroundObjectSpawner backgroundObjectSpawner;
    private SpawnManager spawnManager;
    private ObstacleSpawner obstacleSpawner;
    private Vector3 thisSpawnPos;
    private int emptySpawnCounter;

    private void Start()
    {
        backgroundObjectSpawner = GetComponentInParent<BackgroundObjectSpawner>();
        spawnManager = GetComponentInParent<SpawnManager>();
        obstacleSpawner = GetComponentInParent<ObstacleSpawner>();

        thisSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnManager.zBound);

        GameController.Instance.OnGameActiveChanged += OnGameActiveChanged;
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameActiveChanged -= OnGameActiveChanged;
    }

    private void OnGameActiveChanged()
    {
        if (GameController.Instance.gameActive)
        {
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        emptySpawnCounter = 0;

        while (GameController.Instance.gameActive)
        {
            yield return new WaitForSeconds(spawnManager.globalSpawnCooldown * SpeedManager.Instance.spawnMultiplier);

            if (SuccessfulSpawn(backgroundObjectSpawner.chanceForRareBackgroundObject))
            {
                backgroundObjectSpawner.SpawnRareBackgroundObject(thisSpawnPos);
                emptySpawnCounter = 0;
            }
            else if (SuccessfulSpawn(backgroundObjectSpawner.chanceForBackgroundObject) || emptySpawnCounter >= obstacleSpawner.gaurenteedSpawnInt / 2)
            {
                backgroundObjectSpawner.SpawnBackgroundObject(thisSpawnPos);
                emptySpawnCounter = 0;
            }
            else
            {
                emptySpawnCounter++;
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
