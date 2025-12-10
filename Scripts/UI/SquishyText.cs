using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class SquishyText : MonoBehaviour
{
    [SerializeField] private float squishAmount;
    [SerializeField] private float duration;
    private TextMeshProUGUI text;
    private float originalScaleY;
    private float squishedScaleY;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        originalScaleY = text.rectTransform.localScale.y;
        squishedScaleY = originalScaleY - squishAmount;

        StartCoroutine(SquishDown());
    }

    private IEnumerator SquishDown()
    {
        text.rectTransform.DOScaleY(squishedScaleY, duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(BackToOriginalShape());
    }

    private IEnumerator BackToOriginalShape()
    {
        text.rectTransform.DOScaleY(originalScaleY, duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(SquishDown());
    }
}
