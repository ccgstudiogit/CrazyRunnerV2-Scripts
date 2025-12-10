using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    public static DataPersistenceManager Instance { get; private set; }

    private GameData gameData;
    private FileDataHandler fileDataHandler;
    private List<ISaveData> saveDataObjects;
    private List<ILoadData> loadDataObjects;

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

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        LoadData();
    }

    public void NewData()
    {
        gameData = new GameData();

        // Resets avatar shop
        for (int i = 0; i < AvatarManager.Instance.avatars.Length; i++)
        {
            if (i == 0 || i == 1)
            {
                gameData.avatarsCollected[i] = true;
            }
            else
            {
                gameData.avatarsCollected[i] = false;
            }
        }

        // Resets cheats shop
        for (int i = 11; i < 11 + System.Enum.GetValues(typeof(CheatType)).Length; i++)
        {
            gameData.cheatsCollected[i] = false;
        }
    }

    public void LoadData()
    {
        gameData = fileDataHandler.Load();
        loadDataObjects = FindAllLoadDataObjects();

        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewData();
            SaveData();
            PlayerPrefs.SetInt("avatar", 0); // Resets selected avatar if user deletes PlayerData
            return;
        }

        foreach (ILoadData loadDataObj in loadDataObjects)
        {
            loadDataObj.LoadData(gameData);
        }

        Debug.Log($"Load Data:\nCoins: {gameData.coinAmount}\n\nRecord Stats\nLongest Distance: {gameData.longestDistance}\nHighestScore: {gameData.highestScore}\nMost Coins: {gameData.mostCoins}\n\nLifetime Stats\n# Of Games: {gameData.gamesPlayed}\nDistance: {gameData.totalDistance}\nScore: {gameData.totalScore}\nCoins: {gameData.totalCoins}");
    }

    private List<ILoadData> FindAllLoadDataObjects()
    {
        IEnumerable<ILoadData> loadDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ILoadData>();
        return new List<ILoadData>(loadDataObjects);
    }

    public void SaveData()
    {
        saveDataObjects = FindAllSaveDataObjects();

        foreach (ISaveData saveDataObj in saveDataObjects)
        {
            saveDataObj.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData);

        Debug.Log($"Save Data:\nCoins: {gameData.coinAmount}\n\nRecord Stats\nLongest Distance: {gameData.longestDistance}\nHighestScore: {gameData.highestScore}\nMost Coins: {gameData.mostCoins}\n\nLifetime Stats\n# Of Games: {gameData.gamesPlayed}\nDistance: {gameData.totalDistance}\nScore: {gameData.totalScore}\nCoins: {gameData.totalCoins}");
    }

    private List<ISaveData> FindAllSaveDataObjects()
    {
        IEnumerable<ISaveData> saveDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();
        return new List<ISaveData>(saveDataObjects);
    }
}
