using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System;

public class GameGUIManager : MonoBehaviour
{
    public static GameGUIManager Instance { get; private set; }

    public GameObject beginScreen, gameOverScreen;
    public GameObject pauseMenu, pauseButton;
    public GameObject scoreText, coinText, distanceText, gameOverlay;
    [SerializeField] private RectTransform pauseMenuRect, gameOverScreenRect, pauseButtonRect;
    [SerializeField] private RectTransform overlayDistance, overlayScore, overlayCoins;
    [SerializeField] private float topPosY, middlePosY;
    [SerializeField] private float pauseButtonOriginalX, pauseButtonOutsideX;
    [SerializeField] private float overlayDistanceTopY, overlayDistanceBottomY;
    [SerializeField] private float overlayScoreTopY, overlayScoreBottomY;
    [SerializeField] private float overlayCoinsOriginalX, overlayCoinsOutsideX;
    [SerializeField] private float tweenDuration, gameOverScreenDelay;

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

    public void DisableAllScreens()
    {
        beginScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    // *** GAME OVERLAY METHODS ***
    public void EnableGameOverlay()
    {
        gameOverlay.SetActive(true);
        GenericYIntro(overlayDistance, overlayDistanceBottomY);
        GenericYIntro(overlayScore, overlayScoreBottomY);
        GenericXIntro(overlayCoins, overlayCoinsOriginalX);
    }

    public async void DisableGameOverlay()
    {
        var task1 = GenericYOutro(overlayDistance, overlayDistanceTopY);
        var task2 = GenericYOutro(overlayScore, overlayScoreTopY);
        var task3 = GenericXOutro(overlayCoins, overlayCoinsOutsideX);

        await Task.WhenAll(task1, task2, task3);
        gameOverlay.SetActive(false);
    }

    // *** BEGIN SCREEN METHODS ***
    public void EnableBeginScreen()
    {
        beginScreen.SetActive(true);
    }

    public void DisableBeginScreen()
    {
        beginScreen.SetActive(false);
    }

    // *** GAME OVER SCREEN METHODS ***
    public void EnableGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        pauseButton.SetActive(false);
        GameOverScreenIntro();
    }

    private void GameOverScreenIntro()
    {
        gameOverScreenRect.DOAnchorPosY(middlePosY, tweenDuration).SetDelay(gameOverScreenDelay);
    }

    // *** PAUSE BUTTON METHODS ***
    public void EnablePauseButton()
    {
        // Side note: pauseButton is automatically turned off in EnableGameOverScreen()

        pauseButton.SetActive(true);
        GenericXIntro(pauseButtonRect, pauseButtonOriginalX);
    }

    public async void DisablePauseButton()
    {
        await GenericXOutro(pauseButtonRect, pauseButtonOutsideX);
        pauseButton.SetActive(false);
    }

    // *** PAUSE MENU METHODS ***
    public void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        PauseMenuIntro();
    }

    public async void DisablePauseMenu()
    {
        await PauseMenuOutro();
        pauseMenu.SetActive(false);
    }

    private void PauseMenuIntro()
    {
        pauseMenuRect.DOAnchorPosY(middlePosY + 40f, tweenDuration).SetUpdate(true);
    }

    private async Task PauseMenuOutro()
    {
        await pauseMenuRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }

    // *** GENERAL METHODS ***
    private void GenericXIntro(RectTransform rect, float originalX)
    {
        rect.DOAnchorPosX(originalX, tweenDuration).SetUpdate(true);
    }

    private async Task GenericXOutro(RectTransform rect, float outsideX)
    {
        await rect.DOAnchorPosX(outsideX, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }

    private void GenericYIntro(RectTransform rect, float originalY)
    {
        rect.DOAnchorPosY(originalY, tweenDuration).SetUpdate(true);
    }

    private async Task GenericYOutro(RectTransform rect, float outsideY)
    {
        await rect.DOAnchorPosY(outsideY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
