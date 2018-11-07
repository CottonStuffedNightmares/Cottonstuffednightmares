using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFlash : MonoBehaviour {

    //---------------------------------
    //Made By: Cappy (Callum Stirrup-Prazak)
    //
    //---------------------------------

    //Specifies The Game Object containing the lights used in lightning flashes
    [HideInInspector]
    public GameObject lightningFlash;

    //Checks if the lights have been flashed, as a way of ensuring that the code runs properly
    private bool flashIsOn;

    public GameObject[] lightningSources;
    public float[] soundDelays;

    //Used to perform Lightning flashes and controls how long the lights stay on before turning off
    [Header("Delay between flashes")]
    public float delayMin = 7;
    public float delayMax = 12;
    public float delayTimer = 5;

    [Header("Flash time")]
    public float flashMin = 0.05f;
    public float flashMax = 0.25f;
    public float flashTimer = 5;

    [Header("Lightning base brightness & vol")]
    public float baseStrengthMin = 1;
    public float baseStrengthMax = 4;
    public float soundDelayMin = 0.05f;
    public float soundDelayMax = 1;
    public float lightningVolDivider = 4f;

    [Header("Lightning flicker brightness")]
    public float lightningMin = 0.5f;
    public float lightningMax = 4f;
    


    // make an array of Lightning Sources and turn lights off
    private void Awake()
    {
        foreach (GameObject lightning in lightningSources)
        {
            lightning.GetComponent<Light>().intensity = 0;
        }
        flashIsOn = false;
        flashTimer = Random.Range(flashMin, flashMax);
    }

    

	
	void Update () {
		
        // count down to flash
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        
        // when countdown completes, do flash, set new delay, and invoke delayed StopFlash
        if (delayTimer <= 0)
        {
            delayTimer = Random.Range(delayMin, delayMax);
            Flash();
            Invoke("StopFlash", flashTimer);
        }

        Flicker();

    }

    //Controls the lightning flash using a random range between two floats then plays a sound
    public void Flash()
    {
        flashIsOn = true;

        foreach (GameObject lightning in lightningSources)
        {
            lightning.GetComponent<LightningSource>().baseStrength = Random.Range(baseStrengthMin, baseStrengthMax);
            lightning.GetComponent<LightningSource>().PlayDelayedSound();
        }

        
        //Invoke("PlaySound", Random.Range(soundDelayMin, soundDelayMax));
    }

    //Used for turning the light off after Flash
    public void StopFlash()
    {
        flashIsOn = false;
        flashTimer = Random.Range(flashMin, flashMax);
        foreach (GameObject lightning in lightningSources)
        {
            lightning.GetComponent<Light>().intensity = 0;
        }
        
    }

    //calculates the intensity for the next flash
    public void Flicker()
    {
        if (flashIsOn)
        {
            foreach (GameObject lightning in lightningSources)
            {
                lightning.GetComponent<Light>().intensity = Random.Range(lightningMin, lightningMax + lightning.GetComponent<LightningSource>().baseStrength);
            }
        }
    }


}
