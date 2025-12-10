using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private float gravityModifier = 1f; // Recommended to leave at 1 as to keep gravity as regular gravity
    private static Vector3 regularGravity = new Vector3(0, -9.81f, 0);
    private Vector3 originalGravity;

    private void Start()
    {
        // Resets gravity on start (for making sure if player leaves to main menu and plays again, gravity is reset)
        Physics.gravity = regularGravity;

        // Completely resets gravity on player death via PlayerCollision
        originalGravity = Physics.gravity;
        Physics.gravity = originalGravity * gravityModifier;
    }
}
