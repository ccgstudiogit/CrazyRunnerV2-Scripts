using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProceduralNotEnoughGems : MonoBehaviour
{
    [SerializeField] private Vector2 finalOffset; // Position to drift to, relative to the gameObject's local origin
    [SerializeField] private float fadeDuration;
    private TextMeshProUGUI popupText;
    private Vector2 startPos = new Vector2(0, 0);
    private float fadeStartTime;
    private float startAlpha;

    private  void Start()
    {
        popupText = GetComponent<TextMeshProUGUI>();

        fadeStartTime = Time.time;
        startAlpha = popupText.color.a;
    }

    private void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;

        if (progress < 1)
        {
            transform.localPosition = Vector2.Lerp(startPos, finalOffset, progress);
            float alpha = Mathf.Lerp(startAlpha, 0, progress);
            popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
