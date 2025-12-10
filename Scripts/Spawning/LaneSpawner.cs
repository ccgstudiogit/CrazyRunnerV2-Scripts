using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [SerializeField] private bool enableMultiLaneObstacleSpawning;
    private SpawnManager spawnManager;
    private CoinSpawner coinSpawner;
    private ObstacleSpawner obstacleSpawner;
    private Vector3 thisSpawnPos;
    private int emptySpawnCounter;
    private int secondsAfterTallObs = 2;
    private int secondsAfterMultiLaneObs = 1;
    private int shortSingleLaneObsCooldown = 2;
    private bool isOnMultiLaneObsCooldown;
    private bool isOnTallObsCooldown;
    private bool isOnShortObsCooldown;
    private bool isSpawningCoins;
    private bool waitingAfterTallObs;

    private void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        coinSpawner = GetComponentInParent<CoinSpawner>();
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
            // Only coin line may spawn immediately after a multi-lane obs has spawned
            if (spawnManager.waitingAfterMultiLaneObs && SuccessfulSpawn(coinSpawner.chanceForCoinSpawn) && !coinSpawner.isOnCoinCooldown)
            {
                yield return new WaitForSeconds(spawnManager.globalSpawnCooldown * SpeedManager.Instance.spawnMultiplier);
                int numberOfCoins = coinSpawner.SpawnCoinLine(thisSpawnPos);
                isSpawningCoins = true;
                coinSpawner.isOnCoinCooldown = true;
                emptySpawnCounter = 0;
                StartCoroutine(SpawningCoins(numberOfCoins));
                StartCoroutine(CoinSpawnCooldown());
            }
            // No obstacles spawn while coins are spawning nor after a tall obstacle spawns after set amount of time
            else if (!isSpawningCoins && !waitingAfterTallObs && !spawnManager.waitingAfterMultiLaneObs)
            {
                yield return new WaitForSeconds(spawnManager.globalSpawnCooldown * SpeedManager.Instance.spawnMultiplier);
                SpawningOrderOfOperations();
            }
            else
            {
                yield return new WaitForSeconds(spawnManager.globalSpawnCooldown * SpeedManager.Instance.spawnMultiplier);
                emptySpawnCounter++;
            }
        }
    }

    private void SpawningOrderOfOperations()
    {
        // Attempt spawning order: multi-lane obs -> coinline -> tall obs -> short obs (with gaurenteed spawn)
        if (enableMultiLaneObstacleSpawning && SuccessfulSpawn(obstacleSpawner.chanceForMultiLaneObs) && !isOnMultiLaneObsCooldown)
        {
            obstacleSpawner.SpawnMultiLaneObs(thisSpawnPos);
            isOnMultiLaneObsCooldown = true;
            spawnManager.waitingAfterMultiLaneObs = true;
            emptySpawnCounter = 0;
            StartCoroutine(MultiLaneObsCooldown());
            StartCoroutine(WaitAfterMultiLaneObs());
            return;
        }
        else if (SuccessfulSpawn(coinSpawner.chanceForCoinSpawn) && !coinSpawner.isOnCoinCooldown)
        {
            int numberOfCoins = coinSpawner.SpawnCoinLine(thisSpawnPos);
            isSpawningCoins = true;
            coinSpawner.isOnCoinCooldown = true;
            emptySpawnCounter = 0;
            StartCoroutine(SpawningCoins(numberOfCoins));
            StartCoroutine(CoinSpawnCooldown());
            return;
        }
        else if (SuccessfulSpawn(obstacleSpawner.chanceForTallSingleLaneObs) && spawnManager.numberOfTallObstacles < spawnManager.amountOfLanes - 1 && !isOnTallObsCooldown)
        {
            obstacleSpawner.SpawnSingleLaneTallObs(thisSpawnPos);
            spawnManager.numberOfTallObstacles++;
            isOnTallObsCooldown = true;
            waitingAfterTallObs = true;
            emptySpawnCounter = 0;
            StartCoroutine(WaitAfterTallObs());
            StartCoroutine(SingleLaneTallObsCooldown());
            return;
        }
        else if ((SuccessfulSpawn(obstacleSpawner.chanceForShortSingleLaneObs) && !isOnShortObsCooldown) || emptySpawnCounter >= obstacleSpawner.gaurenteedSpawnInt)
        {
            obstacleSpawner.SpawnSingleLaneShortObs(thisSpawnPos);
            isOnShortObsCooldown = true;
            emptySpawnCounter = 0;
            StartCoroutine(SingleLaneShortObsCooldown());
            return;
        }
        else
        {
            emptySpawnCounter++;
            spawnManager.numberOfTallObstacles = 0;
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
        int randomInt = Random.Range(1, oddsForSpawn + 1);
        return randomInt;
    }

    private IEnumerator MultiLaneObsCooldown()
    {
        yield return new WaitForSeconds(obstacleSpawner.multiLaneObsCooldown * SpeedManager.Instance.spawnMultiplier);
        isOnMultiLaneObsCooldown = false;
    }

    private IEnumerator SingleLaneTallObsCooldown()
    {
        yield return new WaitForSeconds(obstacleSpawner.tallSingleLaneObsCooldown * SpeedManager.Instance.spawnMultiplier);
        isOnTallObsCooldown = false;
    }

    private IEnumerator SingleLaneShortObsCooldown()
    {
        yield return new WaitForSeconds(shortSingleLaneObsCooldown * SpeedManager.Instance.spawnMultiplier);
        isOnShortObsCooldown = false;
    }

    private IEnumerator CoinSpawnCooldown()
    {
        yield return new WaitForSeconds(coinSpawner.coinSpawnCooldown * SpeedManager.Instance.spawnMultiplier);
        coinSpawner.isOnCoinCooldown = false;
    }

    private IEnumerator WaitAfterTallObs()
    {
        yield return new WaitForSeconds(secondsAfterTallObs * SpeedManager.Instance.spawnMultiplier);
        waitingAfterTallObs = false;
    }

    private IEnumerator WaitAfterMultiLaneObs()
    {
        yield return new WaitForSeconds(secondsAfterMultiLaneObs * SpeedManager.Instance.spawnMultiplier);
        spawnManager.waitingAfterMultiLaneObs = false;
    }

    private IEnumerator SpawningCoins(int numberOfCoins)
    {
        yield return new WaitForSeconds(numberOfCoins * SpeedManager.Instance.spawnMultiplier);
        isSpawningCoins = false;
    }
}
