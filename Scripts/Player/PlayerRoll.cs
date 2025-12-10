using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerCollision playerCollision;
    private PlayerMovement playerMovement;
    private float rollLength = 0.7f;
    private bool rollQueued;
    private bool isRolling;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerCollision = GetComponent<PlayerCollision>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (rollQueued)
        {
            playerAnimations.Roll();
            rollQueued = false;
            Invoke(nameof(NoLongerRolling), rollLength);
        }
    }

    public bool CanRoll()
    {
        if ((playerCollision.isOnGround || playerCollision.isOnObstacle) && !playerMovement.IsMoving())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void QueueRoll()
    {
        rollQueued = true;
        isRolling = true;
    }

    public bool IsRolling()
    {
        return isRolling;
    }

    private void NoLongerRolling()
    {
        isRolling = false;
    }
}
