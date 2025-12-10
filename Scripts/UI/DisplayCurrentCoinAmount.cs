using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrentCoinAmount : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    private PlayerCurrentCoinAmount playerCurrentCoinAmount;
    private bool playerExists;

    private void Start()
    {
        playerExists = false;

        if (GameObject.FindWithTag("Player") != null)
        {
            playerCurrentCoinAmount = GameObject.FindWithTag("Player").GetComponent<PlayerCurrentCoinAmount>();
            playerCurrentCoinAmount.OnCoinAmountIncrease += OnCoinAmountIncrease;

            playerExists = true;
        }

        coinText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        if (playerExists) { playerCurrentCoinAmount.OnCoinAmountIncrease -= OnCoinAmountIncrease; }
    }

    private void OnCoinAmountIncrease()
    {   
        coinText.text = ": " + playerCurrentCoinAmount.currentCoinAmount.ToString();
    }
}
