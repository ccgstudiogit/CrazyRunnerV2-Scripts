using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentScore : MonoBehaviour, ISaveData
{
    public event Action OnScoreIncrease;
    public int currentScore { get; private set; }
    public int scorePerCoin = 3;

    [SerializeField] private int scorePerMeter = 1;
    private PlayerCurrentDistance playerCurrentDistance;
    private float distanceThreshold; // Distance threshold to increase score
    private float previousDistance = 0f;

    private void Awake()
    {
        playerCurrentDistance = GetComponent<PlayerCurrentDistance>();

        distanceThreshold = scorePerMeter;

        ResetScore();
    }

    private void LateUpdate()
    {
        float distanceSinceLastUpdate = playerCurrentDistance.currentDistance - previousDistance;

        if (distanceSinceLastUpdate >= distanceThreshold)
        {
            int metersToAdd = (int)(distanceSinceLastUpdate / distanceThreshold);
            IncreaseScoreViaDistance(metersToAdd * scorePerMeter);
            previousDistance += metersToAdd * distanceThreshold;
        }
    }

    public void IncreaseScoreViaCoin()
    {
        currentScore += scorePerCoin;
        OnScoreIncrease?.Invoke();
    }

    public void SaveData(ref GameData data)
    {
        if (!GameController.Instance.alreadySavedStatsThisRun)
        {
            data.totalScore += currentScore;

            if (data.highestScore < currentScore)
            {
                data.highestScore = currentScore;
            }
        }
    }

    private void IncreaseScoreViaDistance(int amountToIncrease)
    {
        currentScore += amountToIncrease;
        OnScoreIncrease?.Invoke();
    }

    private void ResetScore()
    {
        currentScore = 0;
    }
}
