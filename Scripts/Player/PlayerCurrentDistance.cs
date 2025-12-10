using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentDistance : MonoBehaviour, ISaveData
{
    public event Action OnDistanceIncrease;
    public float currentDistance { get; private set; }

    [SerializeField] private float meters = 0.1f;
    [SerializeField] private float perSecond = 0.12f;

    private void Awake()
    {
        ResetDistance();
    }

    private void Start()
    {
        GameController.Instance.OnGameActiveChanged += OnGameActiveChanged;
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameActiveChanged -= OnGameActiveChanged;
    }

    public void SaveData(ref GameData data)
    {
        if (!GameController.Instance.alreadySavedStatsThisRun)
        {
            data.totalDistance += currentDistance;

            if (data.longestDistance < currentDistance)
            {
                data.longestDistance = currentDistance;
            }
        }
    }

    private void ResetDistance()
    {
        currentDistance = 0;
    }

    private void AddToDistance(float meters)
    {
        currentDistance += meters;
        currentDistance = Mathf.Round(currentDistance * 10f) / 10f;
        OnDistanceIncrease?.Invoke();
    }

    private void OnGameActiveChanged()
    {
        if (GameController.Instance.gameActive)
        {
            StartCoroutine(CalculateDistance());
        }
    }

    private IEnumerator CalculateDistance()
    {
        while (GameController.Instance.gameActive)
        {
            AddToDistance(meters);
            yield return new WaitForSeconds(perSecond * SpeedManager.Instance.distanceMultiplier);
        }
    }
}
