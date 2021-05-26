using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject specialUI;

    void Start()
    {

    }

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

        if (PlayerMovement.died)
        {
            GameOver();
        }

        if (PlayerMovement.canSpecial && !PlayerMovement.specialMode)
        {
            specialUI.SetActive(true);
        }
        else
        {
            specialUI.SetActive(false);
        }
    }

    public void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
    void GameOver()
    {
        gameOverMenuUI.SetActive(true);
    }
}
