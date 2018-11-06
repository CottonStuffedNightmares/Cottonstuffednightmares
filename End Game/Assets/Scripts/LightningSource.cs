using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightningSource : MonoBehaviour {

    public float baseStrength;
    private AudioSource audSrc;
    private LightningFlash lightningManager;


    private void Start()
    {
        audSrc = GetComponentInChildren<AudioSource>();
        lightningManager = GameObject.FindGameObjectWithTag("Flash Controller").GetComponent<LightningFlash>();
    }


    public void PlayDelayedSound()
    {
        Invoke("PlaySound", Random.Range (lightningManager.soundDelayMin, lightningManager.soundDelayMax));
    }



    public void PlaySound()
    {
        //randomise volume
        audSrc.volume = baseStrength / lightningManager.lightningVolDivider;
        Debug.Log(gameObject.name + " vol = " + audSrc.volume);
        // randomise pitch
        audSrc.pitch = Random.Range(0.6f, 1.1f);
        //play sound
        audSrc.Play();
    
    }


}
