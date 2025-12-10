using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioType audioType;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        float volume = 1f;

        switch (audioType)
        {
            case AudioType.Music:
                volume  = PlayerPrefs.GetFloat("musicVol", 1f);
                break;
            case AudioType.SoundEffects:
                volume = PlayerPrefs.GetFloat("sfxVol", 1f);
                break;
        }

        slider.value = volume;
        slider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            switch (audioType)
            {
                case AudioType.Music:
                    AudioManager.Instance.SetMusicVolume(volume);
                    break;
                case AudioType.SoundEffects:
                    AudioManager.Instance.SetSFXVolume(volume);
                    break;
            }
        }
    }
}
