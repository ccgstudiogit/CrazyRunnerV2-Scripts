using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatUI : MonoBehaviour
{
    [SerializeField] private int cheatID;
    [SerializeField] private CheatType cheatType;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI cheatName;
    [SerializeField] private GameObject lockedDisplay;
    [SerializeField] private GameObject activeCheckmark;
    [SerializeField] private int purchasePrice = 3000;
    private bool thisCheatCurrentlyEnabled;

    private void OnEnable()
    {
        UpdateUI();

        PurchaseWindow.purchaseCompleted += UpdateUI;
    }

    private void OnDisable()
    {
        PurchaseWindow.purchaseCompleted -= UpdateUI;
    }

    public void SelectCheat()
    {
        if (CheatManager.Instance.ownedCheats[cheatID] == true)
        {
            CheatManager.Instance.SetCheat(cheatType);
            CheatManager.Instance.LoadCheatData();
            thisCheatCurrentlyEnabled = thisCheatCurrentlyEnabled ? false : true;
            UpdateUI();
        }
        else
        {
            CheatManager.Instance.AttemptToPurchaseCheat(cheatName.text.ToString(), purchasePrice, cheatID);
        }
    }

    private void UpdateUI()
    {
        DeactivateDisplays();

        if (CheatManager.Instance.ownedCheats[cheatID] == true)
        {
            cheatName.fontSize = 130;
            cheatName.enableWordWrapping = false;
            
            switch (cheatType)
            {
                case CheatType.Invincibility:
                    thisCheatCurrentlyEnabled = CheatManager.Instance.invincible;
                    break;
                case CheatType.HugeJumps:
                    thisCheatCurrentlyEnabled = CheatManager.Instance.hugeJumps;
                    break;
                case CheatType.NoSpeedIncrease:
                    thisCheatCurrentlyEnabled = CheatManager.Instance.noSpeedIncrease;
                    break;
            }

            if (thisCheatCurrentlyEnabled)
            {
                activeCheckmark.SetActive(true);
            }
        }
        else
        {
            lockedDisplay.SetActive(true);
        }
    }

    private void DeactivateDisplays()
    {
        activeCheckmark.SetActive(false);
        lockedDisplay.SetActive(false);
    }
}
