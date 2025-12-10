using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        int fullscreen = PlayerPrefs.GetInt("fullscreen", 0);

        switch (fullscreen)
        {
            case 0:
                toggle.isOn = false;
                break;
            case 1:
                toggle.isOn = true;
                break;
        }
    }

    public void Fullscreen(bool enabled)
    {
        SettingsManager.Instance.SetFullscreen(enabled);
    }
}
