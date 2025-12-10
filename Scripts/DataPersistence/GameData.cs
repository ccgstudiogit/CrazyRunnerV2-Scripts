using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Current currency amount
    public int coinAmount;

    // Single run stats
    public float longestDistance;
    public int highestScore;
    public int mostCoins;

    // Lifetime stats
    public int gamesPlayed;
    public float totalDistance;
    public int totalScore;
    public int totalCoins;
    
    // Avatars collected
    public SerializableDictionary<int, bool> avatarsCollected; // int == unique avatar ID, bool == isCollected

    // Cheats collected
    public SerializableDictionary<int, bool> cheatsCollected; // int starts at 11 (since int 0-10 are taken by avatars)

    // The values defined in this constructor will be the default
    // values if the game starts with no data to load
    public GameData()
    {
        coinAmount = 0;

        longestDistance = 0;
        highestScore = 0;
        mostCoins = 0;

        gamesPlayed = 0;
        totalDistance = 0;
        totalScore = 0;
        totalCoins = 0;
        
        avatarsCollected = new SerializableDictionary<int, bool>();
        cheatsCollected = new SerializableDictionary<int, bool>();
    }
}
