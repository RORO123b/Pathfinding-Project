using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuMainGameScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseGameUI;

    public GameObject StartLevelButton;

    bool StartButtonActive;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        if (StartButtonActive == true)
            StartLevelButton.SetActive(true);
        PauseGameUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        if (StartLevelButton.activeSelf == true)
            StartButtonActive = true;
        else
            StartButtonActive = false;
        StartLevelButton.SetActive(false);
        PauseGameUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
