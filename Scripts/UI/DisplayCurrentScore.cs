using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrentScore : MonoBehaviour
{
    public Vector3 scoreUIDisplayPos { get; private set; }

    private TextMeshProUGUI scoreText;
    private PlayerCurrentScore playerCurrentScore;
    private bool playerExists;

    private void Start()
    {
        playerExists = false;

        if (GameObject.FindWithTag("Player") != null)
        {
            playerCurrentScore = GameObject.FindWithTag("Player").GetComponent<PlayerCurrentScore>();
            playerCurrentScore.OnScoreIncrease += OnScoreIncrease;

            playerExists = true;
        }

        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        if (playerExists) { playerCurrentScore.OnScoreIncrease -= OnScoreIncrease; }
    }

    private void OnScoreIncrease()
    {
        scoreText.text = "Score: " + playerCurrentScore.currentScore.ToString();
    }
}
