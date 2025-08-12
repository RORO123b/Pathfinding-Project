using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuTestingScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseGameUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        PauseGameUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseGameUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
