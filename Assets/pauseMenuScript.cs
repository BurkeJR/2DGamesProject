using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuScript : MonoBehaviour
{
    public static bool _GameIsPaused = false;
    public GameObject _pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_GameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        _GameIsPaused = false;
    }

    void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        _GameIsPaused = false;
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
