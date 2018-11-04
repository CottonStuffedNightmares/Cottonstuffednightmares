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

    // GET PLUSHIE/DEMON FORM UI ICONS
    private Image plushie_BearIcon;
    private Image plushie_CrocIcon;
    private Image plushie_OwlIcon;
    private Image demon_BearIcon;
    private Image demon_CrocIcon;
    private Image demon_OwlIcon;

    // Tooltip Icons
    public Sprite UseItemIcon;
    public Sprite InteractIcon;
    public Sprite PickUpIcon;

    // USE TO DISPLAY IN GAME MESSAGES
    private Text MsgBox;
    private float MsgTimer;
    
    public string tutorialMsg1;
    public string tutorialMsg2;
    public string tutorialMsg3;
    private bool tutorialMsg1Played = false;
    private bool tutorialMsg2Played = false;
    private bool tutorialMsg3Played = false;

    // PLUG IN INSPECTOR
    //public Text CrocTimer, BearTimer, OwlTimer;
    private float tempTooltipTimer;
    private Text CountdownTimer;
    public Image pickUpToolTip;
    public Image useItemTooltip;
	public Image CentreDot;

    //public Text Tooltip;
    //public Text WalkieChannel;

    // GAME MANAGER TIMER INFO
    string timeRemaining;
    int timerMinute;
    int timerSecond;

    void Start() {
        // Find script references
        game =  GetComponent<GameManagerScript>();
        items = GameObject.Find("FirstPersonCharacter").GetComponent<Items>();
        walkie = GameObject.Find("FirstPersonCharacter").GetComponent<WalkieTalkie>();
        MsgBox = GameObject.Find("MessageBox").GetComponent<Text>();
        CountdownTimer = transform.Find("GameTimerDisplay").GetComponent<Text>();

        // Find plushie/demon ui icon
        plushie_BearIcon = transform.Find("Timers/p_BearIcon").gameObject.GetComponent<Image>();
        plushie_CrocIcon = transform.Find("Timers/p_CrocIcon").gameObject.GetComponent<Image>();
        plushie_OwlIcon = transform.Find("Timers/p_OwlIcon").gameObject.GetComponent<Image>();
        demon_BearIcon = transform.Find("Timers/d_BearIcon").gameObject.GetComponent<Image>();
        demon_CrocIcon = transform.Find("Timers/d_CrocIcon").gameObject.GetComponent<Image>();
        demon_OwlIcon = transform.Find("Timers/d_OwlIcon").gameObject.GetComponent<Image>();

        // Misc Var.
        //Tooltip.text = string.Empty;
        MsgBox.text = string.Empty;
        CountdownTimer.text = string.Empty;
        MsgTimer = 0;
        tempTooltipTimer = 0;
        //WalkieChannel.enabled = false;

        // Find Monster gameObjects
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
        }

        // IN GAME TIMER ==============================================
        if (game.isTutorialFinished) {
            timeRemaining = string.Format("{0:0}:{1:00}", timerMinute, timerSecond);
            timerMinute = Mathf.FloorToInt(game.GameTimer / 60f);
            timerSecond = Mathf.FloorToInt(game.GameTimer - timerMinute * 60);
            CountdownTimer.text = "survive for: " + timeRemaining;

            // ADJUST ICON ALPHAS
            AdjustBearIconAlpha();
            AdjustCrocIconAlpha();
            AdjustOwlIconAlpha();    
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

        if (useItemTooltip.sprite != null) {
            if (tempTooltipTimer > 0) {
                tempTooltipTimer -= Time.deltaTime;
            }

            if (tempTooltipTimer <= 0) {
                ClearUseItemTooltip();
            }
        }
   
        // MONSTER TRANSFORM TIMER
        //CrocTimer.text = "Croc: " + Crocodile.timeToTransform.ToString("F0");
        //BearTimer.text = "Bear: " + Bear.timeToTransform.ToString("F0");
        //OwlTimer.text = "Owl: " + Owl.timeToTransform.ToString("F0");

        // WALKY CHANNELS DISPLAY
        //if (items.currentItem == Items.ITEMTYPE.WALKYTALKY) {
        //    if (!WalkieChannel.enabled) {
        //        WalkieChannel.enabled = true;
        //    }
        //
        //    if (WalkieChannel.enabled) {
        //        WalkieChannel.text = "CH: " + walkie.currentChannel.ToString();
        //    }
        //}
        //
        //if (items.currentItem != Items.ITEMTYPE.WALKYTALKY) {
        //    if (WalkieChannel.enabled) {
        //        WalkieChannel.enabled = false;
        //    }
        //}       
    }

    //public void DisplayTooltip(string word) {
    //    Tooltip.text = word.ToString();
    //}

    //public void ClearTooltip() {
    //    Tooltip.text = string.Empty;
    //
    //}

    public void DisplayMessage(string msg, float time) {

        if (MsgBox.text != null) {
            MsgBox.text = string.Empty;
        }

        MsgBox.text = msg;
        MsgTimer = time;
    }

    public void TemporaryTooltipDisplay(/*int num,*/ float timer) {

        if (!pickUpToolTip.sprite) {
            pickUpToolTip.sprite = null;
        }

        DisplayUseItemTooltip();
        tempTooltipTimer = timer;
    }

    public void DisplayUseItemTooltip() {
        Color tempColour = useItemTooltip.color;
        tempColour.a = 1;
        useItemTooltip.color = tempColour;
        useItemTooltip.sprite = UseItemIcon;
    }
    public void ChangeTooltip(int num) {
        Color tempColour = pickUpToolTip.color;
        tempColour.a = 1;
        pickUpToolTip.color = tempColour;

        // pick up item icon
        if (num == 1) {
            pickUpToolTip.sprite = PickUpIcon;
        }

        // use item icon
        //if (num == 2) {
        //    pickUpToolTip.sprite = UseItemIcon;
        //}

        // interact at object icon 
        if (num == 3) {
            pickUpToolTip.sprite = InteractIcon;
        }
    }

    public void ClearTooltipImage() {
        Color tempColour = pickUpToolTip.color;
        pickUpToolTip.sprite = null;
        tempColour.a = 0;
        pickUpToolTip.color = tempColour;
    }

    public void ClearUseItemTooltip() {
        Color tempColour = useItemTooltip.color;
        useItemTooltip.sprite = null;
        tempColour.a = 0;
        useItemTooltip.color = tempColour;

    }

    private void AdjustBearIconAlpha() {
        Color pBearAlpha = plushie_BearIcon.color;
        pBearAlpha.a = Bear.timeToTransform / Bear.timeToTransformMax;
        plushie_BearIcon.color = pBearAlpha;

        Color dBearAlpha = demon_BearIcon.color;
        dBearAlpha.a = 1 - pBearAlpha.a;
        demon_BearIcon.color = dBearAlpha;
    }

    private void AdjustCrocIconAlpha() {
        Color pCrocAlpha = plushie_CrocIcon.color;
        pCrocAlpha.a = Crocodile.timeToTransform / Crocodile.timeToTransformMax;
        plushie_CrocIcon.color = pCrocAlpha;

        Color dCrocAlpha = plushie_CrocIcon.color;
        dCrocAlpha.a = 1 - pCrocAlpha.a;
        demon_CrocIcon.color = dCrocAlpha;
    }

    private void AdjustOwlIconAlpha() {
        Color pOwlAlpha = plushie_OwlIcon.color;
        pOwlAlpha.a = Owl.timeToTransform / Owl.timeToTransformMax;
        plushie_OwlIcon.color = pOwlAlpha;

        Color dOwlAlpha = demon_OwlIcon.color;
        dOwlAlpha.a = 1 - pOwlAlpha.a;
        demon_OwlIcon.color = dOwlAlpha;
    }
}
