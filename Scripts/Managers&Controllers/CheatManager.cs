using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheatType
{
    Invincibility,
    HugeJumps,
    NoSpeedIncrease
}

public class CheatManager : MonoBehaviour, ILoadData
{
    public static CheatManager Instance { get; private set; }

    public event Action<string, int, int> attemptToPurchaseCheat; // item name, price, itemID (Cheats itemID: itemID >= 11 && itemID <= 13)

    [HideInInspector] public bool invincible;
    [HideInInspector] public bool hugeJumps;
    [HideInInspector] public bool noSpeedIncrease;
    [HideInInspector] public Dictionary<int, bool> ownedCheats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            invincible = false;
            hugeJumps = false;
            noSpeedIncrease = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        ownedCheats = new Dictionary<int, bool>();

        for (int i = 11; i < 11 + data.cheatsCollected.Count; i++)
        {
            if (data.cheatsCollected[i] == true)
            {
                ownedCheats[i] = true;
            }
            else
            {
                ownedCheats[i] = false;
            }
        }
    }

    public void SetCheat(CheatType cheatType)
    {
        switch (cheatType)
        {
            case CheatType.Invincibility:
                invincible = invincible ? false : true;
                break;
            case CheatType.HugeJumps:
                hugeJumps = hugeJumps ? false : true;
                break;
            case CheatType.NoSpeedIncrease:
                noSpeedIncrease = noSpeedIncrease ? false : true;
                break;
        }
    }

    public void AttemptToPurchaseCheat(string name, int price, int cheatID)
    {
        int itemID = cheatID;
        attemptToPurchaseCheat?.Invoke(name, price, cheatID);
    }

    public void LoadCheatData()
    {
        DataPersistenceManager.Instance.LoadData();
    }
}
