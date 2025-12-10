using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Suggested defualt values
    public int chanceForShortSingleLaneObs = 4;
    public int chanceForTallSingleLaneObs = 13;
    public int chanceForMultiLaneObs = 11;
    public int tallSingleLaneObsCooldown = 5;
    public int multiLaneObsCooldown = 8;
    public int gaurenteedSpawnInt = 2; // Gaurentees a spawn after X amount of empty spaces

    [SerializeField] private GameObject[] singleLaneShortObstacles;
    [SerializeField] private GameObject[] singleLaneTallObstacles;
    // In inspector window under SpawnLane Manager, within each individual lane check
    // the enableMultiLaneObs to enable multi-lane obs spawning in that individual lane
    [SerializeField] private GameObject[] multiLaneObstacles;
    private int shortSelectionNumber;
    private int tallSelectionNumber;
    private int multiSelectionNumber;

    // Each method has its own selection number, that randomly generated selection number
    // is used to pick the GameObject from GameObject[] array, followed by Instantiate(GameObject)
    // Extra note : not sure how efficient this is, all of this code could likely be trimmed down
    public void SpawnSingleLaneShortObs(Vector3 spawnPos)
    {
        shortSelectionNumber = SelectRandomShortObs();
        GameObject newObj = singleLaneShortObstacles[shortSelectionNumber];
        Instantiate(newObj, spawnPos + newObj.transform.position, newObj.transform.rotation);
    }

    public void SpawnSingleLaneTallObs(Vector3 spawnPos)
    {
        tallSelectionNumber = SelectRandomTallObs();
        GameObject newObj = singleLaneTallObstacles[tallSelectionNumber];
        Instantiate(newObj, spawnPos + newObj.transform.position, newObj.transform.rotation);
    }

    public void SpawnMultiLaneObs(Vector3 spawnPos)
    {
        multiSelectionNumber = SelectRandomMultiLaneObs();
        GameObject newObj = multiLaneObstacles[multiSelectionNumber];
        Instantiate(newObj, spawnPos + newObj.transform.position, newObj.transform.rotation);
    }

    private int SelectRandomShortObs()
    {
        int randomInt = Random.Range(0, singleLaneShortObstacles.Length);
        return randomInt;
    }

    private int SelectRandomTallObs()
    {
        int randomInt = Random.Range(0, singleLaneTallObstacles.Length);
        return randomInt;
    }

    private int SelectRandomMultiLaneObs()
    {
        int randomInt = Random.Range(0, multiLaneObstacles.Length);
        return randomInt;
    }
}
