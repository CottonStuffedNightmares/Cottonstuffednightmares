using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;

[RequireComponent(typeof(AudioSource))]

public class Menu : MonoBehaviour
{
    private AudioSource BGM;
    public AudioClip menuBGM;

    //Panels
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    //Bools
    private bool controlsActive = false;
    
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        BGM.clip = menuBGM;
        BGM.playOnAwake = true;
        BGM.loop = true;
        BGM.volume = 0.75f;
        BGM.Play();

        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
    //Game Start
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    ///Quit
    public void ExitGame()
    {
        Application.Quit();
    }

    ///Toggles the Controls Panel
    public void ControlsToggle()
    {
        if (controlsActive)
        {
            mainMenuPanel.SetActive(false);
            controlsPanel.SetActive(true);
            controlsActive = true;
        }
        else
        {
            mainMenuPanel.SetActive(true);
            controlsPanel.SetActive(false);
            controlsActive = false;
        }
    }
}