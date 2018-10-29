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

    public void ToggleCreditsPanel() {
        if (creditsPanel.activeSelf == false) {
            creditsPanel.SetActive(true);
        }

        else {
            creditsPanel.SetActive(false);
        }
    }
}