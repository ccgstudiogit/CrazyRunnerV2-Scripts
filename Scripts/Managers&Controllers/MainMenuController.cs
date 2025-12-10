using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance { get; private set; }

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
        if (GameManager.Instance.gameStarted)
        {
            MainMenuGUIManager.Instance.DisableClickToStartText();
            MainMenuGUIManager.Instance.EnableAndMoveTitleScreen();
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.gameStarted && Input.anyKeyDown)
        {
            GameManager.Instance.gameStarted = true;
            PlayerClickedToStart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuGUIManager.Instance.Back();
        }
    }

    public async void PlayerClickedToStart()
    {
        MainMenuGUIManager.Instance.DisableClickToStartText();
        await MainMenuGUIManager.Instance.MoveCameraDown();
        MainMenuGUIManager.Instance.SlowlyIntroduceTitleScreen();
    }

    public void TitleScreen()
    {
        MainMenuGUIManager.Instance.DismissActiveWindow();
        MainMenuGUIManager.Instance.IntroduceTitleScreen();
    }

    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void Settings()
    {
        MainMenuGUIManager.Instance.DismissTitleScreen();
        MainMenuGUIManager.Instance.IntroduceSettingsWindow();
    }

    public void Stats()
    {
        MainMenuGUIManager.Instance.DismissTitleScreen();
        MainMenuGUIManager.Instance.IntroduceStatsWindow();
    }

    public void Extras()
    {
        MainMenuGUIManager.Instance.DismissTitleScreen();
        MainMenuGUIManager.Instance.IntroduceExtrasWindow();
    }

    public void ConfirmExitGame()
    {
        MainMenuGUIManager.Instance.DismissTitleScreen();
        MainMenuGUIManager.Instance.IntroduceConfirmExitWindow();
    }

    public void ExitGame()
    {
        MainMenuGUIManager.Instance.ExitingGameWindow();
        GameManager.Instance.ExitGame();
    }
}
