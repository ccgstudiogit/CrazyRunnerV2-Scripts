using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    Music,
    SoundEffects
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;

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
        LoadAudioPlayerPrefs();
    }

    private void LoadAudioPlayerPrefs()
    {
        float musicVol = PlayerPrefs.GetFloat("musicVol", 1f);
        float sfxVol = PlayerPrefs.GetFloat("sfxVol", 1f);
        int enableMusicVol = PlayerPrefs.GetInt("musicEnabled", 1); // 0 == off, 1 == on
        int enableSFXVol = PlayerPrefs.GetInt("sfxEnabled", 1); // 0 == off, 1 == on

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
        EnableMusic(enabled = enableMusicVol == 1 ? true : false);
        EnableSoundEffects(enabled = enableSFXVol == 1 ? true : false);
    }

    public void EnableMusic(bool enabled)
    {
        if (enabled == false)
        {
            mixer.SetFloat("musicMaster", -80);
            PlayerPrefs.SetInt("musicEnabled", 0);
        }
        else if (enabled == true)
        {
            mixer.SetFloat("musicMaster", 0);
            PlayerPrefs.SetInt("musicEnabled", 1);
        }
    }

    public void EnableSoundEffects(bool enabled)
    {
        if (enabled == false)
        {
            mixer.SetFloat("sfxMaster", -80);
            PlayerPrefs.SetInt("sfxEnabled", 0);
        }
        else if (enabled == true)
        {
            mixer.SetFloat("sfxMaster", 0);
            PlayerPrefs.SetInt("sfxEnabled", 1);
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        // Converts to logarithm to the base of 10. This is done because it takes the slider value 0.0001 to 1
        // and turns it into a value between -80 and 0 but on a logarithmic scale
        mixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVol", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxVol", sliderValue);
    }
}
