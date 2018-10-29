using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFlash : MonoBehaviour {

    //Specifies The Game Object containing the lights used in lightning flashes
    [HideInInspector]
    public GameObject lightningFlash;

    //Checks if the lights have been flashed, as a way of ensuring that the code runs properly
    private bool flashIsOn;

    public GameObject[] lightningSources;
    public float[] soundDelays;

    //Used to perform Lightning flashes and controls how long the lights stay on before turning off
    [Header("Delay between flashes")]
    public float delayMin;
    public float delayMax;
    public float delayTimer = 5;

    [Header("Flash time")]
    public float flashMin;
    public float flashMax;
    public float flashTimer;

    [Header("Lightning base brightness & vol")]
    public float baseStrengthMin;
    public float baseStrengthMax;
    public float soundDelayMin;
    public float soundDelayMax;
    public float lightningVolDivider = 5f;

    [Header("Lightning flicker brightness")]
    public float lightningMin;
    public float lightningMax;
    


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


    public void StopFlash()
    {
        flashIsOn = false;
        flashTimer = Random.Range(flashMin, flashMax);
        foreach (GameObject lightning in lightningSources)
        {
            lightning.GetComponent<Light>().intensity = 0;
        }
        
    }


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
