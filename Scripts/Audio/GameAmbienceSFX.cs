using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAmbienceSFX : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //GameController.Instance.OnGameReady += OnGameReady;
        OnGameReady();
    }

    private void OnDisable()
    {
        //GameController.Instance.OnGameReady -= OnGameReady;
    }

    private void OnGameReady()
    {
        audioSource.Play();
    }
}
