using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=======================================================
//  Created By Edward Ngo
//  Updates:
//=======================================================

    [RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour {

    private GameManagerScript gameManager;
    private AudioSource currentAudio;

    private GameObject owl;
    private GameObject croc;
    private GameObject bear;

    public AudioClip DefaultAudio;
    public AudioClip ChaseAudio;
    public AudioClip RainAudio;

    private bool isBeingChased;
    private bool isRaining;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameUI").GetComponent<GameManagerScript>();
        currentAudio = GetComponent<AudioSource>();

        croc = GameObject.FindGameObjectWithTag("Crocodile"); //.GetComponent<Crocodile>();
        bear = GameObject.FindGameObjectWithTag("Bear"); //.GetComponent<TeddyBear>();
        owl = GameObject.FindGameObjectWithTag("Owl"); //.GetComponent<Owl>();

        currentAudio.spatialBlend = 1;
        currentAudio.clip = DefaultAudio;

        isBeingChased = false;
        isRaining = false;
	}
	
	// Update is called once per frame
	void Update () {

        // Start bgm when game starts

        if (gameManager.isTutorialFinished) {
            PlayBGM();
        }

        // Play chase music when demons start hunting
        CheckHuntBGM();


        // check if tutorial has finished, if so start rain sfx
        //if (gameManager.isTutorialFinished) {
        //    isRaining = true;
        //}
        //
        //if (isRaining) {
        //    
        //    // START RAIN SFX
        //    //if (!rainSFX.isPlaying) {
        //    //    rainSFX.Play();
        //    //}
        //}  

	}

    void PlayBGM() {

        currentAudio.clip = RainAudio;
        currentAudio.volume = 0.5f;

        if (currentAudio != null && !currentAudio.isPlaying) {
            currentAudio.Play();
        }

       //if (!currentAudio.isPlaying && !isBeingChased) {
       //    if (currentAudio.clip != DefaultAudio) {
       //        currentAudio.clip = DefaultAudio;
       //        currentAudio.loop = true;
       //    }
       //}
       //
       //if (isBeingChased) {
       //    if (currentAudio.clip != ChaseAudio) {
       //        currentAudio.clip = ChaseAudio;
       //        currentAudio.loop = true;
       //    }
       //}
    }

    void CheckHuntBGM() {
        if (croc.GetComponent<Crocodile>().isHunting || bear.GetComponent<TeddyBear>().isHunting) {
            if (!isBeingChased) {
                isBeingChased = true;
            }
        }

        if (!croc.GetComponent<Crocodile>().isHunting && !bear.GetComponent<TeddyBear>().isHunting) {
            if (isBeingChased) {
                isBeingChased = false;
            }
        }
    }
}
