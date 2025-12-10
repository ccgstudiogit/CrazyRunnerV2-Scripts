using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectSpawner : MonoBehaviour
{
    public int chanceForBackgroundObject = 3;
    public int chanceForRareBackgroundObject = 8;

    [SerializeField] private GameObject[] backgroundObjectPrefabs;
    [SerializeField] private GameObject[] rareBackgroundObjectPrefabs;

    public void SpawnBackgroundObject(Vector3 spawnPos)
    {
        int selectionNumber = RandomSelectionNumber(backgroundObjectPrefabs.Length);
        GameObject newObj = backgroundObjectPrefabs[selectionNumber];
        Instantiate(newObj, spawnPos + newObj.transform.position, transform.rotation);
    }

    public void SpawnRareBackgroundObject(Vector3 spawnPos)
    {
        int selectionNumber = RandomSelectionNumber(rareBackgroundObjectPrefabs.Length);
        GameObject newObj = rareBackgroundObjectPrefabs[selectionNumber];
        Instantiate(newObj, spawnPos + newObj.transform.position, transform.rotation);
    }

    private int RandomSelectionNumber(int max)
    {
        int randomInt = Random.Range(0, max);
        return randomInt;
    }
}
