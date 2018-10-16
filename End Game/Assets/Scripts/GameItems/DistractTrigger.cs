using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=======================================================
// Created by: Edward Ngo
// Contributors:
//=======================================================
// NOTE: this script need to be attached to 'SoundTriggerRange' gameObject 
//       which is child of 'WalkyReciever' gameObject.
//=======================================================

public class DistractTrigger : MonoBehaviour {

    private WalkieReciever reciever;
    private GameObject croc;
    private GameObject bear;
    private GameObject owl;

    // Use this for initialization
    void Start () {

        reciever = transform.parent.GetComponent<WalkieReciever>();
        croc = GameObject.FindGameObjectWithTag("Crocodile");
        bear = GameObject.FindGameObjectWithTag("Bear");
        owl = GameObject.FindGameObjectWithTag("Owl");
    }
	
    private void OnTriggerStay(Collider other) {

        if (reciever.emittingSound) {

            // DISTRACT CROC
            if (other.gameObject == croc /* && croc.canBeDistracted*/) {
                // CALL DISTRACT
            }

            if (other.gameObject == bear /* && croc.canBeDistracted*/) {
                // CALL DISTRACT
            }

            if (other.gameObject == owl /* && croc.canBeDistracted*/) {
                // CALL DISTRACT
            }
        }
    }


}
