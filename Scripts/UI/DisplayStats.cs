using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStats : MonoBehaviour, ILoadData
{
    [SerializeField] private TextMeshProUGUI longestRunText, highestScoreText, mostCoinsText;
    [SerializeField] private TextMeshProUGUI gamesPlayedText, totalDistanceText, totalScoreText, totalCoinsText;

    private void Start()
    {
        DataPersistenceManager.Instance.LoadData();
    }

    public void LoadData(GameData data)
    {
        // Single run stats
        longestRunText.text += (Mathf.Round(data.longestDistance * 10f) / 10f).ToString() + "m";
        highestScoreText.text += data.highestScore.ToString();
        mostCoinsText.text += data.mostCoins.ToString();

        // Lifetime stats
        gamesPlayedText.text += data.gamesPlayed.ToString();
        totalDistanceText.text += (Mathf.Round(data.totalDistance * 10f) / 10f).ToString() + "m";
        totalScoreText.text += data.totalScore.ToString();
        totalCoinsText.text += data.totalCoins.ToString();
    }
}
