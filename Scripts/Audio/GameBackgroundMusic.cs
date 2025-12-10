using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip preRunningMusic, runningMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameController.Instance.OnGameActiveChanged += OnGameStart;
        //GameController.Instance.OnGameReady += OnGameReady;

        OnGameReady();
    }

    private void OnDisable()
    {
        //GameController.Instance.OnGameReady -= OnGameReady;
        GameController.Instance.OnGameActiveChanged -= OnGameStart;
    }

    private void PlayBeginScreenMusic()
    {
        audioSource.clip = preRunningMusic;
        audioSource.Play();
    }

    private void PlayRunningMusic()
    {
        audioSource.Stop();
        audioSource.clip = runningMusic;
        audioSource.Play();
    }

    private void OnGameReady()
    {
        PlayBeginScreenMusic();
    }

    private void OnGameStart()
    {
        if (GameController.Instance.gameActive) { PlayRunningMusic(); }
    }
}
