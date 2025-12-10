using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; private set; }

    [HideInInspector] public float moveBackSpeed;
    [HideInInspector] public float jumpSpeedMultiplier = 1f; // Increases over game session, increasing jump speeds
    [HideInInspector] public float distanceMultiplier = 1f; // Decreases over game session, increasing how often one unit of distance is added to player distance
    [HideInInspector] public float spawnMultiplier = 1f; // Decreases over game session, increasing spawn rate as speed increases

    // Affects obstacles/objects. Current values are recommended defaults
    [SerializeField] private float startingSpeed = 14f;
    [SerializeField] private float maxSpeed = 31f;
    [SerializeField] private float moveBackSpeedMultiplier = 1.05f;

    // Affects player jumping physics (jumping speed)
    [SerializeField] private float jumpSpeedAdditiveIncreaseBy = 0.1f; // Adds this to jumpSpeedMultiplier every increaseSpeedEveryXSeconds

    // Affects everything
    [SerializeField] private int increaseSpeedEveryXSeconds = 11;

    // Important values that are needed for calculations
    private float startingJumpSpeedMultiplier = 1f;
    private float minimumDistanceMultiplier;
    private float distanceSubtractiveDecreaseBy; // Subtracts this to distanceMultiplier to increase the speed at which distance is added to player's distance
    private float spawnSubtractiveDecreaseBy; // Similar to distanceSubtractiveDecreaseBy

    private Coroutine increaseSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        moveBackSpeed = startingSpeed;
        jumpSpeedMultiplier = startingJumpSpeedMultiplier;
        minimumDistanceMultiplier = startingSpeed / maxSpeed;
    }

    private void Start()
    {
        GameController.Instance.OnGameActiveChanged += OnGameActiveChanged;

        CalculateDecreaseByRatio();
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameActiveChanged -= OnGameActiveChanged;
    }

    private void OnGameActiveChanged()
    {
        if (GameController.Instance.gameActive && !CheatManager.Instance.noSpeedIncrease)
        {
            increaseSpeed = StartCoroutine(IncreaseSpeed());
        }
    }

    private void CalculateDecreaseByRatio()
    {
        // Calculates the necessary amount of steps it takes to go from startingSpeed to maxSpeed
        int steps = Mathf.CeilToInt(Mathf.Log(maxSpeed / startingSpeed) / Mathf.Log(moveBackSpeedMultiplier));

        // Uses calculated steps to figure out the ratio needed for player distance and spawn rate
        float totalDecreaseNeeded = distanceMultiplier - minimumDistanceMultiplier;
        distanceSubtractiveDecreaseBy = totalDecreaseNeeded / steps;
        spawnSubtractiveDecreaseBy = distanceSubtractiveDecreaseBy;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (GameController.Instance.gameActive)
        {
            if (moveBackSpeed <= maxSpeed)
            {
                moveBackSpeed = Mathf.Round(moveBackSpeed * moveBackSpeedMultiplier * 10f) / 10f;
                jumpSpeedMultiplier += jumpSpeedAdditiveIncreaseBy;

                distanceMultiplier -= distanceSubtractiveDecreaseBy;
                spawnMultiplier -= spawnSubtractiveDecreaseBy;
            }
            else if (moveBackSpeed >= maxSpeed)
            {
                StopCoroutine(increaseSpeed);
                Debug.Log("Maximum speed reached.");
            }

            yield return new WaitForSeconds(increaseSpeedEveryXSeconds);
        }
    }
}
