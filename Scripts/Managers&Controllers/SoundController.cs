using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Click,
    Coin,
    Footstep,
    Land,
    Roll,
    Hit,
    CashRegister,
    Whoosh
}

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    [SerializeField] private AudioClip[] footsteps, lands, rolls;
    [SerializeField] private AudioClip hit, coin, click, cash, whoosh;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.Click:
                audioSource.PlayOneShot(click);
                break;
            case SoundType.Coin:
                audioSource.PlayOneShot(coin);
                break;
            case SoundType.Footstep:
                audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length - 1)]);
                break;
            case SoundType.Land:
                audioSource.PlayOneShot(lands[Random.Range(0, lands.Length - 1)]);
                break;
            case SoundType.Roll:
                audioSource.PlayOneShot(rolls[Random.Range(0, rolls.Length - 1)]);
                break;
            case SoundType.Hit:
                audioSource.PlayOneShot(hit);
                break;
            case SoundType.CashRegister:
                audioSource.PlayOneShot(cash);
                break;
            case SoundType.Whoosh:
                audioSource.PlayOneShot(whoosh);
                break;
        }
    }
}
