using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTabs : MonoBehaviour
{
    [SerializeField] GameObject[] tabs;
    [SerializeField] Image[] tabButtons;
    [SerializeField] Color inactiveTabColor, activeTabColor;

    public void SwitchToTab(int tabID)
    {
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }

        tabs[tabID].SetActive(true);

        foreach (Image image in tabButtons)
        {
            image.color = inactiveTabColor;
        }

        tabButtons[tabID].color = activeTabColor;
    }
}
