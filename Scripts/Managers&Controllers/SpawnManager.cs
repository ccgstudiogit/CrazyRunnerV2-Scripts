using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float globalSpawnCooldown = 1f; // Obstacles will have a chance to spawn every X seconds
    public float zBound = 216f; // Z position where obstacles spawn (keep at multiples of 5 -- mainly for initialSpawning scripts)
    [HideInInspector] public int numberOfTallObstacles; // Keeps track of how many tall obstacles are spawned so that it prevents all lanes spawning tall obstacles at the same time
    [HideInInspector] public int amountOfLanes;
    [HideInInspector] public bool waitingAfterMultiLaneObs; // Prevents obstacles from spawning 1 tick after a multi-lane obstacle spawns

    private LaneManager laneManager;

    private void Start()
    {
        laneManager = GetComponentInChildren<LaneManager>();

        amountOfLanes = laneManager.lanePositions.Length;
    }
}
