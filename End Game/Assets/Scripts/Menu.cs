using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;

public class Menu : MonoBehaviour
{

    //Panels
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;
    public GameObject creditsPanel;

    //Bools
    private bool controlsActive = false;
    
    void Start()
    {
        Time.timeScale = 1;
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
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
        if (controlsPanel.activeSelf == false)
        {
            controlsPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
        else
        {
            controlsPanel.SetActive(false);
        }
    }

    public void ToggleCreditsPanel() {
        if (creditsPanel.activeSelf == false) {
            creditsPanel.SetActive(true);
            controlsPanel.SetActive(false);
        }

        else {
            creditsPanel.SetActive(false);
        }
    }
}