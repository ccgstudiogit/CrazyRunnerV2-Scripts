using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickSound : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(() => PlayClickSound());
    }

    private void PlayClickSound()
    {
        SoundController.Instance.PlaySound(SoundType.Click);
    }

}
