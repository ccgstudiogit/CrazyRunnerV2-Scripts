using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KeepPlayerInBounds : MonoBehaviour
{
    // ***NOTE*** This script is mainly in case bugs occur. No matter the bug, the player obj will always be on screen
    [SerializeField] private float maximumY = 11f;
    [SerializeField] private float minimumY = -2f;
    private LaneManager laneManager;
    private Rigidbody playerRb;
    private Vector3[] lanes;
    private float yOffset;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        laneManager = GameObject.FindWithTag("LaneManager").GetComponent<LaneManager>();

        lanes = new Vector3[laneManager.lanePositions.Length];
        Array.Copy(laneManager.lanePositions, lanes, laneManager.lanePositions.Length);
        yOffset = transform.position.y;
    }

    private void Update()
    {
        // Keeps player within the bounds of the spawn lanes and chosen upper/lower bounds
        CheckLeftBound();
        CheckRightBound();
        CheckUpperBound();
        CheckLowerBound();
    }

    private void CheckLeftBound()
    {
        if (transform.position.x < (lanes[0].x - 1))
        {
            Vector3 newPos = new Vector3(lanes[0].x, transform.position.y, 0);
            playerRb.MovePosition(newPos);
        }
    }

    private void CheckRightBound()
    {
        if (transform.position.x > (lanes[lanes.Length - 1].x + 1))
        {
            Vector3 newPos = new Vector3(lanes[lanes.Length - 1].x, transform.position.y, 0);
            playerRb.MovePosition(newPos);
        }
    }

    private void CheckUpperBound()
    {
        if (transform.position.y > maximumY)
        {
            Vector3 newPos = new Vector3(transform.position.x, 11, 0);
            playerRb.MovePosition(newPos);
        }
    }

    private void CheckLowerBound()
    {
        if (transform.position.y < minimumY)
        {
            Vector3 newPos = new Vector3(transform.position.x, yOffset, 0);
            playerRb.MovePosition(newPos);
        }
    }
}
