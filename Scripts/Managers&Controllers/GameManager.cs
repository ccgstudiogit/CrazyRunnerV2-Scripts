using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public bool gameStarted;

    private SceneFadeInOut sceneFadeInOut;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            sceneFadeInOut = GetComponent<SceneFadeInOut>();
            gameStarted = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene("Game Scene"));
    }

    public void MainMenu()
    {
        StartCoroutine(ChangeScene("Main Menu"));
    }

    public void ResetCurrentScene()
    {
        StartCoroutine(ResetScene());
    }

    public void ExitGame()
    {
        DataPersistenceManager.Instance.SaveData();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator ChangeScene(string sceneName)
    {

        // This makes sure that if the player leaves the game scene from the pause menu, Time.timeScale will return to 1
        if (Time.timeScale == 0)
        {
            sceneFadeInOut.FadeOut();
            yield return new WaitForSecondsRealtime(sceneFadeInOut.timeToFade);
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;
            yield return new WaitForSeconds(sceneFadeInOut.delayTime);
            sceneFadeInOut.FadeIn();
        }
        else
        {
            sceneFadeInOut.FadeOut();
            yield return new WaitForSeconds(sceneFadeInOut.timeToFade);
            SceneManager.LoadScene(sceneName);
            yield return new WaitForSeconds(sceneFadeInOut.delayTime);
            sceneFadeInOut.FadeIn();
        }
    }

    private IEnumerator ResetScene()
    {
        sceneFadeInOut.FadeOut();
        yield return new WaitForSecondsRealtime(sceneFadeInOut.timeToFade);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSecondsRealtime(sceneFadeInOut.delayTime);
        sceneFadeInOut.FadeIn();
    }   
}
