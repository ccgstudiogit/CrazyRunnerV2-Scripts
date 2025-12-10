using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalDistance : MonoBehaviour
{
    private TextMeshProUGUI distanceText;
    private PlayerCurrentDistance playerCurrentDistance;

    private void Awake()
    {
        distanceText = GetComponent<TextMeshProUGUI>();
        playerCurrentDistance = GameObject.FindWithTag("Player").GetComponent<PlayerCurrentDistance>();
    }

    private void OnEnable()
    {
        distanceText.text = "Distance: " + playerCurrentDistance.currentDistance.ToString() + "m";
    }
}
