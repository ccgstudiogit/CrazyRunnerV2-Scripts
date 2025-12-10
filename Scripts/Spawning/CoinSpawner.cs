using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public int chanceForCoinSpawn = 8;
    public int coinSpawnCooldown = 7;
    [HideInInspector] public bool isOnCoinCooldown;

    [SerializeField] private int minimumCoinGeneration = 3;
    [SerializeField] private int maximumCoinGeneration = 5;
    [SerializeField] private GameObject coinPrefab;
    private SpawnManager spawnManager;
    private int coinsToSpawn;

    private void Awake()
    {
        spawnManager = GetComponent<SpawnManager>();
    }

    public void SpawnCoin(Vector3 spawnPos)
    {
        GameObject newCoin = coinPrefab;
        Instantiate(newCoin, spawnPos + newCoin.transform.position, transform.rotation);
    }

    public int SpawnCoinLine(Vector3 spawnPos)
    {
        // When called, SpawnCoinLine first randomly selects a number of coins to
        // spawn, then spawns a line of coins via SpawnCoins() at given spawnPos
        coinsToSpawn = GenerateRandomInt(minimumCoinGeneration, maximumCoinGeneration);

        StartCoroutine(SpawnCoins(spawnPos, coinsToSpawn));

        return coinsToSpawn;
    }

    private IEnumerator SpawnCoins(Vector3 spawnPos, int numberOfCoins)
    {
        // Uses a coinCounter to keep track of coins spawned, method stops once max number of coins is reached
        int coinCounter = 1;
        while (coinCounter <= numberOfCoins)
        {
            GameObject newCoin = coinPrefab;
            Instantiate(newCoin, spawnPos + newCoin.transform.position, transform.rotation);
            coinCounter++;
            yield return new WaitForSeconds(spawnManager.globalSpawnCooldown * SpeedManager.Instance.spawnMultiplier);
        }
    }

    private int GenerateRandomInt(int min, int max)
    {
        int randomInt = Random.Range(min, max + 1);
        return randomInt;
    }
}
