using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrentDistance : MonoBehaviour
{
    private TextMeshProUGUI distanceText;
    private PlayerCurrentDistance playerCurrentDistance;
    private bool playerExists;

    private void Start()
    {
        playerExists = false;

        if (GameObject.FindWithTag("Player") != null)
        {
            playerCurrentDistance = GameObject.FindWithTag("Player").GetComponent<PlayerCurrentDistance>();
            playerCurrentDistance.OnDistanceIncrease += OnDistanceIncrease;

            playerExists = true;
        }

        distanceText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        if (playerExists) { playerCurrentDistance.OnDistanceIncrease -= OnDistanceIncrease; }
    }

    private void OnDistanceIncrease()
    {
        distanceText.text = playerCurrentDistance.currentDistance.ToString() + " m";
    }
}
