using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName;

    public GameObject startMenu;
    public GameObject settingsMenu;

    public AudioMixer audioMixer;

    
    public Image transition;
    public float TransitionTime = 1f;

    public void Play()
    {
        StartCoroutine(LoadLevel());
    }

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

    IEnumerator LoadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        float alphaChange = 0;
        
        while(alphaChange < 1)
        {
            alphaChange += TransitionTime * Time.deltaTime;
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, alphaChange);
            yield return new WaitForSeconds(TransitionTime * Time.deltaTime);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
           yield return null;
        }
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
