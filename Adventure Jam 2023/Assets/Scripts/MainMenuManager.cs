using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public Scene gameScene;

    public GameObject startMenu;
    public GameObject settingsMenu;

    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
