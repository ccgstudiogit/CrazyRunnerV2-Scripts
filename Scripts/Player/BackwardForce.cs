using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardForce : MonoBehaviour
{
    // This script applies a backwards force on the playerRb on death to make sure the legs don't clip into the obstacle
    private PlayerMovement playerMovement;
    private Rigidbody playerRb;
    private float backwardForce;
    private bool backwardForceQueued;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRb = GetComponent<Rigidbody>();

        backwardForceQueued = false;
        backwardForce = 11f;
    }

    private void Start()
    {
        backwardForce *= playerRb.mass;
        GameController.Instance.OnGameActiveChanged += OnGameActiveChanged;
    }

    private void FixedUpdate()
    {
        if (backwardForceQueued)
        {
            ApplyBackwardForce();
            backwardForceQueued = false;
        }
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameActiveChanged -= OnGameActiveChanged;
    }

    private void ApplyBackwardForce()
    {
        playerRb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        playerRb.AddForce(new Vector3(0, 0, -1) * backwardForce, ForceMode.Impulse);
    }

    private void OnGameActiveChanged()
    {
        if (!GameController.Instance.gameActive)
        {
            backwardForceQueued = true;
        }
    }
}
