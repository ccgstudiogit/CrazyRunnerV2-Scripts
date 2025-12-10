using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private PlayerCurrentScore playerCurrentScore;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        playerCurrentScore = GameObject.FindWithTag("Player").GetComponent<PlayerCurrentScore>();
    }

    private void OnEnable()
    {
        scoreText.text = "Score: " + playerCurrentScore.currentScore.ToString();
    }
}
