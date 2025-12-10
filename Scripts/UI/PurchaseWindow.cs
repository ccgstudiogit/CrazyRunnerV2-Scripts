using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseWindow : MonoBehaviour, ILoadData, ISaveData
{
    public static event Action purchaseCompleted; // Here so that AvatarUI calls UpdateUI()

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private GameObject notEnoughGemsPopup;
    [SerializeField] private GameObject notEnoughGemsPopupLocation;
    private int currentCoinAmount;
    private int purchasePrice;
    private int itemID;

    private void OnEnable()
    {
        itemID = -1;

        MainMenuGUIManager.Instance.noLongerViewingExtrasWindow += No;
    }

    private void OnDisable()
    {
        MainMenuGUIManager.Instance.noLongerViewingExtrasWindow -= No;
    }

    public void PurchaseInfo(string name, int price, int ID)
    {
        DataPersistenceManager.Instance.LoadData();
        itemName.text = name;
        purchasePrice = price;
        itemID = ID;
        itemPrice.text = price.ToString();
    }

    public void LoadData(GameData data)
    {
        currentCoinAmount = data.coinAmount;
    }

    public void SaveData(ref GameData data)
    {
        if (itemID >= 0 && itemID <= 10)
        {
            data.avatarsCollected[itemID] = true;
            data.coinAmount -= purchasePrice;
        }
        else if (itemID >= 11 && itemID <= 13)
        {
            data.cheatsCollected[itemID] = true;
            data.coinAmount -= purchasePrice;
        }
    }

    public void Yes()
    {
        Debug.Log($"Attempting to purchase {itemName.text}. ID == {itemID}");

        if (currentCoinAmount < purchasePrice)
        {
            Instantiate(notEnoughGemsPopup, notEnoughGemsPopupLocation.transform);
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.CashRegister);
            
            if (itemID >= 0 && itemID <= 10)
            {
                // AVATAR PURCHASE
                DataPersistenceManager.Instance.SaveData();
                DataPersistenceManager.Instance.LoadData();

                purchaseCompleted?.Invoke();

                AvatarManager.Instance.ChangeAvatar(itemID);

            }
            else if (itemID >= 11 && itemID <= 13)
            {
                // CHEATS PURCHASE
                DataPersistenceManager.Instance.SaveData();
                DataPersistenceManager.Instance.LoadData();

                purchaseCompleted?.Invoke();
            }

            gameObject.SetActive(false);
            Debug.Log("Purchase successful!");
        }
    }

    public void No()
    {
        gameObject.SetActive(false);
    }
}
