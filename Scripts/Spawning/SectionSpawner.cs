using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] sectionPrefabs;
    [SerializeField] private Transform sectionParent;
    private SpawnManager spawnManager;
    private int distanceBuffer = 22;
    private int startingZPos = 22;

    private void Awake()
    {
        spawnManager = GetComponent<SpawnManager>();

        SpawnSections();
    }

    private void SpawnSections()
    {
        int lastIndex = 1; // Set to one since the section in the scene is Section1

        for (int z = startingZPos; z < spawnManager.zBound + distanceBuffer; z += distanceBuffer)
        {
            Vector3 thisSpawnPos = new Vector3(0, 0, z);

            // Makes sure that the same sections do not spawn consecutively
            int newIndex;
            do
            {
                newIndex = Random.Range(0, sectionPrefabs.Length);
            } while (newIndex == lastIndex);

            GameObject section = Instantiate(sectionPrefabs[newIndex], thisSpawnPos, Quaternion.identity);
            section.transform.SetParent(sectionParent);
            lastIndex = newIndex;
        }
    }
}
