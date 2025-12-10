using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerJump), typeof(PlayerRoll))]
[RequireComponent(typeof(PlayerAnimations), typeof(PlayerCollision), typeof(PlayerCurrentDistance))]
[RequireComponent(typeof(PlayerCurrentScore), typeof(PlayerCurrentCoinAmount), typeof(PlayerSFX))]
[RequireComponent(typeof(PlayerVFX), typeof(BackwardForce), typeof(KeepPlayerInBounds))]
public class GetInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerRoll playerRoll;
    private float inputCooldown = 0.07f;
    private float actionCooldown = 0.5f;
    private bool recentInput; // Any input that moves the player
    private bool recentAction; // Any action that the player performs (jump, roll)

    /*
    ***NOTE***
    recentInput is here to prevent a bug that occurs when the user rapidly inputs a jump and move within the 
    same frame. This would cause both a jump and move to occur at the same time, creating an ugly jittering
    effect as the game would try to figure out where to put the player character (since inputs are only
    configured to handle one input at a time -- either moving or jumping, not both). recentInput and Cooldown()
    prevent this bug from occuring by making sure only 1 input is received every 0.1 seconds.
    */

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerRoll = GetComponent<PlayerRoll>();
    }

    private void Start()
    {
        playerInputActions = InputManager.inputActions;

        playerInputActions.Player.Enable();
    }

    private void Update()
    {
        if (GameController.Instance.gameActive)
        {
            GetUserInput();
        }
        else if (GameController.Instance.beginScreenActive && (playerInputActions.Player.Jump.WasPressedThisFrame() || playerInputActions.Player.Continue.WasPressedThisFrame()))
        {
            GameController.Instance.StartGame();
        }
    }

    private void GetUserInput()
    {
        if (playerInputActions.Player.Pause.WasPressedThisFrame())
        {
            GameController.Instance.Pause();
        }

        if (playerInputActions.Player.MoveRight.WasPressedThisFrame() && playerMovement.CanMove() && !playerRoll.IsRolling() && !recentInput && !GameController.Instance.gamePaused)
        {
            recentInput = true;
            playerMovement.QueueMoveRight();
            Invoke(nameof(InputCooldown), inputCooldown);
        }
        else if (playerInputActions.Player.MoveLeft.WasPressedThisFrame() && playerMovement.CanMove() && !playerRoll.IsRolling() && !recentInput && !GameController.Instance.gamePaused)
        {
            recentInput = true;
            playerMovement.QueueMoveLeft();
            Invoke(nameof(InputCooldown), inputCooldown);
        }
        else if (playerInputActions.Player.Jump.WasPressedThisFrame() && playerJump.CanJump() && !playerRoll.IsRolling() && !recentInput && !recentAction && !GameController.Instance.gamePaused)
        {
            recentInput = true;
            recentAction = true;
            playerJump.QueueJump();
            Invoke(nameof(InputCooldown), inputCooldown);
            Invoke(nameof(ActionCooldown), actionCooldown);
        }
        else if (playerInputActions.Player.Roll.WasPressedThisFrame() && playerRoll.CanRoll() && !recentInput && !recentAction && !GameController.Instance.gamePaused)
        {
            recentInput = true;
            recentAction = true;
            playerRoll.QueueRoll();
            Invoke(nameof(InputCooldown), inputCooldown);
            Invoke(nameof(ActionCooldown), actionCooldown);
        }
    }

    private void InputCooldown()
    {
        recentInput = false;
    }

    private void ActionCooldown()
    {
        recentAction = false;
    }
}
