using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProceduralScorePopupText : MonoBehaviour
{
    // Default values recommended
    [SerializeField] private Vector2 initialOffset = new Vector2(300, 40);
    [SerializeField] private float minDistance = 110f;
    [SerializeField] private float maxDistance = 175f;
    [SerializeField] private float variation = 40f;
    [SerializeField] private float fadeDuration = 1.7f;
    [SerializeField] private float fadeVariation = 0.3f;

    private TextMeshProUGUI scorePopupText;
    private Vector2 finalOffset; // Position to drift to, relative to the gameObject's local origin
    private float fadeStartTime;
    private float startAlpha;

    private void Start()
    {
        scorePopupText = GetComponent<TextMeshProUGUI>();

        fadeStartTime = Time.time;
        fadeDuration = Random.Range(fadeDuration - fadeVariation, fadeDuration + fadeVariation);

        initialOffset = new Vector2(
            Random.Range(initialOffset.x - (variation / 2), initialOffset.x + (variation / 2)), 
            Random.Range(initialOffset.y - (variation / 2), initialOffset.y + (variation / 2))
        );
        finalOffset = RandomFinalOffset();

        startAlpha = scorePopupText.color.a;
    }

    private void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;

        if (progress <= 1)
        {
            transform.localPosition = Vector2.Lerp(initialOffset, finalOffset, progress);
            float alpha = Mathf.Lerp(startAlpha, 0, progress);
            scorePopupText.color = new Color(scorePopupText.color.r, scorePopupText.color.g, scorePopupText.color.b, alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector2 RandomFinalOffset()
    {
        return new Vector2(
            Random.Range(initialOffset.x + minDistance, initialOffset.x + maxDistance), 
            Random.Range(initialOffset.y - maxDistance, initialOffset.y - minDistance)
        );
    }
}
