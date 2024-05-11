using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0f;
    public Text countdownText;
    public GameObject gameOverScreen;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevelString(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            StartCoroutine(SetupGameOver());
        }
        else
        {
            SceneManager.LoadScene(levelIndex);
        }
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

    IEnumerator SetupGameOver()
    {
        yield return new WaitForSeconds(2);
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(GameOverCountdown());
    }

    IEnumerator GameOverCountdown()
    {
        int countdownTime = 11;
        while (countdownTime > 0)
        {
            countdownText.text = "Returning to main menu in " + (countdownTime - 1).ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        // SceneManager.LoadScene(0);
    }
}
