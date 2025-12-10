using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour, ILoadData
{
    private TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        DataPersistenceManager.Instance.LoadData();
    }

    public void LoadData(GameData data)
    {
        coinText.text = ": " + data.coinAmount.ToString();
    }
}
