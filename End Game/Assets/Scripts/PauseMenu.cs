using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject soundPanelOptions;
    public GameObject controlsPanelOptions;
    public GameObject graphicalPanelOptions;
    public GameObject mainMenuPanel;
    public GameObject restartPanel;

    // Use this for initialization
    void Awake()
    {
        Pause();
    }

    public void Pause()
    {
        //set active pause panel
        pausePanel.SetActive(true);
        // nothing else active
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    public void Options()
    {
        //set active options panel
        optionsPanel.SetActive(true);
        // nothing else active
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
        pausePanel.SetActive(false);

        //sound
        ///player
        ///enviromnet
        ///npc
        ///music
        //controls
        ///image of controls
        //graphical
        ///aspect ratio
    }

    public void MainMenu()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);

        //confirm();
    }

    public void Restart()
    {
        //?
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    public void Confirm()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    public void SoundPanel()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(true);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    public void Controls()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(true);
        graphicalPanelOptions.SetActive(false);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    public void Graphicaloptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        soundPanelOptions.SetActive(false);
        controlsPanelOptions.SetActive(false);
        graphicalPanelOptions.SetActive(true);
        mainMenuPanel.SetActive(false);
        restartPanel.SetActive(false);
    }
}
