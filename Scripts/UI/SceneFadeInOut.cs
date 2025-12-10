using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
    public float timeToFade = 1f;
    public float delayTime = 0.35f;

    [SerializeField] private CanvasGroup canvasGroup;
    private bool fadeIn;
    private bool fadeOut;

    public void FadeIn()
    {
        fadeIn = true;
        StartCoroutine(FadeInRoutine());
    }

    public void FadeOut()
    {
        fadeOut = true;
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        // Scene view fading in
        while (fadeIn)
        {
            if (canvasGroup.alpha <= 1)
            {
                if (Time.timeScale == 0)
                {
                    canvasGroup.alpha -= timeToFade * Time.unscaledDeltaTime;
                }
                else
                    canvasGroup.alpha -= timeToFade * Time.deltaTime;

                if (canvasGroup.alpha == 0)
                {
                    canvasGroup.alpha = 0;
                    fadeIn = false;
                }
            }

            yield return null;
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        // Scene view fading out
        while (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                if (Time.timeScale == 0)
                {
                    canvasGroup.alpha += timeToFade * Time.unscaledDeltaTime;
                }
                else
                    canvasGroup.alpha += timeToFade * Time.deltaTime;

                if (canvasGroup.alpha == 1)
                {
                    canvasGroup.alpha = 1;
                    fadeOut = false;
                }
            }

            yield return null;
        }
    }
}
