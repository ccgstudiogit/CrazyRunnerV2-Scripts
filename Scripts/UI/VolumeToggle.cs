using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    [SerializeField] private AudioType audioType;
    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = true;
        int volume = 1; // Default is 1. 0 == off, 1 == on
        
        switch (audioType)
        {
            case AudioType.Music:
                volume = PlayerPrefs.GetInt("musicEnabled");
                break;
            case AudioType.SoundEffects:
                volume = PlayerPrefs.GetInt("sfxEnabled");
                break;
        }

        switch (volume)
        {
            case 0:
                toggle.isOn = false;
                break;
            case 1:
                toggle.isOn = true;
                break;
        }

        toggle.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(bool isOn)
    {
        if (AudioManager.Instance != null)
        {
            switch (audioType)
            {
                case AudioType.Music:
                    AudioManager.Instance.EnableMusic(isOn);
                    break;
                case AudioType.SoundEffects:
                    AudioManager.Instance.EnableSoundEffects(isOn);
                    break;
            }
        }
    }
}
