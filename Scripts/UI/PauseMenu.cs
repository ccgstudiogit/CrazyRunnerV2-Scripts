using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject infoScreen;
    [SerializeField] private GameObject settingsMenus;

    public void InfoScreen()
    {
        settingsMenus.SetActive(false);
        infoScreen.SetActive(true);
    }

    public void SettingsMenu()
    {
        infoScreen.SetActive(false);
        settingsMenus.SetActive(true);
    }
}
