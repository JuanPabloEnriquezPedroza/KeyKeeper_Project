using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        PersistentData.obtainedKeys = 0;
        PersistentData.totalKeys = 5;
        PersistentData.healthPoints = PersistentData.initialHealthPoints;
        PersistentData.intialPositionChurch = new Vector3(-16.9f, 1.4f, 30.83f);
        PersistentData.intialViewChurch = new Vector3(0f, 0f, 0f);
        PersistentData.isGraveyard = false;
        PersistentData.isChurch = true;
        PersistentData.isCatacombs = false;
        PersistentData.isTower = false;
        PersistentData.isDungeons = false;
        PersistentData.keysID[0] = PersistentData.keysID[1] = PersistentData.keysID[2] = PersistentData.keysID[3] = PersistentData.keysID[4] = 0;
        PauseMenu.isPaused = false;
        SceneManager.LoadScene("Church");
    }

    public void QuitGame()
    {
        print("Quit pressed");
        Application.Quit();
    }
}

//https://youtu.be/zc8ac_qUXQY