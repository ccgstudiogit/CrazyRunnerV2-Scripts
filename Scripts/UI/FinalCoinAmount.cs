using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalCoinAmount : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    private PlayerCurrentCoinAmount playerCurrentCoinAmount;

    private void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        playerCurrentCoinAmount = GameObject.FindWithTag("Player").GetComponent<PlayerCurrentCoinAmount>();
    }

    private void OnEnable()
    {
        coinText.text = ": " + playerCurrentCoinAmount.currentCoinAmount.ToString();
    }
}
