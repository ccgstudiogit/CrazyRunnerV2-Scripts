using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        SetQualityValue();
    }

    public void ChangeGraphicsSettings(int qualityIndex)
    {
        SettingsManager.Instance.SetQuality(qualityIndex);
    }

    private void SetQualityValue()
    {
        dropdown.value = PlayerPrefs.GetInt("quality", 2);
    }
}
