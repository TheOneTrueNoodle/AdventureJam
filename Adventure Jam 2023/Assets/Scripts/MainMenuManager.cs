using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Scene gameScene;

    public GameObject startMenu;
    public GameObject settingsMenu;

    public void SetMusicVolume(float volume)
    {

    }

    public void SetSFXVolume(float volume)
    {

    }

    public void Play()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    public void OpenSettings()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    public void CloseSettings()
    {
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
