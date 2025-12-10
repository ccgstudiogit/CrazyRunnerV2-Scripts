using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentCoinAmount : MonoBehaviour, ISaveData
{
    public event Action OnCoinAmountIncrease;
    public int currentCoinAmount { get; private set; }

    void Awake()
    {
        ResetCoinAmount();
    }

    public void IncreaseCoinAmount()
    {
        currentCoinAmount++;
        OnCoinAmountIncrease?.Invoke();
    }

    public void SaveData(ref GameData data)
    {
        if (!GameController.Instance.alreadySavedStatsThisRun)
        {
            data.coinAmount += currentCoinAmount;
            data.totalCoins += currentCoinAmount;

            if (data.mostCoins < currentCoinAmount)
            {
                data.mostCoins = currentCoinAmount;
            }
        }
    }

    private void ResetCoinAmount()
    {
        currentCoinAmount = 0;
    }
}
