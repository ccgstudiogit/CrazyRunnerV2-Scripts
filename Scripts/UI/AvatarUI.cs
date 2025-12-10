using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarUI : MonoBehaviour
{
    [SerializeField] private int avatarID;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject lockedDisplay;
    [SerializeField] private GameObject activeCheckmark;
    [SerializeField] private int purchasePrice = 1000;

    private void OnEnable()
    {
        UpdateUI();

        AvatarManager.Instance.avatarChanged += UpdateUI;
        PurchaseWindow.purchaseCompleted += UpdateUI;
    }

    private void OnDisable()
    {
        AvatarManager.Instance.avatarChanged -= UpdateUI;
        PurchaseWindow.purchaseCompleted -= UpdateUI;
    }

    public void SelectAvatar()
    {
        if (AvatarManager.Instance.ownedAvatars[avatarID] == true)
        {
            AvatarManager.Instance.ChangeAvatar(avatarID);
            AvatarManager.Instance.LoadAvatarData();
            UpdateUI();
        }
        else
        {
            AvatarManager.Instance.AttemptToPurchaseAvatar(avatarName.text.ToString(), purchasePrice, avatarID);
        }
    }

    private void UpdateUI()
    {
        DeactivateDisplays();

        if (AvatarManager.Instance.ownedAvatars[avatarID] == true)
        {
            if (avatarID == PlayerPrefs.GetInt("avatar", 0))
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
