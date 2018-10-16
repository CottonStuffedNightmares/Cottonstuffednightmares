using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//=======================================================
// Created by: Edward Ngo
// Contributors:
//=======================================================

public class InfoDisplay : MonoBehaviour {

    // SCRIPT REFERENCE
    private Items items;
    private WalkieTalkie walkie;
    private Crocodile Crocodile;
    private TeddyBear Bear;  
    private Owl Owl;
    private GameManagerScript game;

    // USE TO DISPLAY IN GAME MESSAGES
    private string aMessage;
    private Text MsgBox;
    public float MsgTimer;

    public string tutorialMsg1;
    public string tutorialMsg2;
    public string tutorialMsg3;

    private bool tutorialMsg1Played = false;
    private bool tutorialMsg2Played = false;
    private bool tutorialMsg3Played = false;


    // PLUG IN INSPECTOR
    public Text CrocTimer, BearTimer, OwlTimer;
    public Text CountdownTimer;
    public Text WalkieChannel;
    public Text Tooltip;
	public Image CentreDot;

    // GAME MANAGER TIMER INFO
    string timeRemaining;
    int timerMinute;
    int timerSecond;

    void Start() {

        game =  GetComponent<GameManagerScript>();
        items = GameObject.Find("FirstPersonCharacter").GetComponent<Items>();
        walkie = GameObject.Find("FirstPersonCharacter").GetComponent<WalkieTalkie>();
        MsgBox = GameObject.Find("MessageBox").GetComponent<Text>();

        Tooltip.text = string.Empty;
        MsgBox.text = string.Empty;
        CountdownTimer.text = string.Empty;
        MsgTimer = 0;
        WalkieChannel.enabled = false;

        Crocodile = GameObject.FindGameObjectWithTag("Crocodile").GetComponent<Crocodile>();
        Bear = GameObject.FindGameObjectWithTag("Bear").GetComponent<TeddyBear>();
        Owl =  GameObject.FindGameObjectWithTag("Owl").GetComponent<Owl>();
    }

    void Update() {

        // TUTORIAL TIMER ==============================================
        if (!game.isTutorialFinished) {
            if (!tutorialMsg1Played) {
                DisplayMessage(tutorialMsg1, 3);
                tutorialMsg1Played = true;
                CountdownTimer.text = "Find spray bottle!";
            }

            if (items.BottleAcquired) {
                if (!items.TeapotAcquired && !tutorialMsg2Played) {
                    DisplayMessage(tutorialMsg2, 3);
                    CountdownTimer.text = "Find teacup!";
                    tutorialMsg2Played = true;
                }               
            }

            if (items.TeapotAcquired) {
                if (!items.BottleAcquired && !tutorialMsg3Played) {
                    DisplayMessage(tutorialMsg1, 3);
                    CountdownTimer.text = "Find spray bottle!";
                    tutorialMsg3Played = true;
                }
            }

            if (items.TeapotAcquired && items.BottleAcquired) {
                DisplayMessage(tutorialMsg3, 3);
                game.isTutorialFinished = true;
            }

            //GRACE PERIOD?
            //timeRemaining = string.Format("{0:0}:{1:00}", timerMinute, timerSecond);
            //timerMinute = Mathf.FloorToInt(game.tutorialTimer / 60f);
            //timerSecond = Mathf.FloorToInt(game.tutorialTimer - timerMinute * 60);
        }

        // IN GAME TIMER ==============================================
        if (game.isTutorialFinished) {
            timeRemaining = string.Format("{0:0}:{1:00}", timerMinute, timerSecond);
            timerMinute = Mathf.FloorToInt(game.GameTimer / 60f);
            timerSecond = Mathf.FloorToInt(game.GameTimer - timerMinute * 60);
            CountdownTimer.text = timeRemaining;
        }


        // MESSAGE DISPLAY TIMER ==============================================
        if (MsgBox.text != null) {
            if (MsgTimer > 0) {
                MsgTimer -= Time.deltaTime;
            }

            if (MsgTimer <= 0) {
                MsgBox.text = string.Empty;
            }
        }

        // MONSTER TRANSFORM TIMER
        CrocTimer.text = "Croc: " + Crocodile.timeToTransform.ToString("F0");
        BearTimer.text = "Bear: " + Bear.timeToTransform.ToString("F0");
        OwlTimer.text = "Owl: " + Owl.timeToTransform.ToString("F0");

        // WALKY CHANNELS DISPLAY
        if (items.currentItem == Items.ITEMTYPE.WALKYTALKY) {
            if (!WalkieChannel.enabled) {
                WalkieChannel.enabled = true;
            }

            if (WalkieChannel.enabled) {
                WalkieChannel.text = "CH: " + walkie.currentChannel.ToString();
            }
        }

        if (items.currentItem != Items.ITEMTYPE.WALKYTALKY) {
            if (WalkieChannel.enabled) {
                WalkieChannel.enabled = false;
            }
        }       
    }

    public void DisplayTooltip(string word) {
        Tooltip.text = word.ToString();
    }

    public void ClearTooltip() {
        Tooltip.text = string.Empty;
    }

    public void DisplayMessage(string msg, float time) {

        if (MsgBox.text != null) {
            MsgBox.text = string.Empty;
        }

        MsgBox.text = msg;
        MsgTimer = time;
    }
}
