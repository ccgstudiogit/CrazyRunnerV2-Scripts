using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ISaveData
{
    public static GameController Instance { get; private set; }
    public event Action OnGameActiveChanged;
    public event Action OnGameReady;

    [HideInInspector] public bool gameActive;
    [HideInInspector] public bool gamePaused;
    [HideInInspector] public bool beginScreenActive;
    [HideInInspector] public bool alreadySavedStatsThisRun; //  This is here to prevent a double-save on game-over and head back to main menu scenario

    private bool playedGameStatSaved;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    public void SaveData(ref GameData data)
    {
        if (!playedGameStatSaved)
        {
            data.gamesPlayed++;
            playedGameStatSaved = true;
        }
    }

    private void InitializeGame()
    {
        alreadySavedStatsThisRun = false;
        playedGameStatSaved = false;
        beginScreenActive = true;
        OnGameReady?.Invoke();
        GameGUIManager.Instance.DisableAllScreens();
        GameGUIManager.Instance.EnableBeginScreen();
        
        DataPersistenceManager.Instance.SaveData();
    }

    public void StartGame()
    {
        beginScreenActive = false;
        gameActive = true;
        OnGameActiveChanged?.Invoke();

        GameGUIManager.Instance.DisableBeginScreen();
        GameGUIManager.Instance.EnableGameOverlay();
        GameGUIManager.Instance.EnablePauseButton();

        Debug.Log("Game started!");
    }

    public void Pause()
    {
        // If Time.timeScale is currently set to 0, set it to 1. Otherwise, set it to 0
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        SoundController.Instance.PlaySound(SoundType.Whoosh);

        if (Time.timeScale == 0)
        {
            gamePaused = true;
            GameGUIManager.Instance.EnablePauseMenu();
            GameGUIManager.Instance.DisablePauseButton();
        }
        else
        {
            gamePaused = false;
            GameGUIManager.Instance.DisablePauseMenu();
            GameGUIManager.Instance.EnablePauseButton();
        }
    }

    public void GameOver()
    {
        gameActive = false;
        OnGameActiveChanged?.Invoke();
        GameGUIManager.Instance.DisableGameOverlay();
        GameGUIManager.Instance.EnableGameOverScreen();

        DataPersistenceManager.Instance.SaveData();
        alreadySavedStatsThisRun = true;

        Debug.Log("Game over!");
    }

    public void RestartGame()
    {
        GameManager.Instance.ResetCurrentScene();
    }

    public void MainMenu()
    {
        DataPersistenceManager.Instance.SaveData();

        GameManager.Instance.MainMenu();
    }
}
