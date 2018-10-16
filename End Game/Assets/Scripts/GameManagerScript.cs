using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    //=======================================================
    // Created by Liam Gates
    // Updates:
    // by Callum Bradshaw
    // -Edward Ngo
    //=======================================================

public class GameManagerScript : MonoBehaviour
{
    private Items items;
    private InfoDisplay infoDisplay;

    public GameObject WinScreen;
    public GameObject YouDied;

    public float tutorialTimer;
    public bool isTutorialFinished;
    public float GameTimer;
    public bool isNightmareStarted;
    public int CurrentScene = 1;

    [HideInInspector] public bool isGameOver;

    void Start()
    {
        items = GameObject.Find("FirstPersonCharacter").GetComponent<Items>();

        isGameOver = false;
        WinScreen.SetActive(false);
        YouDied.SetActive(false);

        isTutorialFinished = false;
        tutorialTimer = 30;
        GameTimer = 300;
    }

    private void Update()
    {

        // TUTORIAL COUNTDOWM ==============================================

        //if (!isTutorialFinished) {
        //    if (tutorialTimer > 0) {
        //        tutorialTimer -= Time.deltaTime;
        //    }
        //    
        //    if (tutorialTimer <= 0) {
        //        isTutorialFinished = true;
        //    }
        //}



        // GAME COUNTDOWN ==============================================
        if (isTutorialFinished) {
            if (GameTimer > 0) {
                GameTimer -= Time.fixedDeltaTime;
            }

            if (GameTimer <= 0) {
                WinGame();
            }
        }

        if(isGameOver)
        {
            SetGameOver();
        }
    }

    public void SetGameOver() {
        Time.timeScale = 0;
        GameTimer = 0;
        YouDied.SetActive(true);
    }

    public void WinGame() {
        Time.timeScale = 0;
        GameTimer = 0;
        infoDisplay.DisplayMessage("win message", 15);
    }
}
