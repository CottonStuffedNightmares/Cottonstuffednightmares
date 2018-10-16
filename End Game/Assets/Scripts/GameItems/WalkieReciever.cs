using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]

public class WalkieReciever : MonoBehaviour {

    private GameObject playerCam;
    private GameObject soundTriggerRange;
    private GameObject displayWalkyNumber;
    private bool displayNumberSet = false;

    public int walkyNumber;

    public bool emittingSound;
    public float soundDuration;
    public float soundDurationMax;

    void Start () {
        playerCam = GameObject.Find("FirstPersonCharacter");
        soundTriggerRange = transform.Find("SoundTriggerRange").gameObject;
        displayWalkyNumber = transform.Find("Canvas/displayChannelNumber").gameObject;

        GetComponent<BoxCollider>().isTrigger = true;
        walkyNumber = 0;
        emittingSound = false;
        soundDurationMax = 2;
	}
	
	void Update () {

        // displays the walky channel over the walky talky
        if (!displayNumberSet) {
            displayWalkyNumber.GetComponent<Text>().text = walkyNumber.ToString();
            displayNumberSet = true;
        }

        // rotates on all axis instead of only Y, need fix?
        if (displayNumberSet) { 
            displayWalkyNumber.transform.LookAt(playerCam.transform.position);
        }

        // Sound timer countdown
        if (emittingSound && soundDuration >= 0) {
            soundDuration -= Time.deltaTime;
        }

        // Stop timer and set emitting to false;
        if (soundDuration <= 0 && emittingSound) {
            emittingSound = false;
        }
	}

    private void OnTriggerStay(Collider other) {
        if (emittingSound) {
            if (other.gameObject.GetComponent<Crocodile>()  ||
                other.gameObject.GetComponent<Owl>()        ||
                other.gameObject.GetComponent<TeddyBear>()) {

                // CALL DISTRACT FUNCTION WHEN ITS READY          
            }
        }
    }

    public void SetCurrentChannel(int val) {
        walkyNumber = val;
    }

    public int GetChannel() {
        return walkyNumber;
    }

    public void PlaySound() {
        soundDuration = soundDurationMax;
        emittingSound = true;
        Debug.Log("HELLO FROM RECIEVER " + walkyNumber);
    
        //PLAY DISTORTED AUDIO HERE
    }
}
