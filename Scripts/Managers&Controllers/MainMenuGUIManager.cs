using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class MainMenuGUIManager : MonoBehaviour
{
    public static MainMenuGUIManager Instance { get; private set; }
    public event Action noLongerViewingExtrasWindow;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject titleScreen, clickToStartText, settingsWindow, statsWindow, extrasWindow, confirmExitWindow, exitingGameWindow, purchaseWindow, coinDisplayObj;
    [SerializeField] private RectTransform title, playGameButton, settingsButton, statsButton, extrasButton, exitGameButton, coinDisplay;
    [SerializeField] private float mainCameraPosY = 3f;
    [SerializeField] private float mainCameraMoveDuration = 3f;
    [SerializeField] private float titleOriginalX, titleOutsideX;
    [SerializeField] private float buttonsOriginalX, buttonsOutsideX;
    [SerializeField] private float activeWindowOutsideY;
    [SerializeField] private float settingsOriginalY, statsOriginalY, extrasOriginalY, confirmExitOriginalY;
    [SerializeField] private float coinDisplayOriginalX, coinDisplayOutsideX;
    [SerializeField] private float tweenDuration = 0.35f;
    private GameObject activeWindow;
    private RectTransform activeWindowRect;

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
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCameraPosY, mainCamera.transform.position.z);
        }

        AvatarManager.Instance.attemptToPurchaseAvatar += EnablePurchaseWindow;
        CheatManager.Instance.attemptToPurchaseCheat += EnablePurchaseWindow;
    }

    private void OnDisable()
    {
        AvatarManager.Instance.attemptToPurchaseAvatar -= EnablePurchaseWindow;
        CheatManager.Instance.attemptToPurchaseCheat -= EnablePurchaseWindow;
    }

    // *** CLICK TO START SCREEN METHODS ***
    public void DisableClickToStartText()
    {
        clickToStartText.SetActive(false);
    }

    // *** TITLE SCREEN METHODS ***
    public void EnableAndMoveTitleScreen()
    {
        coinDisplayObj.SetActive(true);
        titleScreen.SetActive(true);
        title.anchoredPosition = new Vector2(titleOriginalX, title.anchoredPosition.y);
        playGameButton.anchoredPosition = new Vector2(buttonsOriginalX, playGameButton.anchoredPosition.y);
        settingsButton.anchoredPosition = new Vector2(buttonsOriginalX, settingsButton.anchoredPosition.y);
        statsButton.anchoredPosition = new Vector2(buttonsOriginalX, statsButton.anchoredPosition.y);
        extrasButton.anchoredPosition = new Vector2(buttonsOriginalX, extrasButton.anchoredPosition.y);
        exitGameButton.anchoredPosition = new Vector2(buttonsOriginalX, exitGameButton.anchoredPosition.y);
        coinDisplay.anchoredPosition = new Vector2(coinDisplayOriginalX, coinDisplay.anchoredPosition.y);
    }

    public void IntroduceTitleScreen()
    {
        titleScreen.SetActive(true);
        coinDisplayObj.SetActive(true);

        GenericXIntro(title, titleOriginalX);
        GenericXIntro(playGameButton, buttonsOriginalX);
        GenericXIntro(settingsButton, buttonsOriginalX);
        GenericXIntro(statsButton, buttonsOriginalX);
        GenericXIntro(extrasButton, buttonsOriginalX);
        GenericXIntro(exitGameButton, buttonsOriginalX);
        GenericXIntro(coinDisplay, coinDisplayOriginalX);
    }

    public async void DismissTitleScreen()
    {
        var task1 = GenericXOutro(title, titleOutsideX);
        var task2 = GenericXOutro(playGameButton, buttonsOutsideX);
        var task3 = GenericXOutro(settingsButton, buttonsOutsideX);
        var task4 = GenericXOutro(statsButton, buttonsOutsideX);
        var task5 = GenericXOutro(extrasButton, buttonsOutsideX);
        var task6 = GenericXOutro(exitGameButton, buttonsOutsideX);

        await Task.WhenAll(task1, task2, task3, task4, task5, task6);
        titleScreen.SetActive(false);
    }

    public async void SlowlyIntroduceTitleScreen()
    {
        titleScreen.SetActive(true);
        coinDisplayObj.SetActive(true);

        await GenericWaitForXIntro(title, titleOriginalX);
        await GenericWaitForXIntro(playGameButton, buttonsOriginalX);
        await GenericWaitForXIntro(settingsButton, buttonsOriginalX);
        await GenericWaitForXIntro(statsButton, buttonsOriginalX);
        await GenericWaitForXIntro(extrasButton, buttonsOriginalX);
        await GenericWaitForXIntro(exitGameButton, buttonsOriginalX);
        await GenericWaitForXIntro(coinDisplay, coinDisplayOriginalX);
    }

    // *** SETTINGS METHODS ***
    public void IntroduceSettingsWindow()
    {
        settingsWindow.SetActive(true);
        activeWindow = settingsWindow;
        activeWindowRect = settingsWindow.GetComponent<RectTransform>();

        GenericYIntro(activeWindowRect, settingsOriginalY);
    }

    // *** STATS METHODS ***
    public void IntroduceStatsWindow()
    {
        statsWindow.SetActive(true);
        activeWindow = statsWindow;
        activeWindowRect = statsWindow.GetComponent<RectTransform>();

        GenericYIntro(activeWindowRect, statsOriginalY);
    }

    // *** EXTRAS METHODS ***
    public void IntroduceExtrasWindow()
    {
        extrasWindow.SetActive(true);
        activeWindow = extrasWindow;
        activeWindowRect = extrasWindow.GetComponent<RectTransform>();

        GenericYIntro(activeWindowRect, extrasOriginalY);
    }

    // *** CONFIRM QUIT METHODS ***
    public void IntroduceConfirmExitWindow()
    {
        confirmExitWindow.SetActive(true);
        activeWindow = confirmExitWindow;
        activeWindowRect = confirmExitWindow.GetComponent<RectTransform>();

        GenericYIntro(activeWindowRect, confirmExitOriginalY);
    }

    public void ExitingGameWindow()
    {
        RectTransform exitingGameWindowRect = exitingGameWindow.GetComponent<RectTransform>();
        RectTransform confirmExitWindowRect = confirmExitWindow.GetComponent<RectTransform>();
        exitingGameWindowRect.anchoredPosition = confirmExitWindowRect.anchoredPosition;
        confirmExitWindow.SetActive(false);
        exitingGameWindow.SetActive(true);
    }

    // ***  PURCHASE WINDOW METHODS ***
    public void EnablePurchaseWindow(string name, int price, int itemID)
    {
        purchaseWindow.SetActive(true);
        PurchaseWindow purchaseWindowScript = purchaseWindow.GetComponent<PurchaseWindow>();
        purchaseWindowScript.PurchaseInfo(name, price, itemID);
    }

    // *** COIN DISPLAY METHODS ***
    public void IntroduceCoinDisplay()
    {
        GenericXIntro(coinDisplay, coinDisplayOriginalX);
    }

    public async void DismissCoinDisplay()
    {
        await GenericXOutro(coinDisplay, coinDisplayOutsideX);
    }

    // *** CAMERA METHODS ***
    public async Task MoveCameraDown()
    {
        await mainCamera.transform.DOMoveY(mainCameraPosY, mainCameraMoveDuration).AsyncWaitForCompletion();
    }

    // *** GENERIC METHODS ***
    public void Back()
    {
        if (activeWindow == null)
        {
            DismissTitleScreen();
            IntroduceConfirmExitWindow();
        }
        else
        {
            DismissActiveWindow();
            IntroduceTitleScreen();
        }
    }

    public async void DismissActiveWindow()
    {
        if (activeWindow != null)
        {
            // Makes sure that the purchase window gets closed automatically if extras window is closed and purchase window is still open
            if (activeWindow == extrasWindow)
                noLongerViewingExtrasWindow?.Invoke();

            await GenericYOutro(activeWindowRect, activeWindowOutsideY);
            activeWindow.SetActive(false);
            activeWindow = null;
            activeWindowRect = null;
        }
    }

    private async Task GenericWaitForXIntro(RectTransform rect, float originalX)
    {
        await rect.DOAnchorPosX(originalX, tweenDuration).AsyncWaitForCompletion();
    }

    private void GenericXIntro(RectTransform rect, float originalX)
    {
        rect.DOAnchorPosX(originalX, tweenDuration);
    }

    private async Task GenericXOutro(RectTransform rect, float outsideX)
    {
        await rect.DOAnchorPosX(outsideX, tweenDuration).AsyncWaitForCompletion();
    }

    private void GenericYIntro(RectTransform rect, float originalY)
    {
        rect.DOAnchorPosY(originalY, tweenDuration);
    }

    private async Task GenericYOutro(RectTransform rect, float outsideY)
    {
        await rect.DOAnchorPosY(outsideY, tweenDuration).AsyncWaitForCompletion();
    }
}
