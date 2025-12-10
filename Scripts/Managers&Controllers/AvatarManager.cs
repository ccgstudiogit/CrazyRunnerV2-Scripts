using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour, ILoadData
{
    public static AvatarManager Instance { get; private set; }

    public event Action avatarChanged;
    public event Action<string, int, int> attemptToPurchaseAvatar; // item name, price, itemID (Avatars itemID: itemID >= 0 && itemID <= 10)

    public GameObject[] avatars;
    [HideInInspector] public Dictionary<int, bool> ownedAvatars;

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

    public void LoadData(GameData data)
    {
        ownedAvatars = new Dictionary<int, bool>();

        for (int i = 0; i < avatars.Length; i++)
        {
            if (data.avatarsCollected[i] == true)
            {
                ownedAvatars[i] = true;
            }
            else
            {
                ownedAvatars[i] = false;
            }
        }
    }

    public void ChangeAvatar(int avatarID)
    {
        PlayerPrefs.SetInt("avatar", avatarID);

        avatarChanged?.Invoke();
    }

    public void AttemptToPurchaseAvatar(string name, int price, int avatarID)
    {
        int itemID = avatarID;
        attemptToPurchaseAvatar?.Invoke(name, price, itemID);
    }

    public void LoadAvatarData()
    {
        DataPersistenceManager.Instance.LoadData();
    }
}
