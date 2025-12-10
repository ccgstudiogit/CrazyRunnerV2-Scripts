using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(SettingsManager.Instance.resolutionOptions);

        dropdown.value = SettingsManager.Instance.currentResolutionIndex;
        dropdown.RefreshShownValue();
    }

    public void ChangeResolution(int resolutionIndex)
    {
        SettingsManager.Instance.SetResolution(resolutionIndex);
    }
}
