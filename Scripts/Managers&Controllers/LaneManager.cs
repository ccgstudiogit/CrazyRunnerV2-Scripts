using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lanes;
    [HideInInspector] public Vector3[] lanePositions;
    [HideInInspector] public int[] lanePositionIndex;

    /* 
    *** NOTE ***
    In order to add extra lanes, make sure to order them properly (from left to right, 
    element 0 being furthest on the left) within the inspector window in Unity
    */
    
    private void Awake()
    {
        lanePositions = new Vector3[lanes.Length];
        lanePositionIndex = new int[lanes.Length];

        DetermineLanePosition();
    }

    private void DetermineLanePosition()
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            lanePositions[i] = lanes[i].transform.position;
            lanePositionIndex[i] = i;
            Debug.Log("Lane: " + lanes[i] + lanes[i].transform.position + "\n");
        }
    }
}
