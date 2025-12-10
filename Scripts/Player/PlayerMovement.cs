using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.05f; // Higher = smoother
    [SerializeField] private float snapDistance = 0.15f;
    private PlayerCollision playerCollision;
    private LaneManager laneManager;
    private Rigidbody playerRb;
    private Vector3[] lanes;
    private Vector3 targetPos;
    private Vector3 refVelocity = Vector3.zero;
    private int[] laneIndex;
    private int currentPosIndex;
    private bool isMoving;
    private bool moveRightQueued;
    private bool moveLeftQueued;

    private void Awake()
    {
        playerCollision = GetComponent<PlayerCollision>();
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        laneManager = GameObject.FindWithTag("LaneManager").GetComponent<LaneManager>();

        // Copies lane position information from Lane Manager
        lanes = new Vector3[laneManager.lanePositions.Length];
        laneIndex = new int[laneManager.lanePositionIndex.Length];
        Array.Copy(laneManager.lanePositions, lanes, laneManager.lanePositions.Length);
        Array.Copy(laneManager.lanePositionIndex, laneIndex, laneManager.lanePositionIndex.Length);

        // Starting lane position is determined by the middle of laneIndex array
        currentPosIndex = laneIndex[laneIndex.Length / 2];

        playerRb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public bool CanMove()
    {
        if (!isMoving && playerCollision.isOnGround && !playerCollision.isInObstacle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void QueueMoveRight()
    {
        moveRightQueued = true;
    }

    public void QueueMoveLeft()
    {
        moveLeftQueued = true;
    }

    // Makes sure that the player does not completely clip into an obstacle if they hit it sideways
    public void HitObstacleWhileMoving()
    {
        targetPos = transform.position;
    }

    private void ApplyMovement()
    {
        // After the user initiates valid input, the player object moves to the target 
        // position then snaps into place via snap distance and movement ceases
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPos) < snapDistance)
            {
                playerRb.Move(targetPos, Quaternion.identity);
                isMoving = false;
                return;
            }

            playerRb.Move(Vector3.SmoothDamp(playerRb.position, targetPos, ref refVelocity, smoothTime), Quaternion.identity);
            return;
        }

        if (moveRightQueued)
        {
            moveRightQueued = false;
            GoRight();
        }
        else if (moveLeftQueued)
        {
            moveLeftQueued = false;
            GoLeft();
        }
    }

    private void GoRight()
    {
        // Moving utilizes both lanes array and laneIndex array -- laneIndex is here to make sure
        // the proper lane from lanes is selected. If statement keeps player in-bounds
        int i = currentPosIndex;

        if (transform.position.x <= lanes[lanes.Length - 1].x && currentPosIndex < laneIndex.Length - 1)
        {
            targetPos = new Vector3(lanes[i + 1].x, transform.position.y, 0);
            currentPosIndex += 1;
            isMoving = true;
        }
    }

    private void GoLeft()
    {
        int i = currentPosIndex;

        if (transform.position.x >= lanes[0].x && currentPosIndex > 0)
        {
            targetPos = new Vector3(lanes[i - 1].x, transform.position.y, 0);
            currentPosIndex -= 1;
            isMoving = true;
        }
    }
}
