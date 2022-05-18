using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    GameObject HUD;

    void Update()
    {
        if(PersistentData.healthPoints > 0)
        {
            if (!isPaused)
            {
                Cursor.visible = false;
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
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        //HUD.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        //HUD.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        print("Quit pressed");
        Application.Quit();
    }
}
