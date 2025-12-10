using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{   
    [SerializeField] private float jumpHeight = 4.5f;
    [SerializeField] private float fallMultiplier = 3.5f; // Controls descent speed. Higher = faster descent
    [SerializeField] private float upMultiplier = 3.5f; // Controls ascent speed. Higher = faster ascent (with less accurate jumpHeight)
    [SerializeField] private float hugeJumpCheatHeight = 9f;
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerCollision playerCollision;
    private Rigidbody playerRb;
    private Vector3 cachedGravity; // Stores gravity info to avoid unnecessary calculations in FixedUpdate()
    private float fall;
    private float up;
    private bool jumpQueued;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ResetMultipliers();

        if (CheatManager.Instance.hugeJumps)
        {
            jumpHeight = hugeJumpCheatHeight;
        }

        cachedGravity = Physics.gravity.y * Vector3.up;
        jumpHeight *= upMultiplier;
        up = upMultiplier;
    }

    private void FixedUpdate()
    {
        if (jumpQueued)
        {
            float jumpForce = CalculateJumpForce();
            playerAnimations.Jump();
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerCollision.isOnGround = false;
            jumpQueued = false;
        }

        if (playerRb.velocity.y < 0)
        {
            // - 1 cancels out the Physics normal gravity
            playerRb.velocity += ((fall * SpeedManager.Instance.jumpSpeedMultiplier) - 1) * cachedGravity *  Time.deltaTime; 
        }
        else if (playerRb.velocity.y > 0)
        {
            playerRb.velocity += ((up * SpeedManager.Instance.jumpSpeedMultiplier) - 1) * cachedGravity *  Time.deltaTime;
        }
    }

    public bool CanJump()
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

    public void QueueJump()
    {
        jumpQueued = true;
    }

    private float CalculateJumpForce()
    {
        return Mathf.Sqrt(jumpHeight * SpeedManager.Instance.jumpSpeedMultiplier * Physics.gravity.y * -2) * playerRb.mass;
    }

    private void ResetMultipliers()
    {
        fall = fallMultiplier;
        up = upMultiplier;
    }
}
